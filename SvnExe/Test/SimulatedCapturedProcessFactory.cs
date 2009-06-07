using System;
using System.Collections.Generic;
using System.Text;

namespace SoftwareNinjas.BranchAndReviewTools.SvnExe.Test
{
    internal class SimulatedCapturedProcessFactory : ICapturedProcessFactory
    {
        private SimulatedCapturedProcess _instance;

        internal SimulatedCapturedProcessFactory(SimulatedCapturedProcess instance)
        {
            _instance = instance;
        }

        #region ICapturedProcessFactory Members

        ICapturedProcess ICapturedProcessFactory.Create(string pathToExecutable, 
            IEnumerable<object> arguments, Action<string> standardOutHandler, Action<string> standardErrorHandler)
        {
            _instance.StandardOutHandler = standardOutHandler;
            _instance.StandardErrorHandler = standardErrorHandler;
            return _instance;
        }

        #endregion
    }
}
