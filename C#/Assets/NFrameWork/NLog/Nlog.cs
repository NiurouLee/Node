namespace NFrameWork.NLog
{
    public static class Nlog
    {
        public static void Msg(params object[] inContent)
        {
            UnityEngine.Debug.Log(inContent);
        }


        public static void Err(params object[] inContent)
        {
            UnityEngine.Debug.LogError(inContent);
        }
    }
}