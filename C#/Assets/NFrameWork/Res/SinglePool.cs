using System;
using System.Collections.Generic;
using UnityEngine;

namespace NFrameWork.Res
{
    /// <summary>
    /// 一旦简单的同质Item缓存
    /// </summary>
    public class SinglePool<T> where T : UnityEngine.Object

    {
        public int MaxSize { get; private set; }
        public Queue<T> cached;
        public event Action<T> DestroyAction;
        public event Action<T> PutAction;

        public SinglePool(int inMaxSize)
        {
            cached = new Queue<T>();
            this.MaxSize = inMaxSize;
        }

        public T Take()
        {
            T item = null;
            if (this.cached.Count > 0)
            {
                item = cached.Dequeue();
            }

            return item;
        }

        public void Put(T inItem)
        {
            if (inItem == null)
            {
                return;
            }

            if (this.cached.Count > this.MaxSize)
            {
                this.PutAction?.Invoke(inItem);
                this.DestroyAction?.Invoke(inItem);
            }
            else
            {
                this.PutAction?.Invoke(inItem);
                this.cached.Enqueue(inItem);
            }
        }

        public void Destroy()
        {
            if (this.cached != null)
            {
                while (this.cached.Count > 0)
                {
                    var item = cached.Dequeue();
                    this.DestroyAction?.Invoke(item);
                }

                cached.Clear();
                cached = null;
            }
        }

        public void SetMaxSize(int inMaxSize)
        {
            this.MaxSize = inMaxSize;
        }
    }
}