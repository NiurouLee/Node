using Random = UnityEngine.Random;

/// <summary>
/// 对数器
/// </summary>
public class Validator
{
    //对数器，自我验证技巧
    //可以控制长度，值，随机
    //
    public static int[] RandomArray(int n, int v)
    {
        var arr = new int[n];
        for (int i = 0; i < n; i++)
        {
            arr[i] = Random.Range(0, v);
        }

        return arr;
    }

    public static int[] CopyArray(int[] inArr)
    {
        var result = new int[inArr.Length];
        for (int i = 0; i < inArr.Length; i++)
        {
            result[i] = inArr[i];
        }

        return result;
    }

    /// <summary>
    /// 对比两个Array 是不是一样
    /// </summary>
    /// <param name="inArray1"></param>
    /// <param name="inArray2"></param>
    /// <returns></returns>
    public static bool SameArray(int[] inArray1, int[] inArray2)
    {
        if (inArray1.Length != inArray2.Length)
        {
            return false;
        }

        var length = inArray1.Length;
        for (int i = 0; i < length; i++)
        {
            if (inArray1[i] != inArray2[i])
            {
                return false;
            }
        }

        return true;
    }
}