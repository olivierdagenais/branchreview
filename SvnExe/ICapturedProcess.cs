using System;

namespace SoftwareNinjas.BranchAndReviewTools.SvnExe
{
    internal interface ICapturedProcess : IDisposable
    {
        int Run();
    }
}
