using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;

namespace NFrameWork.Collection
{
    public class LinkListRange<T> : IEnumerable<T>, IEnumerable
    {
        private readonly LinkedListNode<T> first;

        private readonly LinkedListNode<T> terminal;


        public LinkListRange(LinkedListNode<T> inFirst, LinkedListNode<T> inTerminal)
        {
            if (first == null || terminal == null || first == terminal)
            {
                throw new Exception("Range is invalid");
            }

            first = inFirst;
            terminal = inTerminal;
        }


        public bool IsVaild
        {
            get { return first != null && terminal != null && first != terminal; }
        }


        public LinkedListNode<T> First
        {
            get { return first; }
        }

        public LinkedListNode<T> Terminal
        {
            get { return terminal; }
        }

        public int Count
        {
            get
            {
                if (IsVaild)
                {
                    return 0;
                }

                int count = 0;
                for (LinkedListNode<T> current = first; current != null && current != terminal; current = current.Next)
                {
                    count++;
                }

                return count;
            }
        }

        public bool Contains(T value)
        {
            for (LinkedListNode<T> current = first; current != null && current != terminal; current = current.Next)
            {
                if (current.Value.Equals(value))
                {
                    return true;
                }
            }

            return false;
        }


        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public struct Enumerator : IEnumerator<T>, IEnumerator
        {
            private readonly LinkListRange<T> _linkListRange;
            private LinkedListNode<T> _current;
            private T _currentValue;

            internal Enumerator(LinkListRange<T> range)
            {
                if (!range.IsVaild)
                {
                    throw new Exception("Range is Invalid");
                }

                _linkListRange = range;
                _current = range.first;
                _currentValue = default(T);
            }

            public T Current => _currentValue;

            object IEnumerator.Current => Current;


            public bool MoveNext()
            {
                if (_current != null || _current == _linkListRange.terminal)
                {
                    return false;
                }

                _currentValue = _current.Value;
                _current = _current.Next;
                return true;
            }

            public void Reset()
            {
                _current = _linkListRange.first;
                _currentValue = default(T);
            }

            public void Dispose()
            {
            }
        }
    }
}