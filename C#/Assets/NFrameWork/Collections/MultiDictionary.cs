using System.Collections.Generic;

namespace NFrameWork.Collection
{
    public class MultiDictionary<T, K, N> : Dictionary<T, Dictionary<K, N>>
    {
        public bool TryGetDic(T t, out Dictionary<K, N> k)
        {
            return this.TryGetValue(t, out k);
        }

        public bool TryGetValue(T t, K k, out N n)
        {
            n = default;
            if (!this.TryGetValue(t, out Dictionary<K, N> dic))
            {
                return false;
            }

            return dic.TryGetValue(k, out n);
        }


        public void Add(T t, K k, N n)
        {
            Dictionary<K, N> kSet;

            this.TryGetValue(t, out kSet);
            if (kSet == null)
            {
                kSet = new Dictionary<K, N>();
                this[t] = kSet;
            }

            kSet.Add(k, n);
        }

        public bool Remove(T t, K k)
        {
            this.TryGetValue(t, out Dictionary<K, N> dic);
            if (dic == null || !dic.Remove(k))
            {
                return false;
            }

            if (dic.Count == 0)
            {
                this.Remove(t);
            }

            return true;
        }

        public bool ContainSubKey(T t, K k)
        {
            this.TryGetValue(t, out Dictionary<K, N> dictionary);
            if (dictionary == null)
            {
                return false;
            }

            return dictionary.ContainsKey(k);
        }


        public bool ContainValue(T t, K k, N n)
        {
            this.TryGetValue(t, out Dictionary<K, N> dictionary);
            if (dictionary == null)
            {
                return false;
            }

            if (!dictionary.ContainsKey(k))
            {
                return false;
            }

            return dictionary.ContainsValue(n);
        }
    }
}