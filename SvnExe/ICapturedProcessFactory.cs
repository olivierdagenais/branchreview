using System;
using System.Collections.Generic;

namespace SoftwareNinjas.BranchAndReviewTools.SvnExe
{
    internal interface ICapturedProcessFactory
    {
        ICapturedProcess Create(string pathToExecutable, IEnumerable<object> arguments,
                                Action<string> standardOutHandler, Action<string> standardErrorHandler);
    }
}
