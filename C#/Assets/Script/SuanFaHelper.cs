public static class SuanFaHelper
{
    public static void Swap(int[] inArr, int indexA, int indexB)
    {
        if (indexA >= inArr.Length || indexB >= inArr.Length || inArr == null || inArr.Length == 0)
        {
            return;
        }

        (inArr[indexA], inArr[indexB]) = (inArr[indexB], inArr[indexA]);
    }
}