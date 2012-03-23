using System.Collections;
using System.Collections.Generic;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Collections
{
    public abstract class AbstractHistory<T> : IEnumerable<T>
    {
        public abstract void Go(T destination);
        public abstract bool CanBack { get; }
        public abstract void Back();
        public abstract void BackTo(T destination);
        public abstract bool CanForward { get; }
        public abstract void Forward();
        public abstract int Count { get; }
        public abstract T Last { get; }

        public abstract IEnumerator<T> GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
