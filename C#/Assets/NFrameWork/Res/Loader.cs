using UnityEngine.PlayerLoop;

namespace NFrameWork.Res
{
    public enum LoaderStat
    {
        Loading = 1,
        Using = 2,
        Free = 3,
    }


    public class Loader
    {
        public LoaderStat Stat { get; private set; }

        public AssetInfo AssetInfo { get; private set; }

        public Loader(AssetInfo inInfo)
        {
            this.AssetInfo = inInfo;
            this.Stat = LoaderStat.Loading;
        }
    }
}