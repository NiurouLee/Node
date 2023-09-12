using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting.Dependencies.NCalc;

namespace Script.SuanFa
{
    /// <summary>
    /// Sort
    /// </summary>
    public class S_04
    {
        /// <summary>
        /// 选择排序
        /// </summary>
        /// <param name="inList"></param>
        public static void SelectSort(List<int> inList)
        {
            if (inList == null || inList.Count < 2)
            {
                return;
            }

            for (int i = 0; i < inList.Count - 1; i++)
            {
                var minIndex = i;
                for (int j = i + 1; j < inList.Count; j++)
                {
                    if (inList[j] < inList[minIndex])
                    {
                        minIndex = j;
                    }
                }

                Swap(inList, i, minIndex);
            }
        }


        /// <summary>
        /// 冒泡
        /// </summary>
        /// <param name="inList"></param>
        public static void BubbleSort(List<int> inList)
        {
            if (inList == null || inList.Count < 2)
            {
                return;
            }

            for (int i = inList.Count - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    var next = j + 1;
                    if (inList[next] < inList[j])
                    {
                        Swap(inList, next, j);
                    }
                }
            }
        }

        /// <summary>
        /// 只用一次循环的冒泡
        /// </summary>
        /// <param name="inList"></param>
        public static void BubbleSort2(List<int> inList)
        {
            if (inList == null || inList.Count < 2)
            {
                return;
            }

            int n = inList.Count, end = n - 1, i = 0;
            while (end > 0)
            {
                if (inList[i] > inList[i + 1])
                {
                    Swap(inList, i, i + 1);
                }

                if (i < end - 1)
                {
                    i++;
                }
                else
                {
                    end--;
                    i = 0;
                }
            }
        }


        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="inList"></param>
        public static void InsertSort(List<int> inList)
        {
            if (inList == null || inList.Count < 2)
            {
                return;
            }

            for (int i = 1; i < inList.Count; i++)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    if (inList[j + 1] < j)
                    {
                        Swap(inList, j + 1, j);
                    }
                }
            }
        }


        private static void Swap(List<int> inList, int inIndex1, int inIndex2)
        {
            if (inIndex1 < inList.Count && inIndex2 < inList.Count && inIndex1 > 0 && inIndex2 > 0)
            {
                (inList[inIndex1], inList[inIndex2]) = (inList[inIndex2], inList[inIndex1]);
            }
        }
    }
}