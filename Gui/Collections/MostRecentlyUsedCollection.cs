﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Collections
{
    public class MostRecentlyUsedCollection<T> : ICollection<T>
    {
        private readonly IList<T> _list = new List<T>();

        #region Implementation of IEnumerable

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of ICollection<T>

        public void Add(T item)
        {
            Remove(item);
            _list.Insert(0, item);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotSupportedException();
        }

        public bool Remove(T item)
        {
            return _list.Remove(item);
        }

        public int Count
        {
            get { return _list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        #endregion

        public T Penultimate
        {
            get
            {
                return _list.Count > 1 ? _list[1] : default(T);
            }
        }
    }
}
