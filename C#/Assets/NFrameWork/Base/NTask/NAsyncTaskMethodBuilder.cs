using System;
using System.Runtime.CompilerServices;

namespace NFrameWork.NTask
{
    public struct NAsyncTaskMethodBuilder
    {
        private NTask tcs;

        /// <summary>
        /// 1.Static Create Method
        /// </summary>
        /// <returns></returns>
        public static NAsyncTaskMethodBuilder Create()
        {
            NAsyncTaskMethodBuilder builder = new NAsyncTaskMethodBuilder() { tcs = NTask.Create(true) };
            return builder;
        }

        /// <summary>
        /// 2.TaskLike Task Property.
        /// </summary>
        public NTask Task => tcs;


        /// <summary>
        /// 3.SetException
        /// </summary>
        /// <param name="exception"></param>
        public void SetException(Exception exception)
        {
            this.tcs.SetException(exception);
        }


        /// <summary>
        /// 4.SetResult
        /// </summary>
        public void SetResult()
        {
            this.tcs.SetResult();
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
        /// 6.AwaitUnsafeOnCompleted
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
        /// 8.SetStateMachine
        /// </summary>
        /// <param name="stateMachine"></param>
        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
        }
    }


    public struct NAsyncTaskMethodBuilder<T>
    {
        private NTask<T> tcs;

        /// <summary>
        /// 1.Static Create Method 
        /// </summary>
        /// <returns></returns>
        public static NAsyncTaskMethodBuilder<T> Create()
        {
            NAsyncTaskMethodBuilder<T> builder = new NAsyncTaskMethodBuilder<T>() { tcs = NTask<T>.Create(true) };
            return builder;
        }


        /// <summary>
        /// 2.TaskLick Task Property
        /// </summary>
        public NTask<T> Task => tcs;


        /// <summary>
        /// 3.SetException
        /// </summary>
        /// <param name="exception"></param>
        public void SetException(Exception exception)
        {
            this.tcs.SetException(exception);
        }


        /// <summary>
        /// 4.SetResult
        /// </summary>
        /// <param name="ret"></param>
        public void SetResult(T ret)
        {
            this.tcs.SetResult(ret);
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
        /// 6.AwaitUnsafeOnCompleted
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
        /// 8.SetStateMachine
        /// </summary>
        /// <param name="stateMachine"></param>
        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
        }
    }
}