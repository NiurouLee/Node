using System;

namespace NFrameWork.Res
{
    public static class Util
    {
        public static int FactorSize(int inOrigMaxSize, int inFactor)
        {
            var maxSize = inOrigMaxSize * inFactor;
            if (maxSize == 0)
            {
                maxSize = 1;
            }

            return maxSize;
        }
    }
}