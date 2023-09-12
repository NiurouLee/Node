public class S_06
{
    //二分搜索
    public static int[] Arr = new int[] { 1, 3, 4, 5, 8, 9, 13, 15, 23, 39, 59 };

    /// <summary>
    ///如果在有序数组中确定一个数是否存在
    /// </summary>
    /// <param name="inArr"></param>
    /// <param name="inNum"></param>
    /// <returns></returns>
    public static bool Exist(int[] inArr, int inNum)
    {
        if (inArr == null || inArr.Length == 0)
        {
            return false;
        }

        int l = 0, r = inArr.Length - 1, m = 0;
        while (l < r)
        {
            //为什么这么写：防止l+r 太大 溢出，然后>>1 代表/2
            m = (l + (r - l)) >> 1;
            if (inArr[m] == inNum)
                return true;
            else if (inArr[m] > inNum)
                r = m - 1;
            else
                l = m + 1;
        }

        return false;
    }

    /// <summary>
    /// 找大于或者等于num的最左位置
    /// </summary>
    /// <returns></returns>
    public static int FindIndex(int[] inArr, int inNum)
    {
        if (inArr == null || inArr.Length == 0)
        {
            return -1;
        }

        if (!(inArr[^1] >= inNum))
            return -1;

        int result = -1;

        int l = 0, r = inArr.Length - 1, m = (l + (r - l)) >> 1;
        while (l < r)
        {
            m = (l + (r - l)) >> 1;

            if (inArr[m] >= inNum)
            {
                r = m - 1;
                result = m;
            }
            else
            {
                l = m + 1;
            }
        }

        return result;
    }


    /// <summary>
    /// 找小于等于num的最右侧位置
    /// </summary>
    /// <param name="inArr"></param>
    /// <param name="inNum"></param>
    /// <returns></returns>
    public static int FindLessRight(int[] inArr, int inNum)
    {
        if (inArr == null || inArr.Length == 0)
        {
            return -1;
        }

        int l = 0, r = inArr.Length - 1, m = 0, result = -1;
        while (l < r)
        {
            m = (l + (r - l) >> 1);
            if (inArr[m] <= inNum)
            {
                result = m;
                l = m + 1;
            }
            else
            {
                r = m - 1;
            }
        }

        return result;
    }

    //二分搜索不一定发生在有序数组上，
    /// <summary>
    /// 找峰值问题 （数组中任意相邻的数不等） 不是找最大的峰值 只要返回一个峰值就行
    /// </summary>
    /// <returns></returns>
    public static int FindPeakValue(int[] inArr)
    {
        if (inArr == null)
        {
            return -1;
        }

        if (inArr.Length == 1)
        {
            return 0;
        }

        if (inArr[0] > inArr[1])
        {
            return 0;
        }

        if (inArr[^1] > inArr[^2])
        {
            return inArr.Length - 1;
        }


        int l = 1, r = inArr.Length - 2, m = 0, result = -1;
        while (l < r)
        {
            m = (l + (r - l)) >> 2;
            // 左大去左边，
            if (inArr[m - 1] > inArr[m])
            {
                r = m - 1;
            }
            //右边大去右边
            else if (inArr[m + 1] > inArr[m])
            {
                l = m + 1;
            }
            else
            {
                result = m;
            }
        }

        return result;
    }
}