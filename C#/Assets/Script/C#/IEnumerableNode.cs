using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 迭代器笔记
/// </summary>
public class IEnumerableNode
{
    //迭代器是包含迭代器块的方法或者属性。
    //迭代器块本质上是包含yield return 或者 yield break的代码
    // IEnumerable
    // IEnumerable<T>  
    // IEnumerator
    // IEnumerator<T> 
    // 根据迭代器类型的返回类型，每个迭代器都有一个生成类型，如果不是泛型迭代器，则返回Object。
    //  如果是泛型类型，则返回泛型类型的对象。
    // yield return 语句用于返回序列的各个值， yield break 用于终止返回序列
    // 相比于List<T>迭代器是延迟
    //-------------------------------------
    // 延迟执行（延迟计算）属于Lambda 演算的一部分： 只在需要获取计算结果时才执行代码。
    // IEnumerable 是可用以迭代的序列，IEnumerator 像一个游标一样。
    // IEnumerable 可以同时被多个IEnumerator 遍历，并且不会改变IEnumerable 的状态
    // IEnumerator 本事就是多状态的，每次调用MoveNext,当前游标都会向前移动一个元素
    // 遇到yield return , Current 属性会赋予此值，然后MoveNext 会返回true  如果返回false则代表是移动到了最后一个元素，无法继续往下
    // 返回Ture后程序暂时挂起，直到执行下一次MoveNext
    //-------执行Yield语句--------------------
    // 如果遇到yield break 或者执行到了程序末尾，MoveNext就会返回false;
    // 如果遇到了return 则准备current MoveNext返回True,

    // 在迭代器中中添加try cash finally 最后一次 或者 dispose的时候会执行Finally
    // Foreach  隐含执行一个using ，在调用Dispose的时候，会调用一次Finally 方法
    // 如果使用while(enumerator.MoveNext()) 需要调用using  保证IEnumerator 的 dispose 能够正确被调用
    static void Test()
    {
        
 
        foreach (var item in CreateSimpleIterator())
        {
            UnityEngine.Debug.Log(item);
        }

        //延迟计算演示：
        IEnumerable<int> enumerable = CreateSimpleIterator();
        //执行到这里的时候，方法体中的代码还没执行
        using (IEnumerator<int> enumerator = enumerable.GetEnumerator())
        {
            //以上的时候方法体内的代码都没执行呢
            //开始执行moveNext 方法才执行
            while (enumerator.MoveNext())
            {
                int value = enumerator.Current;
                Debug.Log(value);
            }
        }


        IEnumerable<int> enumerable1 = Fibonacci();
        using (IEnumerator<int> enumerator1 = enumerable1.GetEnumerator())
        {
            while (enumerator1.MoveNext() || enumerator1.Current < 1000)
            {
                int value = enumerator1.Current;
                Debug.Log(value);
            }
        }
    }

    
    public class ObseverDicOjb<K,V>: System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<K, V>>
    {
        public Dictionary<K, V> Dictionary;
        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            return (IEnumerator<KeyValuePair<K,V>>)Dictionary;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


    static IEnumerable<int> CreateSimpleIterator()
    {
        yield return 10;
        for (int i = 0; i < 10; i++)
        {
            yield return i;
        }

        yield break;
    }

    /// <summary>
    /// 迭代器实现斐波那契
    /// </summary>
    /// <returns></returns>
    static IEnumerable<int> Fibonacci()
    {
        int current = 0;
        int next = 1;
        while (true)
        {
            yield return current;
            int oldCurrent = current;
            current = next;
            next = next + oldCurrent;
        }
    }

    // 迭代器实现机制
    public class Enumerator : IEnumerator
    {
        public bool MoveNext()
        {
            throw new System.NotImplementedException();
        }

        public void Reset()
        {
            throw new System.NotImplementedException();
        }

        public object Current { get; }
    }


    public class Enumerable : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            return new Enumerator();
        }
    }

    public class StateMachine : IEnumerable, IEnumerator
    {
        public IEnumerator GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        public bool MoveNext()
        {
            throw new System.NotImplementedException();
        }

        public void Reset()
        {
            throw new System.NotImplementedException();
        }

        public object Current { get; }
    }


    // 编译器会把方法编译为一个状态机，然后复制参数，把状态机实例返回给调用对象，
    // 原始的方法中的代码被分裂到各个状态中，延迟调用的由来，
    // 每次MoveNext 都会跳转到下一个状态，
    // 状态机同时实现了IEnumerator 和 IEnumerable
    // GetEnumerator 方法检查状态机，并且返回this
    // 状态机中包含一个变量，用来标识当前执行的状态，
    // Roslyn 为例：
    // -3：MoveNext() 当前正在执行
    // -2：GetEnumerator() 尚未被调用
    // -1：执行完成（无论成功与否）
    // 0 ：GetEnumerator() 已经被调用，但是MoveNext() 还未被调用
    // 1 ：在第一条Yield return 语句
    // 2 ：在第二条yield return 语句
}