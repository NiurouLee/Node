using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.UI;

namespace NFrameWork.Res
{
    [Flags]
    public enum AssetState : byte
    {
        Loading = 1 << 0,
        Using = 1 << 1,
        Free = 1 << 2,
    }


    public class Asset
    {
        public AssetInfo AssetInfo { get; private set; }

        public AssetState AssetState { get; private set; }

        public UnityEngine.Object AssetObject { get; private set; }

        public string Err { get; private set; }

        private bool wantFree = false;


        public Asset(AssetInfo inAssetInfo)
        {
            //todo:补充本地化资源
            this.AssetInfo = inAssetInfo;
            this.AssetState = AssetState.Loading;
        }


        public void Load(Action<UnityEngine.Object, string> callback)
        {
            //Res.Ins.Load(this.AssetInfo, this.AssetLoadDone, this, callback);
        }

        public void Free()
        {
            this.wantFree = true;
            if (this.AssetState == AssetState.Free)
            {
                return;
            }

            if (this.AssetState == AssetState.Using)
            {
                Res.Ins.Free(this.AssetInfo);
                this.AssetObject = null;
                this.AssetState = AssetState.Free;
            }
        }


        public override bool Equals(object obj)
        {
            if (obj is Asset other)
            {
                return other.AssetInfo == this.AssetInfo && other.AssetInfo.ID == this.AssetInfo.ID;
            }

            return false;
        }


        private void AssetLoadDone(UnityEngine.Object inAssetObject, string inErr, Asset inAsset,
            Action<UnityEngine.Object, string> callback)
        {
            if (this.wantFree)
            {
                Res.Ins.Free(this.AssetInfo);
                this.AssetState = AssetState.Free;
                return;
            }

            this.AssetState = AssetState.Using;
            this.AssetObject = inAssetObject;
            this.Err = inErr;
            callback?.Invoke(this.AssetObject, Err);
        }
    }
}