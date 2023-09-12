using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Script.SuanFaDaolun
{
    /// <summary>
    /// 算法导论第二章
    /// </summary>
    public class Unit2
    {
        /*插入排序问题：
         * 对于少量的元素他是一个有效的算法，插入排序的方式像抓牌的过程，每次到来一个新元素时，与当前已经有序的部分的最后一个元素进行对比，
         * 如果当前有序的最后一位大于到来的新元素，那么最后一位后移，接着与倒数第二位进行比较，知道找到该元素应在的位置，进行插入，以此类推
         * 直至整个集合有序。
         */

        public void InsertSort(int[] inArr)
        {
            if (inArr == null || inArr.Length <= 1)
            {
                return;
            }

            for (int i = 1; i < inArr.Length; i++)
            {
                var j = i - 1;
                var currentItem = inArr[i];
                while (j >= 0 && inArr[j] > currentItem)
                {
                    inArr[i] = inArr[j];
                    j--;
                }

                inArr[j + 1] = currentItem;
            }
        }


        /*
         * 循环不变式：在循环体每次执行前后均为真。
         * 性质:
         *  初始化:循环的第一次迭代之前，他为真。
         *  保持:如果循环的某次迭代之前他为真，那么下次迭代之前他仍为真。
         *  终止:当循环终止时，不变式为我们提供了一个有用的性质，该性质有助于证明算法是正确的。
         */

        /*分析算法
         * 预测算法的结果和预测算法消耗的资源，通常度量的是时间。
         * 一般的，算法需要的时间与输入的规模同步增长，所以通常把一个程序的运行时间描述成其输入规模的函数。
         * 输入规模：输入中的项数，在排序中就是待排序数组的规模n。 可以根据问题来规定输入规模的度量。
         * 算法的运行时间是指执行的基本操作的数或部数，虽然每台计算器执行一步的时间是不一样的，但是用步可以控制变量。
         * 一般描述一个算法运行时间，一般指最差情况，因为最差情况决定了这个算法的上限。
         * 算法关注的是运行时间的增长率和增长量级，所以只考虑算法时间公式中最重要的项。
         */


        /*
         * 分治法：
         * 许多有用的算法在结构上是递归的，为了解决一个给定的问题，算法一次或多次递归的调用其自身以解决紧密相关的若干子问题。
         * 将原问题分解为几个规模较小的但是类似的子问题，递归的求解这些子问题，然后再合并这些子问题的解来建立原问题的解。
         * 分治法的三个步骤：
         * 1.分解原问题为若干子问题，子问题是原问题的规模较小的实例。
         * 2.解决这些子问题，递归的求解各子问题，然而，如果子问题的规模足够小，则直接求解。
         * 3.合并这些子问题的解为原问题的解。
         */


        /*
         * 归并排序：
         * 1.分解：分解待排序的n个元素的序列成各具有你n/2个元素的两个序列。
         * 2.解决：使用归并排序递归的排序两个子序列。
         * 3.合并：合并两个已经排序的子序列以产生的以排序的子序列。
         *
         * 当子序列长度为1时，递归 开始回升，这种情况下，不用做任何工作，因为长度为1的子序列就是有序的。
         * 归并操作的核心操作是合并，通过一个函数Merge(A,p,q,r) A 表示一个数组，p,q,r都是数组的下标，满足p<=q<r.
         * 该过程假设A[p..q]和A[q+1,r] 都已经拍好序，合并后A[p..r]有序.
         * 合并过程需要n的时间，n=r-p+1,是待合并元素的总数。
         * 合并的过程还是跟玩扑克牌一样，假设现在有两堆已经有序的扑克，
        */
        public void MergeSort(int[] inArr)
        {
            if (inArr.Length <= 1 && inArr == null)
            {
                return;
            }

            _MergeSort(inArr, 0, inArr.Length - 1);
        }

        private void _MergeSort(int[] inArr, int l, int r)
        {
            if (inArr == null || l == r)
            {
                return;
            }

            int m = l + ((r - l) << 1);
            _MergeSort(inArr, l, m);
            _MergeSort(inArr, m + 1, r);
            Merge(inArr, l, m, r);
        }


        /// <summary>
        /// 合并两个部分
        /// </summary>
        /// <param name="inArr">数组</param>
        /// <param name="p">左</param>
        /// <param name="q">中间</param>
        /// <param name="r">右</param>
        private void Merge(int[] inArr, int p, int q, int r)
        {
            int n1 = q - p + 1;
            int n2 = r - q;
            var L = new int[n1];
            var R = new int[n2];
            for (int i = 0; i < n1; i++)
            {
                L[i] = inArr[p + i];
            }

            for (int i = 0; i < n2; i++)
            {
                R[i] = inArr[q + i + 1];
            }

            int ii = 1;
            int jj = 1;
            for (int k = p; k < r; k++)
            {
                if (ii >= L.Length)
                {
                    inArr[k] = R[jj];
                    jj++;
                    continue;
                }

                if (jj >= R.Length)
                {
                    inArr[k] = L[ii];
                    ii++;
                    continue;
                }

                if (L[ii] <= R[jj])
                {
                    inArr[k] = L[ii];
                    ii++;
                }
                else
                {
                    inArr[k] = R[jj];
                    jj++;
                }
            }
        }

        /*算法证明：
         * 证明上述for循环的循环不变式成立，在每次循环之前，L和R都是有序的。
         * 初始化:k=p，ii==jj==1,L[ii]和R[jj]都是各自所在数组中未被复制回数组a的最小元素。
         * 保持：每次迭代都要能保持循环不变式成立，假设L的第一个元素小于R的第一个元素，所以第一次循环执行inArr[k]=L[ii];
         *      inArr[p..k]范围上是有序的,然后执行ii++，重新建立了循环不变式。
         * 终止：当k=r时，根据循环不变式，数组Arr[p..k]都是有序的。
         */

        /*算法分析:
         * 整个Merge的过程的元素共有r-p+1个，假设时间复杂度0(n);
         * 复制数组的时间复杂度为O(n);
         * for循环有n次，时间复杂度还是O(n);
         * Merge知识对一个子数组进行操作的过程，那么整个数组有多少个子数组呢？
         * 
         */

        /*分治算法分析:
         * 当一个算法包含对其自身递归调用时，可以用递归方程和递归式来描述运行时间，递归方程在较小的输入上的运行时间来描述规模为n的问题上的总时间，然后利用
         * 数学工具来求解递归式并给出算法性能的界。
         *分治算法运行时间的递归式来自基本模式的三个步骤：
         * 假设T(n)是规模为n的一个问题的运行时间，若问题的规模足够小，如对某个常量从c,n<=c,则直接求解需要常量时间，我们将其写作0(1).
         * 假设我们把原问题分解为a个子问题，每个子问题的规模是原问题的1/b,对归并排序，a==b==2,但是有些分治算法中a!=b,所以求解一个规模为n/b的子问题，需要T(n/b)的时间，
         * 所以我们需要aT(n/b)的时间来求解a个子问题。
         * 假设分解成子问题时间为D(n),合并子问题的解成原问题的解需要时间C(n),那么得到递归式：
         * T(n)=aT(n/b)+D(n)+C(n);
         */

        /*归并算法分析：
         * 假设原问题的规模为2的幂，那么基于递归式的分析被简化，这是分解步骤产生的规模刚好为n/2的两个子序列。
         * T(n)=2T(n/2)+0(n);
         * 递归树: 一共会有lgn+1层，每层贡献总代价为cn（第一层为n，第二层为c(n/2)+c(n/2)...类推之最后一层，共有n个叶子节点，加起来仍是n）,所以总代价为cn lgn +cn , 就是0(n lgn)
         * 最后忽略常数项即为O(nlgn);
         */

        public static int[] arr = new int[int.MaxValue];
        public static int[] help = new int[int.MaxValue];

        /// <summary>
        /// 递归版
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        public void MergeSort1(int l, int r)
        {
            if (l == r)
            {
                return;
            }

            int m = (l + r) / 2;
            MergeSort1(l, m);
            MergeSort1(m + 1, r);
            Merge1(l, m, r);
        }

        /// <summary>
        /// 非递归版
        /// </summary>
        public void MergeSort2()
        {
            var n = arr.Length;
            for (int l, m, r, step = 1; step < n; step <<= 1)
            {
                l = 0;
                while (l < n)
                {
                    m = l + step - 1;
                    if (m + 1 >= n)
                    {
                        break;
                    }

                    r = Math.Min(l + (step << 1) - 1, n - 1);
                    Merge1(l, m, r);
                    l = r + 1;
                }
            }
        }

        private void Merge1(int l, int m, int r)
        {
            int i = l;
            int a = l;
            int b = m + 1;
            while (a <= m && b <= r)
            {
                help[i++] = arr[a++];
            }

            while (b <= r)
            {
                help[i++] = arr[b++];
            }

            while (a <= m)
            {
                help[i++] = arr[a++];
            }

            for (int j = l; j <= r; j++)
            {
                arr[i] = help[i];
            }
        }
    }
}