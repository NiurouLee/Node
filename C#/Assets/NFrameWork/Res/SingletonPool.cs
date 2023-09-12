namespace NFrameWork.Res
{
    /// <summary>
    /// 一个每个item都是单例的item缓存
    /// </summary>
    public class SingletonPool
    {
        public int Serial { get; private set; }

        public int CachedSize { get; private set; }
        
        public SingletonPool()
        {
        }
    }
}