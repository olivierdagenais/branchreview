using System;
using System.Collections.Generic;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Collections
{
    public class LinkedListHistory<T> : AbstractHistory<T>
    {
        private Node<T> _first, _current;
        private int _count;

        private class Node<TItem>
        {
            private readonly TItem _item;
            public Node<TItem> Previous { get; set; }
            public Node<TItem> Next { get; set; }
            public Node(TItem item)
            {
                _item = item;
            }
            public TItem Item { get { return _item; } }
        }

        public override void Go(T destination)
        {
            var node = new Node<T>(destination);
            if (_first == null && _current == null)
            {
                _first = node;
            }
            else
            {
                _current.Next = node;
                node.Previous = _current;
            }
            _current = node;
            _count++;
        }

        public override bool CanBack { get { return _current != null && _current.Previous != null; } }

        public override void Back()
        {
            AssertThereAreItems();
            AssertCanGoBack();
            _current = _current.Previous;
            _count--;
        }

        internal void AssertCanGoBack()
        {
            if (!CanBack)
            {
                throw new InvalidOperationException("Can't go back beyond first item");
            }
        }

        internal void AssertThereAreItems()
        {
            if (_current == null)
            {
                throw new InvalidOperationException("There are no items");
            }
        }

        public override void BackTo(T destination)
        {
            AssertThereAreItems();
            AssertCanGoBack();
            var current = _current;
            var count = _count;
            var found = false;
            while (count > 1)
            {
                if (ReferenceEquals(current.Item, destination))
                {
                    found = true;
                    break;
                }
                current = current.Previous;
                count--;
            }
            if (found)
            {
                _current = current;
                _count = count;
            }
            else
            {
                throw new ArgumentException("Item was not found");
            }
        }

        public override bool CanForward { get { return _current != null && _current.Next != null; } }

        public override void Forward()
        {
            AssertThereAreItems();
            AssertCanGoForward();
            _current = _current.Next;
            _count++;
        }

        internal void AssertCanGoForward()
        {
            if (!CanForward)
            {
                throw new InvalidOperationException("Can't go forward beyond last item");
            }
        }

        public override int Count
        {
            get { return _count; }
        }

        public override T Last
        {
            get { return _current.Item; }
        }

        public override IEnumerator<T> GetEnumerator()
        {
            var current = _first;
            while (current != null)
            {
                yield return current.Item;
                if (current == _current)
                {
                    break;
                }
                current = current.Next;
            }
        }
    }
}
