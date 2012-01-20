using System;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    public class BeforeAfter : IDisposable
    {
        private readonly Action _after;
        public BeforeAfter(Action before, Action after)
        {
            _after = after;
            before();
        }

        public void Dispose()
        {
            _after();
        }
    }
}
