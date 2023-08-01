using System;
using System.Runtime.CompilerServices;

namespace NFrameWork.NTask
{
    public class AsyncNVoidMethodBuilder
    {
        /// <summary>
        /// 1.Static Create Method.
        /// </summary>
        /// <returns></returns>
        public static AsyncNVoidMethodBuilder Create()
        {
            AsyncNVoidMethodBuilder builder = new AsyncNVoidMethodBuilder();
            return builder;
        }


        /// <summary>
        /// 2.TaskLike Task property(void)
        /// </summary>
        public NVoid Task => default;


        /// <summary>
        /// 3.SetException
        /// </summary>
        /// <param name="e"></param>
        public void SetException(Exception e)
        {
            NTask.ExceptionHandler.Invoke(e);
        }

        /// <summary>
        /// 4.SetResult
        /// </summary>
        public void SetResult()
        {
            //do nothing
        }


        /// <summary>
        /// 5.AwaitOnCompleted
        /// </summary>
        /// <param name="awaiter"></param>
        /// <param name="stateMachine"></param>
        /// <typeparam name="TAwaiter"></typeparam>
        /// <typeparam name="TStateMachine"></typeparam>
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
        {
            awaiter.OnCompleted(stateMachine.MoveNext);
        }


        /// <summary>
        ///  6.AwaitUnsafeOnCompleted
        /// </summary>
        /// <param name="awaiter"></param>
        /// <param name="stateMachine"></param>
        /// <typeparam name="TAwaiter"></typeparam>
        /// <typeparam name="TStateMachine"></typeparam>
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter,
            ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
        {
            awaiter.OnCompleted(stateMachine.MoveNext);
        }


        /// <summary>
        /// 7.Start
        /// </summary>
        /// <param name="stateMachine"></param>
        /// <typeparam name="TStateMachine"></typeparam>
        public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
        {
            stateMachine.MoveNext();
        }


        /// <summary>
        /// </summary>
        /// <param name="stateMachine"></param>
        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
        }
    }
}