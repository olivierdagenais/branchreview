using System;
using System.Collections.Generic;
using System.Text;

namespace SoftwareNinjas.BranchAndReviewTools.SvnExe.Test
{
    internal class SimulatedCapturedProcess : ICapturedProcess
    {
        private static readonly string[] emptyStringArray = new string[] { };
        private static readonly string[] splittingStrings = new string[] { Environment.NewLine };
        private readonly IEnumerable<string> _stdOut;
        private readonly IEnumerable<string> _stdErr;
        private readonly int _exitCode;

        internal SimulatedCapturedProcess(string stdOut, string stdErr, int exitCode)
        {
            _stdOut = ToLines(stdOut);
            _stdErr = ToLines(stdErr);

            _exitCode = exitCode;
        }

        internal static IEnumerable<string> ToLines(string input)
        {
            if (null == input)
            {
                return emptyStringArray;
            }
            else
            {
                return input.Split(splittingStrings, StringSplitOptions.None);
            }
        }

        internal SimulatedCapturedProcess(IEnumerable<string> stdOut, IEnumerable<string> stdErr, int exitCode)
        {
            _stdOut = stdOut;
            _stdErr = stdErr;

            _exitCode = exitCode;
        }

        public Action<string> StandardOutHandler
        {
            get;
            set;
        }

        public Action<string> StandardErrorHandler
        {
            get;
            set;
        }

        #region ICapturedProcess Members

        int ICapturedProcess.Run()
        {
            foreach (string line in _stdOut)
            {
                if (StandardOutHandler != null)
                {
                    StandardOutHandler(line);
                }
            }
            foreach (string line in _stdErr)
            {
                if (StandardErrorHandler != null)
                {
                    StandardErrorHandler(line);
                }
            }
            return _exitCode;
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            // do nothing
        }

        #endregion
    }
}
