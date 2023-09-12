using System;
using System.Runtime.CompilerServices;

namespace NFrameWork.NTask
{
    [AsyncMethodBuilder(typeof(AsyncNTaskCompletedMethodBuilder))]
    public class NTaskCompleted : ICriticalNotifyCompletion
    {
        public NTaskCompleted GetAwaiter()
        {
            return this;
        }

        public bool IsCompleted => true;


        public void GetResult()
        {
        }

        public void OnCompleted(Action continuation)
        {
        }

        public void UnsafeOnCompleted(Action continuation)
        {
        }
    }
}