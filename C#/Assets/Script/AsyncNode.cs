using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using NFrameWork.NTask;
using UnityEngine;




public class MyAsync : IEnumerator
{
    private IEnumerator _enumeratorImplementation;

    public MyAsync()
    {
    }


    public MyAwaiter2 GetAwaiter()
    {
        return new MyAwaiter2();
    }


    public bool MoveNext()
    {
        throw new NotImplementedException();
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }

    public object Current { get; }
}

public class MyAsync2
{
    public MyAwaiter2 GetAwaiter()
    {
        return new MyAwaiter2();
    }
}


public class MyAwaiter : INotifyCompletion
{
    public bool IsCompleted { get; }

    public void GetResult()
    {
    }

    public void OnCompleted(Action continuation)
    {
    }
}

public class MyAwaiter2 : ICriticalNotifyCompletion
{
    public void OnCompleted(Action continuation)
    {
        throw new NotImplementedException();
    }

    public void UnsafeOnCompleted(Action continuation)
    {
        throw new NotImplementedException();
    }

    public void GetResult()
    {
    }

    public bool IsCompleted { get; }
}

public static class Test
{
    static async NTask Main()
    {
    }
}


public class AsyncNode : MonoBehaviour
{
    /*异步问题:
     * 需要异步的原因：解决线程因为等待独占式任务而导致的阻塞问题，
     * 现在的解决问题是await  async  基于并行库，
     *  异步函数：使用async 修饰的函数，它可以对await 表达式使用await 运算符。
     *  如果await 表达式所做的任务尚未完成，异步函数将会立即返回，当表达式的值可用后，
     * 代码将从之前的的位置（在恰当的线程中）恢复执行，
     *
     *
     *
     *
     * 可等待模式：
     * 用于判别那些类型可以使用await 运算符，
     * 可等待模式不是基于接口表达的（例如using 必须要求 using 对象继承IDispose 接口），
     * 可等待模式是基于模式实现的:
     * 假设一个返回类型为T的表达式需要使用await ,那么编译器会进行以下检查：
     * 1.T 必须具有一个无参数的GetAwaiter()实例方法，或者存在T的扩展方法，改方法以类型T作为唯一参数。
     *      GetAwaiter 方法不能是void了，其返回类型称为Awaiter类型。
     * 2.awaiter 类型必须实现System.Runtime.INotifyCompletion接口，该接口只有一个方法：void OnCompleted(Action)
     * 3.awaiter 类型必须具有一个可读的实例属性 IsCompletion,类型为bool。
     * 4.awaiter 类型还必须有一个非泛型的，无参数的实例方法GetResult。
     * 
     *
     * 
     */
}