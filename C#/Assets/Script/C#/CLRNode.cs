namespace Script
{
    /// <summary>
    /// CLR 执行模型
    /// </summary>
    public class CLRNode
    {
        
        /* 什么是CLR?
         * Common Language Runtime,可以多种语言使用的运行时，
         * 有什么功能?
         * 内存管理，程序集加载，安全性，异常处理，线程同步，异步
         * CLR 不关心语言，只要编译器是面向CLR的。
         * 编译器：微软有好几个编译器：c++/cli,C#,vb,f#,Iron Python,Iron Ruby,以及IL汇编器。
         * 无论那种语言编译器最后都会编译成为托管模块(中间语言和元数据)
         *
         * 什么是托管模块?
         * 是标准的32位Microsoft Windows 可移植执行体(PE32)文件，或者标准的64位windows可移植执行体(PE32+)文件，他们都需要CLR才能执行。
         * （托管程序总是利用window的数据保护和地址空间随机化来增强整个系统的安全性）
         *
         * 托管模块包含那些部分：
         * PE32或者PE32+头：
         * CLR头：这个模块成为托管模块的信息，Clr的版本，标志(Flag),托管模块的入口方法的MethodDef元数据token以模块的元数据，资源，强名称等
         * 元数据：一个描述源代码中定义的类型和成员的表，一个描述源代码引用的类型和成员的表。
         * IL(中间语言)代码：编译器编译源代码生成的代码，在运行时被CLR编译成本机CPU指令。
         *
         *本机代码编译器(Native code compilers)：
         * 生成的是特定CPU架构的代码
         *面向CLR的编译器生成的都是IL代码,(IL)代码有时称为托管代码，因为是CLR管理他的执行。
         *
         * 面向CLR的编译器需要生成那些东西：
         * 1.IL代码
         * 2.元数据(metadata):数据表集合：
         *  描述模块中定义了那些类型和成员，引用了那些类型和成员。元数据是一些老技术的超集
         *  老技术：类型库（Type library）,接口定义语言(Interface definition language,IDL)
         *  元数据需要与IL代码关联，通常都在EXE/DLL 文件中。
         *
         * 元数据的用处：
         * 1.避免了编译时对C/C++头和库文件的需求，因为在IL代码中已经有了所有的引用类型/成员信息。
         * 2.智能感知，写代码时智能提示用的就是元数据。
         * 3.CLR使用元数据来确保执行类型安全的。
         * 4.元数据允许将对象序列化到内存块，然后反序列化。
         * 5.元数据允许垃圾回收期跟踪对象的生存期，并且从元数据知道一个对象引用了那些其他对象。
         *
         *CLR不和托管模块工作，他之和程序集工作。
         *程序集参考程序集笔记。
         *
         * IL语言：
         *  比大多数CPU机器语言都高级，IL能访问和操作对象类型，并提供指令来创建和初始化对象，调用对象的虚方法以及直接操作数组元素。
         *  提供了抛出或者捕获异常的指令。 可以将IL视为面向对象的机器语言。
         * IL可以用汇编语言写,(为啥要用汇编写)
         * 高级语言通常之公开了CLR全部功能的一个子集。可以使用IL语言访问所有CLR的功能，
         *
         *
         *为了执行代码，必须把IL代码转换成(native) Cpu指令，这是CLR中JIT的职责。
         * 大致流程如下：
         * 1.当一个函数被调用时，在这个类型的程序集的元数据中找到被调用的方法，
         * 2.从元数据获取该方法的IL代码，
         * 3.分配一块内存，
         * 4.将IL编译成本机CPU指令，存储在3分配的内存中，
         * 5.在type表中修改与该方法对应的条目，然他指向内存3，
         * 6.跳转到内存块中的本机代码也就是3
         *
         *在执行Main方法之前，CLR会检测代码所引用的所有类型。
         * 然后CLR会分配一个内部结构(我猜是哈希表)来管理对引用类型的访问，在这个内部数据结构中，存储着所有引用类型的方法，每个方法对应一个
         * 记录项(entry),每个记录项都有一个地址，根据此地址可以找到方法的实现。
         * 对这个结构初始化时，CLR将每个记录项都设置为(指向CLR)包含在CLR内部的一个位编档函数，这个函数叫JITCompiler.
         * 
         *在首次调用一个方法时，JITCompiler 函数会被调用，JItCompiler函数负责将这个函数的IL代码编译成本机代码，因为是即时编译的所以通常叫JIT编译器。
         * JITCompiler 函数被调用时，因为他知道要调用的是哪个类型的那个方法，所以JITCompiler会在该类型的程序集的元数据中找到对用的IL代码。
         * 然后JITCompiler验证IL代码，将IL编译成CPU指令，然后将CPU指令存储到为CLR创建的内部数据结构中，找到被调用方法对应的记录，把记录指向的
         * 内存指向CPU指令那块内存，使其指向刚才编译好的cpu指令的地址。
         * 第二次执行时，因为已经有了记录所以直接调用本机的cpu指令。   这算不算是懒加载
         * 所以只有第一次会有一些性能损失，以后对该方法调用都是直接调用本机代码。
         * 但是当程序运行停止时，动态编译存储的指令就没了。即使一台机器同时运行两个应用，也无法共享。(用空间换取时间)
         *
         * 编译器开关设置：
         * optimize- ,在C#编译器生成的未优化,包含很多NOP指令(空操作)，但是可以用来断点等调试操作。 如果生成优化过的代码，这些空操作会被删除掉，提高性能且变小IL代码。
         * debug(+/full/pdbonly)开关，开启的话编译器才会生成program dataBase(PDB)文件，pdb帮调试器查找局部变量，并将IL指令映射到源码，
         * Release 就是关掉了debug。
         *
         * IL是基于栈的，所以所有的指令都要将操作数压入(push)一个栈，并从栈弹出。由于IL没有提供操作寄存器的指令，所以可以很容易的创建新的语言和编译器，面向CLR编程。
         * IL指令还是无类型(typeLess)的，例如Il提供的add指令将压入栈的两个操作数加到一起，Add指令不区分32位还是64位。Add指令执行是，他判断栈中两个操作数的类型，然后进行恰当的操作。
         * IL的优势是应用程序的健壮性和安全性，将IL编译成CPU指令时，CLR会进行一个验证的过程，这个过程会检查高级IL代码，确定代码所做的一切都是安全的，检查方法的参数，返回等。
         *
         * Windows的每一个进程都有自己的虚拟地址空间。独立地址空间的原因是：不能简单的信任一个应用程序的代码。应用程序完全可能读写无效的内存空间，所以将每个应用程序放到独立的地址空间
         * 一个进程不能去干另一个进程.(那外挂呢?)
         * 通过验证托管代码，可以确保代码不会访问不正确的内存空间，所以可以将多个托管应用程序放到同一个windows虚拟地址空间运行。
         * windows上进行需要消耗的资源太大，所以进程多的话资源利用率太高，所以用一个进程可以运行多个应用程序节省一些资源。
         * CLR中的每个托管应用程序都在一个APPDomain中.
         *
         *
         * 不安全的代码 unsafe
         * c#编译器默认生成的是safe的代码，这些代码的安全性可以验证，c#编译器也允许写unsafe的代码，不安全的代码允许直接操作内存地址，并操作这些地址的处的字节。
         * 编译器需要打开Unsafe编译开关。
         * 当JIT编译器编译一个unsafe代码时，会检查这个程序集是否被授予了System.Security.Permissions.SecurityPermission的权限。
         *
         * 本机代码生成器 NGen.exe
         * 使用NGen.exe可以在应用程序安装到计算机时，将IL代码编译成本机代码。由于代码在安装时已经编译好了， 所以CLR的JIT编译器不需要在运行时编译IL代码了，这可以提高程序的性能。
         * 1.提高应用程序的启动速度，因为不需要在JIT编译了。
         * 2.减少应用程序的工作集 （工作集：是指在进程的所有内存中，已映射的物理内存那一部分（即这些内存块全在物理内存中，并且CPU可以直接访问。）进程还有一部分虚拟内存，他们可能在转换列表中，
         * CPU不能通过虚地址访问，需要window映射之后才能访问）还有一部分内存在磁盘上的分页文件里。
         *  如果一个程序集同时加载到多个进程中，对程序集进行NGen.exe可以减少应用程序的工作集，因为已经将这些IL直接编译成本机代码存储到单独的文件中，这些文件通过内存映射的方式共享。
         *  CLR加载程序集文件时，先找有没有对应的NGen.exe生成的本机文件，有就不用JIT，否则就JIT运行。
         * NGen生成的文件可能因为某些原因失去同步，CLR版本，CPU型号改变，
         * 较差的执行时性能，NGen.exe 生成的代码没有运行时JIT生成的效率高。
         *
         * Framework 类库 （Framework class library,FCL）
         * 常用的命名空间
         * System                                    包含常用的基本类型
         * System.Data                               数据库通信以及处理数据的类型
         * System.IO                                 I/O一浏览目录/文件的类型
         * System.Net                                低级网络通信，以及常用的Internet协议协作类型 
         * System.Runtime.InteropServices            允许非托管代码
         * System.Security                           保护数据和资源类型
         * System.Text                               编码文本类型
         * System.Threading                          异步
         * System.Xml                                XML操作
         * 
         *
         *通用类型系统
         * CLR一切都围绕类型展开，通过类型，用一种编程语言写的代码能与另一种编程语言写的代码沟通。
         * CLR规定一一种通用类型系统(Common Type System，CTS)。
         * CTS规范：
         * 字段(Field) 作为对象状态一部分的数据变量，字段根据名称和类型来区分。
         * 方法(Method)针对对象执行操作的函数，通常会改变对象的状态，方法需要有方法名称，签名，修饰符，参数数量，参数类型，返回值，返回值类型。
         * 属性(property) 属性算是对字段的一种封装，但可以校验输入参数以及延迟计算返回值。
         * 事件(Event) 一种通知机制。
         *
         * 类型的可见性规则和访问规则
         * private
         * family(protected)
         * assembly(internal)
         * public 
         *
         *
         * 与非托管代码的互操作性
         * 1.托管代码调用DLL中的非托管函数
         * P/Invoke(platform Invoke)机制调用dll中的函数，
         * 2.非托管代码使用现有的com组件
         * 3.非托管代码使用托管类型
         * 可以用c#创建ActiveX空间或者Shell扩展。
         *
         * 
         */
    }
}