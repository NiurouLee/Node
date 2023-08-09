using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashTablePractice : MonoBehaviour
{

    #region HashTable
    //HashTable 又称散列表 是基于键的哈希代码组织起来的键/值对
    //它的主要作用是提高数据查询的效率
    //可以使用键来快速访问集合中的元素

    #endregion

    void Start()
    {
        #region 一、声明

        Hashtable hashTable = new Hashtable();

        #endregion

        #region  二、增加

        //增加  hashTable.Add(Key,Value)
        //不能出现相同的Key
        hashTable.Add("Name", "Zhuhongli");
        hashTable.Add("Age", 22);
        hashTable.Add(007, "上班时间");
        hashTable.Add(true, false);

        #endregion

        #region  三、删除

        //删除  hashTable.Remove(Key)
        //1.Remove中填对应的Key值 Remove后会删除对应的Key和值
        //2.Remove没有的Key 没有反应 不会报空
        //3.清除所有的key 调用Clear()方法

        hashTable.Remove("Name");
        hashTable.Remove(007);
        hashTable.Remove(true);
        hashTable.Clear();

        #endregion

        #region 四、查找/获取

        //1.通过键来获得值
        var Name = hashTable["Name"];
        var Age = hashTable["age"];
        var buer = hashTable[true];
        //如果获取不到值 那么会返回空
        var value = hashTable[1231];  //bull  

        //2.判断是否有这个键/值对
        hashTable.Contains(1);       //通过键来判断
        hashTable.ContainsKey(1);    //通过键来判断
        hashTable.ContainsValue(1);  //通过值来判断

        //注意不能通过值来找对应的键

        #endregion

        #region 五、改

        //类似于数值的直接赋值
        hashTable["Name"] = "BuXiangJiaBan";

        #endregion

        #region 六、遍历

        //获取键/值对数  但是不能用For循环得到所有的键值对 因为key不确定
        int hashTbaleCount = hashTable.Count;

        //遍历hashTable的键或值
        foreach(object item in hashTable.Keys)
        {
            Debug.Log("键：" + item);
            Debug.Log("值：" + hashTable[item]);
        }

        //遍历hashTable的键和值
        foreach (DictionaryEntry item in hashTable)
        {
            Debug.Log("键：" + item.Key);
            Debug.Log("值：" + item.Value);
        }

        //通过迭代器遍历 （复盘）
        IDictionaryEnumerator myEnumerator = hashTable.GetEnumerator();
        bool flag = myEnumerator.MoveNext();
        while (flag)
        {
            Debug.Log("键：" + myEnumerator.Key + "值：" + myEnumerator.Value);
            flag = myEnumerator.MoveNext();
        }

        #endregion

        #region 七、拆装箱

        //由于用object来存储数据，自然存在装箱拆箱
        //当往其中进行值类型存储时就是在装箱
        //当将值类型对象取出来转换使用时，就是在拆箱

        #endregion

        #region 八、Hash算法理论

        //哈希地址： 哈希函数——>逻辑地址   f(key)——>Address

        //哈希函数的定义方式：

        //1.取余法： f(key) = key%p  (p为数组的最大容量 可以理解为hash表中的空位)  <最常用的方法>
        //如果知道 Hash 表的最大长度为 m，可以取不大于m的最大质数 p，然后对关键字进行取余运算，address(key)=key % p。这里 p 的选取非常关键，p 选择的好的话，能够最大程度地减少冲突，p 一般取不大于m的最大质数。
        //Hash表的容量过大 容易浪费空间 容量过小容易产生冲突
        //Hash 表大小确定通常有这两种思路：
        //如果最初知道存储的数据量，则需要根据存储个数 和 关键字的分布特点来确定 Hash 表的大小。
        //事先不知道最终需要存储的记录个数，需要动态维护Hash表的容量，此时可能需要重新计算 Hash 地址。

        //2.平方取中法
        //对关键字进行平方计算，取结果的中间几位作为 Hash 地址。如有以下关键字序列 { 421，423，436} ，平方之后的结果为 { 177241，178929，190096} ，那么可以取中间的两位数 { 72，89，00} 作为 Hash 地址。

        //3.直接定址法
        //取关键字或者关键字的某个线性函数为 Hash 地址，即address(key) = a * key + b; 如知道学生的学号从2000开始，最大为4000，则可以将address(key) = key - 2000(其中a = 1)作为Hash地址。

        //4.折叠法
        //将关键字拆分成几部分，然后将这几部分组合在一起，以特定的方式进行转化形成Hash地址。如图书的 ISBN 号为 8903 - 241 - 23，可以将 address(key)= 89 + 03 + 24 + 12 + 3 作为 Hash 地址。

        #endregion

        #region 八、Hash冲突

        //Hash叫做”散列表“，就是把任意长度的输入，通过散列算法，变成固定长度输出，该输出结果是散列值。
        //其实这种转换是一种压缩映射，散列表的空间通常小于输入的空间，不同的输入可能会散列成相同的输出，所以不能从散列表来唯一的确定输入值。这就出现了Hash冲突。
        //Hash冲突：
        //根据key（键）即经过一个函数f(key)得到的结果的作为地址去存放当前的key value键值对(这个是hashmap的存值方式)，但是却发现算出来的地址上已经被占用了。这就是所谓的hash冲突。

        //解决Hash冲突
        //1.开放定址法
        //细分：(1).线性探查法：如果当前单元被占用了 依次判断下一个单元是否被占用 直到找到空闲单元或者遍历所有单元为止 
        //(2).平方探查法：从发生冲突的单元起 加上f(key)+1的平方 f(key)+2的平方...
        //(3).双散列函数探查法：两个函数h1和h2 以关键字为自变量 h1产生一个从0到m-1之间的数作为散列地址 h2产生一个从1到m-1且和m互素(不能被m整除)的数作为探查地址的步长增量
        //(4).伪随机探查法：假设f(69)=3 冲突了 这时候我定义一个伪随机数队列 2 5 9  那么下一个散列地址为 f(3+2)=3 假设还是冲突了 那么 f(3+5)=1 

        //2.链地址法(拉链法)
        //将所有哈希地址为i的元素构成一个称为同义词链的单链表，并将单链表的头指针存在哈希表的第i个单元中，因而查找、插入和删除主要在同义词链中进行。链地址法适用于经常进行插入和删除的情况。

        //3.再Hash法
        //这种方法就是同时构造多个不同的哈希函数： Hi = RH1（key）  i = 1，2，…，k。当哈希地址Hi = RH1（key）发生冲突时，再计算Hi = RH2（key）……，直到冲突不再产生。这种方法不易产生聚集，但增加了计算时间。

        //4.建立公共溢出区
        //这种方法的基本思想是：将哈希表分为基本表和溢出表两部分，凡是和基本表发生冲突的元素，一律填入溢出表。

        #endregion




    }
}
