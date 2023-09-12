using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;

namespace NFrameWork.Collection
{
    public class MultiMapMpaLink<TKey, TValue> : IEnumerable<KeyValuePair<TKey, LinkListRange<TValue>>>, IEnumerable
    {
        private readonly LinkedList<TValue> linkedList;

        private readonly Dictionary<TKey, LinkListRange<TValue>> dictionary;


        public MultiMapMpaLink()
        {
            linkedList = new LinkedList<TValue>();
            dictionary = new Dictionary<TKey, LinkListRange<TValue>>();
        }


        public int Count => dictionary.Count;


        public LinkListRange<TValue> this[TKey key]
        {
            get
            {
                LinkListRange<TValue> range = default(LinkListRange<TValue>);
                dictionary.TryGetValue(key, out range);
                return range;
            }
        }

        public void Clear()
        {
            dictionary.Clear();
            linkedList.Clear();
        }


        public bool Contains(TKey key)
        {
            return dictionary.ContainsKey(key);
        }


        public bool Contains(TKey key, TValue value)
        {
            LinkListRange<TValue> range = default(LinkListRange<TValue>);
            if (dictionary.TryGetValue(key, out range))
            {
                return range.Contains(value);
            }

            return false;
        }


        public bool TryGetValue(TKey key, out LinkListRange<TValue> value)
        {
            return dictionary.TryGetValue(key, out value);
        }


        public void Add(TKey key, TValue value)
        {
            LinkListRange<TValue> range = default(LinkListRange<TValue>);
            if (dictionary.TryGetValue(key, out range))
            {
                linkedList.AddBefore(range.Terminal, value);
            }
            else
            {
                LinkedListNode<TValue> first = linkedList.AddLast(value);
                LinkedListNode<TValue> terminal = linkedList.AddLast(default(TValue));
                dictionary.Add(key, new LinkListRange<TValue>(first, terminal));
            }
        }


        public bool Remove(TKey key, TValue value)
        {
            LinkListRange<TValue> range = default(LinkListRange<TValue>);
            if (dictionary.TryGetValue(key, out range))
            {
                for (LinkedListNode<TValue> current = range.First;
                     current != null && current != range.Terminal;
                     current = current.Next)
                {
                    if (current.Value.Equals(value))
                    {
                        if (current == range.First)
                        {
                            LinkedListNode<TValue> next = current.Next;
                            if (next == range.Terminal)
                            {
                                linkedList.Remove(next);
                                dictionary.Remove(key);
                            }
                            else
                            {
                                dictionary[key] = new LinkListRange<TValue>(next, range.Terminal);
                            }
                        }

                        linkedList.Remove(current);
                        return true;
                    }
                }
            }

            return false;
        }

        public bool RemoveAll(TKey key)
        {
            LinkListRange<TValue> range = default(LinkListRange<TValue>);
            if (dictionary.TryGetValue(key, out range))
            {
                dictionary.Remove(key);
                LinkedListNode<TValue> current = range.First;
                while (current != null)
                {
                    LinkedListNode<TValue> next = current != range.Terminal ? current.Next : null;
                    linkedList.Remove(current);
                    current = next;
                }

                return true;
            }

            return false;
        }

        public IEnumerator<KeyValuePair<TKey, LinkListRange<TValue>>> GetEnumerator()
        {
            return new Enumerator(dictionary);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<KeyValuePair<TKey, LinkListRange<TValue>>> IEnumerable<KeyValuePair<TKey, LinkListRange<TValue>>>.
            GetEnumerator()
        {
            return GetEnumerator();
        }

        [StructLayout(LayoutKind.Auto)]
        internal struct Enumerator : IEnumerator<KeyValuePair<TKey, LinkListRange<TValue>>>, IEnumerator
        {
            private Dictionary<TKey, LinkListRange<TValue>>.Enumerator enumerator;

            internal Enumerator(Dictionary<TKey, LinkListRange<TValue>> dictionary)
            {
                if (dictionary == null)
                {
                    throw new Exception("Dictionary is invalid");
                }

                enumerator = dictionary.GetEnumerator();
            }


            public bool MoveNext()
            {
                return enumerator.MoveNext();
            }

            public void Reset()
            {
                ((IEnumerator<KeyValuePair<TKey, LinkListRange<TValue>>>)enumerator).Reset();
            }

            public KeyValuePair<TKey, LinkListRange<TValue>> Current
            {
                get => enumerator.Current;
            }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                enumerator.Dispose();
            }
        }
    }
}