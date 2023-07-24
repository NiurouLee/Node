using System;

namespace NFrameWork.Singleton
{
    public abstract class Singleton<T> : ISingleton where T : Singleton<T>, new()
    {
        private bool isDisposed;
        private static T _instance;
        public static T Ins => _instance;

        public bool IsDisposed()
        {
            return isDisposed;
        }

        public virtual void Register()
        {
            if (_instance == null)
            {
                throw new Exception($"重复注册单例{typeof(T).Name}");
            }

            _instance = (T)this;
        }

        public void Destroy()
        {
            if (isDisposed)
            {
                return;
            }

            isDisposed = true;
            T t = _instance;
            _instance = null;
            t.Dispose();
        }

        protected virtual void Dispose()
        {
        }
    }
}