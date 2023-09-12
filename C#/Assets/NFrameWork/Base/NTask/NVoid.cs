using System;
using System.Runtime.CompilerServices;

namespace NFrameWork.NTask
{
    [AsyncMethodBuilder(typeof(AsyncNVoidMethodBuilder))]
    public struct NVoid
    {
        public void Coroutine()
        {
        }

        public bool IsCompleted => true;

        public void OnCompleted(Action continuation)
        {
        }

        public void UnsafeOnCompleted(Action continuation)
        {
        }
    }
}