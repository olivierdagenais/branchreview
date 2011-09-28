using System;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    public class BeforeAfter : IDisposable
    {
        private readonly Action _before, _after;
        public BeforeAfter(Action before, Action after)
        {
            _before = before;
            _after = after;
            _before();
        }

        public void Dispose()
        {
            _after();
        }
    }
}
