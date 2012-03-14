using System;
using System.Collections;
using System.Collections.Generic;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    public abstract class UnsupportedList<T> : IList<T>
    {
        public virtual IEnumerator<T> GetEnumerator()
        {
            throw new NotSupportedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public virtual void Add(T item)
        {
            throw new NotSupportedException();
        }

        public virtual void Clear()
        {
            throw new NotSupportedException();
        }

        public virtual bool Contains(T item)
        {
            throw new NotSupportedException();
        }

        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotSupportedException();
        }

        public virtual bool Remove(T item)
        {
            throw new NotSupportedException();
        }

        public virtual int Count
        {
            get { throw new NotSupportedException(); }
        }

        public virtual bool IsReadOnly
        {
            get { throw new NotSupportedException(); }
        }

        public virtual int IndexOf(T item)
        {
            throw new NotSupportedException();
        }

        public virtual void Insert(int index, T item)
        {
            throw new NotSupportedException();
        }

        public virtual void RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        public virtual T this[int index]
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }
    }
}
