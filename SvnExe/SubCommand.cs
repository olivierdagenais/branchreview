using System;
using System.Collections.Generic;
using System.Text;
using SoftwareNinjas.BranchAndReviewTools.Core;

namespace SoftwareNinjas.BranchAndReviewTools.SvnExe
{
    internal struct SubCommand
    {
        public static SubCommand Info = new SubCommand("info", new string[] { "--non-interactive", "--xml" });

        private string _commandName;
        private IEnumerable<string> _arguments;

        private SubCommand(string commandName, IEnumerable<string> standardArguments)
        {
            _commandName = commandName;
            _arguments = EnumerableExtensions.Compose(_commandName, standardArguments);
        }

        public override string ToString()
        {
            return _commandName;
        }

        public IEnumerable<string> Arguments
        {
            get
            {
                return _arguments;
            }
        }
    }
}
