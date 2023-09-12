using System;
using System.Reflection;
using UnityEngine;

namespace Script
{
    [AttributeUsage(AttributeTargets.Struct)]
    public class TestStructAttribute : Attribute
    {
        public int Index;

        public TestStructAttribute(int inIndex)
        {
            Index = inIndex;
        }
    }

    [TestStruct(1)]
    public struct TestMyStruct
    {
        public int Num;

        public TestMyStruct(int inNum)
        {
            Num = inNum;
        }
    }


    /// <summary>
    /// 特性
    /// </summary>
    public class AttributeNode
    {
        /*
         * 思考：如果给一个struct 标记一个Attribute,获取该Attribute时会不会拆装箱
         * 拿到上述Struct的attribute
         * 
         */

        private void Test()
        {
            //?这里通过assembly获取到所有的Type对象不会装箱，但是干嘛了呢？
            var types = this.GetType().Assembly.GetTypes();
            foreach (var type in types)
            {
                foreach (var att in type.GetCustomAttributes())
                {
                    if (att is TestStructAttribute myStruct)
                    {
                        Debug.Log(myStruct.Index);
                    }
                }
            }
        }
    }
}