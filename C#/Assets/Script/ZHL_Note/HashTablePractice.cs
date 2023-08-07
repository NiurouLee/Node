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

        #region 拆装箱

        //由于用object来存储数据，自然存在装箱拆箱
        //当往其中进行值类型存储时就是在装箱
        //当将值类型对象取出来转换使用时，就是在拆箱

        #endregion

    }
}
