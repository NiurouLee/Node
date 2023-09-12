using NFrameWork.Collections;
using UnityEngine;

namespace Script.SuanFaDaolun
{
    /// <summary>
    /// 排序和顺序统计量
    /// 堆排序 HeapSort 时间复杂度是O(nlogn)
    /// </summary>
    public class Unit6
    {
        /*排序和顺序统计量
         *为什么要排序？
         *  排序是算法研究中最基础的问题。
         * 插入 O(n*n)
         * 归并 O(nlogn)
         * 堆排序O(nlogn)
         * 快排 O(nlogn) 最坏情况 O(n*n)
         * 
         * 
         */

        /*堆可以认为是一棵近似完全二叉树，树上的每个节点对应数组中的一个元素，除了最底层外，该树是完全充满的。
         * 需要引入一个Size来管理数组对应树的问题。
         *堆排序相是基于原地址的，亦可以用于构建优先队列数据结构。
         * 假设一个节点的索引为i,那么左子节点为2i+1,右子节点为2i+2,父节点为 floor.((i-1)/2)
         * 如果一棵树的总结点为n，那么树的高度为logn,堆结构的基本操作与堆的高度成正比，即O(lgn).
         *
         * MAX-HEAPIFY过程：其时间复杂度为O(lgn),他是维护最大堆性质的关键。
         * BUILD-MAX-HEAP过程，具有线性时间复杂度，功能是从无序的输入数组中构建一个最大堆。
         * HEAPSORT过程：时间复杂度为O(nlgn),功能是对一个数组进行原址排序。
         *
         *
         *维护堆的性质
         * MAX-HEAPIFY 是用于维护最大堆性质的重要过程，输入一个数组A，和一个下标i。
         * 思路：每次从A[i]和A[Left(i)]和A[Right(i)]中选择出最大的，然后交换i和较大的那个索引的值，然后递归执行i当前所在的索引位置
         *
         *
         *大根堆：
         * 要求任何一个子结构，最大时都得在顶部。
         * 向上调整
         * 如果比父大，就交换（递归）
         *
         * 向下调整
         * 如果比子小就（跟较大的那个）交换（递归）
         * 
         */


        /// <summary>
        /// 向上调整
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="i"></param>
        public static void HeapInsert(int[] arr, int i)
        {
            while (arr[i] > arr[(i - 1) / 2])
            {
                SuanFaHelper.Swap(arr, i, i - 1 / 2);
                i = (i - 1) / 2;
            }
        }


        /// <summary>
        /// 向下调整
        /// </summary>
        public static void HeapIfy(int[] inArr, int i, int inSize)
        {
            int l = (2 * i) + 1;
            while (l < inSize)
            {
                int best = l + 1 < inSize && inArr[l + 1] > inArr[l] ? l + 1 : l;
                best = inArr[best] > inArr[i] ? best : i;
                if (best == i)
                {
                    return;
                }

                SuanFaHelper.Swap(inArr, i, best);
                i = best;
                l = (best * 2) + 1;
            }
        }


        /*
         * 堆排序
         * 1.先变成大根，确保最大值在0位置。
         * 2.把最大值与最有一个交换，然后整个Size-1，这样保证了最后一个就是最大值，并且后续不参与排序过程。
         * 3.接着向下调整，保证大根堆的逻辑，然后递归2。
         */

        public static void HeapSort(int[] inArr)
        {
            int n = inArr.Length;
            //先构建一个大根堆
            for (int i = 0; i < n; i++)
            {
                HeapInsert(inArr, i);
            }

            int size = n;

            while (size > 1)
            {
                //交换最后一个与最大值
                SuanFaHelper.Swap(inArr, 0, --size);
                //向下调整,保证大根堆
                HeapIfy(inArr, 0, size);
            }
        }
    }
}