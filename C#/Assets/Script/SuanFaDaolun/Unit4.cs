/// <summary>
/// 分治策略
/// </summary>
public class Unit4
{
    /*分治策略的三大步:
     * 1.分解(divide)：将问题分解为与原问题相同形式的问题，只是规模更小。
     * 2.解决(conquer)：递归的解决子问题，然后如果子问题的规模足够小，那么直接解决，饭后则继续分解为更小的子问题。
     * 3.合并(combine):将子问题的解组合成原问题的解。
     * 当子问题足够大时，需要递归求解时，称这种情况为递归情况，当子问题足够小时，不再需要递归，那就是说明递归已经触底了，进入了基本情况。
     * 有时有些子问题的形式与原问题不同，那么将子问题的求解过程看作合并的一部分。
     * 
     * 使用分治策略求解最大子数组和问题(股票问题)
     * 分析：最终的解可能完全位于分治后的左数组，或者右数组，或者同时跨越左数组和右数组
     * 如果只是位于左数组或者右数组，那么就是规模更小的同类型问题，但是如果要是跨越左右两个数组，就是需要额外求解了。
     * 如何求解同时跨越左右数组的解?
     */


    /// <summary>
    /// 求解最大子数组
    /// </summary>
    /// <param name="inArr"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public (int left, int right, int sum) FindMaxSumSubArray(int[] inArr, int low, int heigh)
    {
        ///递归的出口
        if (low == heigh)
        {
            return (low, heigh, inArr[low]);
        }

        int mid = low + (heigh - low) / 2;
        var leftResult = FindMaxSumSubArray(inArr, low, mid);
        var rightResult = FindMaxSumSubArray(inArr, mid + 1, heigh);
        var crossResult = FindMaxCrossingSubarray(inArr, low, mid, heigh);
        if (leftResult.sum >= rightResult.sum && leftResult.sum > crossResult.leftAndRight)
        {
            return leftResult;
        }
        else if (rightResult.sum >= leftResult.sum && rightResult.sum >= crossResult.leftAndRight)
        {
            return rightResult;
        }
        else
        {
            return crossResult;
        }
    }


    /// <summary>
    /// 最大子数组跨越中点的情况
    /// </summary>
    /// <param name="inArr"></param>
    /// <param name="low"></param>
    /// <param name="mid"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    public (int maxleft, int maxright, int leftAndRight) FindMaxCrossingSubarray(int[] inArr, int low, int mid,
        int height)
    {
        int maxleft = 0;
        int maxright = 0;
        int leftSum = 0;
        int rightSum = 0;
        int sum = 0;
        //求解左侧最大子数组
        for (int i = mid; i > low; i--)
        {
            sum = sum + inArr[i];
            if (sum > leftSum)
            {
                leftSum = sum;
                maxleft = i;
            }
        }

        //求解右侧最大子数组
        rightSum = 0;
        sum = 0;
        for (int i = mid + 1; i < height; i++)
        {
            sum = sum + inArr[i];
            if (sum > rightSum)
            {
                rightSum = sum;
                maxright = i;
            }
        }

        return (maxleft, maxright, leftSum + rightSum);
    }


    /*矩阵乘法问题 Strassen 算法
     *为了简单起见，我们假设使用分治算法计算C=A*B,假设三个矩阵均为nXn的矩阵，其中n为2的幂,这样可以保证每次一个矩阵划分为4个n/2的矩阵。
     * 分成s1-s10 10个小矩阵。空间换时间？
    */

    /*用代入法求解递归式
     * 1.猜测解的形式。
     * 2.用数学归纳法求出解的常数，并证明解是正确的。
     * 技巧：
     * 好的猜测:需要创造力和经验
     * 逐步调整法
     * 换元法
     */

    /*用递归树求解递归式
     *计算求解单一子节点问题的代价，然后将树中每层代价求和，得到每层的代价，然后再将所有层求和，得到所有层次的总代价
     */

    /*主定理
     *令a>1和b>1是常数，f(n)是一个函数，T(n)是定义在自然数集上的递归式：
     * T(n)=aT(n/b)+f(n)
     * 将n/b解释为n/b向下取整或者向上取整。
     * f(n）与n^(logb^a) 中的较大者决定了递归式的解
     * 若n^(logb^a)更大，则解为0(n^(logb^a))
     * 若f(n)更大，那么解就是f(n)；
     * 若两则大小一样，那么就需要乘上一个对数因子解为0(n^(logb^a)logn).
     * 这里的较大是多项式意义上的较大
     * 主定理三种情况是有间隙的，
     */

    /*证明主定理
     * 
     */
}