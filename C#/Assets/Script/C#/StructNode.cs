using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 值类型和引用类型
/// </summary>
public class StructNode : MonoBehaviour
{
    // private int a = new System.Int32();
    // private System.Int32 a=0;
    // private int a = new int();
    // private int a = 0;
    /*基元类型：
     * 编译器直接支持的类型称为基元类型。例如int.上述代码生成的IL代码完全一样
     * C# 提供的基元类型有
     * sbyte 有符号8位
     * byte  无符号8位
     * short 有符号16位
     * ushort无符号16位
     * int   有符号32位
     * uint  无符号32位
     * long  有符号64位
     * ulong 无符号64位
     * char  16位Unicode字符
     * float 32位 IEEE 浮点
     * double64位 IEEE 浮点
     * bool
     * decimal 128位高精度浮点
     * string 字符串组
     * object 所有类型的基类型
     * dynamic  和object 一致
     */
    
    /*引用类型和值类型
     * 引用类型总是从托管堆分配内存，使用new 关键字返回对象的内存地址。
     * 1.内存必须从托管堆分配。
     * 2.堆上分配的每个对象都有一些额外成员，这些成员必须初始化。（类型对象指针，同步索引块）
     * 3.对象中的其他字节（为字段而设）总是设置为0。 c 中为垃圾值
     * 4.从托管堆中分配对象时，可能会执行一次垃圾回收。
     *
     *
     * 值类型：
     * 值类型实例“一般”在线程栈中分配（虽然也可能作为字段嵌入到引用类型中）
     * 在代表值类型实例的变量中不包含指向实例的指针，变量中包含的是实例本身的字段。
     * 值类型的对象有两种表示：未装箱和已装箱，引用类型总是处于已装箱形式。
     *
     * 值类型了装箱和拆箱
     * 如果使用非泛型容器ArrayList.Add()方法添加一个struct 那么必然会将值类型装箱为引用类型，因为Add()方法的参数是Object。
     * 装箱操作：
     * 1.在托管堆中分配内存，分配的内存量是值类型个字段所需的内存量，还要加上托管堆上所有对象都必须有的两个额外成员（类型对象指针和同步索引块）。
     * 2.值类型的字段复制到新分配的堆内存。
     * 3.返回对象地址。
     *
     * 有哪些操作会进行装箱：
     * 1.GetType() 因为GetType方法是从System.Object 继承的，所以为了调用GetType,CLR必须指向类型对象的指针，这个指针必须通过装箱来得到。
     * 2.转为ICompareTo 接口，因为接口被定义为引用类型。
     * 
     */
    
    
    /*
     * CLR中如何控制类型中的字段分布：
     * System.Runtime.InteropServices.StructLayoutAttribute.
     * 默认引用类型使用Auto分布，值类型使用Sequential分布。
     */
    
}