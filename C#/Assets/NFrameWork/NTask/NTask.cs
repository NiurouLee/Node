using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace NFrameWork.NTask
{
    [AsyncMethodBuilder(typeof(NAsyncTaskMethodBuilder))]
    public class NTask : ICriticalNotifyCompletion
    {
        public static Action<Exception> ExceptionHandler;

        public static NTaskCompleted completedTask => new();

        private static readonly ConcurrentQueue<NTask> queue = new();

        public static NTask Create(bool fromPool = false)
        {
            if (!fromPool)
            {
                return new NTask();
            }

            if (!queue.TryDequeue(out NTask task))
            {
                return new NTask() { fromPool = true };
            }

            return task;
        }


        private bool fromPool;

        private AwaiterStatus state;

        private object callback;

        private NTask()
        {
        }

        private void Recycle()
        {
            if (!this.fromPool)
            {
                return;
            }

            this.state = AwaiterStatus.Pending;
            this.callback = null;
            if (queue.Count > 1000)
            {
                return;
            }

            queue.Enqueue(this);
        }


        private async NVoid InnerCoroutine()
        {
            await this;
        }


        public void Coroutine()
        {
            InnerCoroutine().Coroutine();
        }


        public NTask GetAwaiter()
        {
            return this;
        }


        public bool IsCompleted
        {
            get { return this.state != AwaiterStatus.Pending; }
        }


        public void UnsafeOnCompleted(Action action)
        {
            if (this.state != AwaiterStatus.Pending)
            {
                action?.Invoke();
            }

            this.callback = action;
        }


        public void OnCompleted(Action action)
        {
            this.UnsafeOnCompleted(action);
        }


        public void GetResult()
        {
            switch (this.state)
            {
                case AwaiterStatus.Succeeded:
                    this.Recycle();
                    break;
                case AwaiterStatus.Faulted:
                    ExceptionDispatchInfo c = this.callback as ExceptionDispatchInfo;
                    this.callback = null;
                    this.Recycle();
                    c?.Throw();
                    break;
                default:
                    throw new NotSupportedException(
                        "NTask does not allow call GetResult directly when task not completed. Please use 'await'.");
            }
        }

        public void SetResult()
        {
            if (this.state != AwaiterStatus.Pending)
            {
                throw new InvalidOperationException("TaskT_TransitionToFinal_AlreadyCompleted");
            }

            this.state = AwaiterStatus.Succeeded;

            Action c = this.callback as Action;
            this.callback = null;
            c?.Invoke();
        }


        public void SetException(Exception e)
        {
            if (this.state != AwaiterStatus.Pending)
            {
                throw new InvalidOperationException("TaskT_TransitionToFinal_AlreadyCompleted");
            }

            this.state = AwaiterStatus.Faulted;
            Action c = this.callback as Action;
            this.callback = ExceptionDispatchInfo.Capture(e);
            c?.Invoke();
        }
    }


    [AsyncMethodBuilder(typeof(NAsyncTaskMethodBuilder<>))]
    public class NTask<T> : ICriticalNotifyCompletion
    {
        private static readonly ConcurrentQueue<NTask<T>> queue = new ConcurrentQueue<NTask<T>>();

        public static NTask<T> Create(bool fromPool = false)
        {
            if (!fromPool)
            {
                return new NTask<T>();
            }

            if (!queue.TryDequeue(out NTask<T> task))
            {
                return new NTask<T>() { fromPool = true };
            }

            return task;
        }


        private bool fromPool;

        private AwaiterStatus state;

        private T value;

        private object callback;


        private NTask()
        {
        }


        private void Recycle()
        {
            if (!this.fromPool)
            {
                return;
            }

            this.callback = null;
            this.value = default;
            this.state = AwaiterStatus.Pending;
            if (queue.Count > 10000)
            {
                return;
            }

            queue.Enqueue(this);
        }

        private async NVoid InnerCoroutine()
        {
            await this;
        }

        public void Coroutine()
        {
            InnerCoroutine().Coroutine();
        }

        public NTask<T> GetAwaiter()
        {
            return this;
        }


        public T GetResult()
        {
            switch (this.state)
            {
                case AwaiterStatus.Succeeded:
                    T v = this.value;
                    this.Recycle();
                    return v;
                case AwaiterStatus.Faulted:
                    ExceptionDispatchInfo c = this.callback as ExceptionDispatchInfo;
                    this.callback = null;
                    this.Recycle();
                    c?.Throw();
                    return default;
                default:
                    throw new NotSupportedException(
                        "NTask does not allow call GetResult directly when task not completed.Please use 'await'.");
            }
        }


        public bool IsCompleted
        {
            get { return state != AwaiterStatus.Pending; }
        }


        public void UnsafeOnCompleted(Action action)
        {
            if (this.state != AwaiterStatus.Pending)
            {
                action?.Invoke();
                return;
            }

            this.callback = action;
        }

        public void OnCompleted(Action action)
        {
            this.UnsafeOnCompleted(action);
        }

        public void SetResult(T result)
        {
            if (this.state != AwaiterStatus.Pending)
            {
                throw new InvalidOperationException("TaskT_TransitionToFinal_AlreadyCompleted");
            }

            this.state = AwaiterStatus.Succeeded;

            this.value = result;
            Action c = this.callback as Action;
            this.callback = null;
            c?.Invoke();
        }


        public void SetException(Exception e)
        {
            if (this.state != AwaiterStatus.Pending)
            {
                throw new InvalidOperationException("TaskT_TransitionToFinal_AlreadyCompleted");
            }

            this.state = AwaiterStatus.Faulted;
            Action c = this.callback as Action;
            this.callback = ExceptionDispatchInfo.Capture(e);
            c?.Invoke();
        }
    }
}