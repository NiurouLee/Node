using System.Collections.Generic;
using NFrameWork.NLog;
using Unity.VisualScripting;
using UnityEngine;

namespace NFrameWork.Res
{
    /// <summary>
    /// Res之下是ResCache,是资源的cache
    /// 一个资源的生命周期如下：
    /// 1.Res.Load(a), 得到c，c依赖bundleB，BundleB依赖BundleA, c->B->A
    /// 则这是c,B,A 都各自cache。loaded中，refCnt=1
    /// 2.Res.Free(a) 这是c进入cache.Cached中
    /// 3.Cache.Purge 出c , c被真正的释放，同时开始释放c的直接依赖B，b进入cache.Cached
    /// 4.Cache.Purge 出B , B被真正的释放，同是直接释放B的直接依赖A，A进入cache.Cached
    /// 5.Cache.Purge 出A , A被真正的释放.
    /// </summary>
    public class ResCache
    {
        public int MaxSize { get; private set; }

        private Dictionary<int, ResCacheObj> loaded;

        private int loadedSize = 0;

        private Dictionary<int, ResCacheObj> cached;

        private int cachedSize = 0;

        private int serial = 0;

        private List<int> timeOutAssetID;

        public void SetCachePolicy(int inMaxSize, float inTimeOutSeconds)
        {
            this.MaxSize = inMaxSize;
        }


        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="inAssetInfo"></param>
        /// <returns></returns>
        public ResCacheObj Get(AssetInfo inAssetInfo)
        {
            var assetID = inAssetInfo.AssetID;
            var cache = loaded[assetID];
            if (cache != null)
            {
                cache.RefCnt += 1;
                return cache;
            }
            else
            {
                var c = this.cached[assetID];
                if (c != null)
                {
                    this.cached[assetID] = null; //出cached 进loaded
                    if (this.timeOutAssetID.Contains(assetID))
                    {
                        timeOutAssetID.Remove(assetID);
                        this.cachedSize = this.cachedSize - 1;
                        ResCacheObj resCacheObj = new();
                        resCacheObj.Asset = c.Asset;
                        resCacheObj.Error = c.Error;
                        resCacheObj.RefCnt = 1;
                        resCacheObj.AssetInfo = c.AssetInfo;
                        this.loaded.Add(assetID, resCacheObj);
                        this.loadedSize = this.loadedSize + 1;
                        return resCacheObj;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 放入
        /// </summary>
        /// <param name="inAssetInfo"></param>
        /// <param name="inAsset"></param>
        /// <param name="inRefCount"></param>
        public void Put(AssetInfo inAssetInfo, Object inAsset, int inRefCount)
        {
            var assetID = inAssetInfo.AssetID;
            var c = this.cached[assetID];
            if (c != null)
            {
                //不应该到这里
                this.cached[assetID] = null;
                if (this.timeOutAssetID.Contains(assetID))
                {
                    timeOutAssetID.Remove(assetID);
                }

                this.cachedSize = this.cachedSize - 1;
                Nlog.Err($"ResCache.Put in Cached {inAssetInfo.AssetPath}");
            }

            var a = this.loaded[assetID];
            if (a != null)
            {
                a.RefCnt = a.RefCnt + inRefCount;
                //不应该到这里
                Nlog.Err($"ResCache.Put in Loaded {inAssetInfo.AssetPath}");
            }
            else
            {
                var cacheObj = new ResCacheObj();
                cacheObj.Asset = inAsset;
                cacheObj.AssetInfo = inAssetInfo;
                cacheObj.RefCnt = inRefCount;
                this.loaded[assetID] = new ResCacheObj();
            }
        }

        /// <summary>
        /// 释放
        /// </summary>
        /// <param name="inAssetInfo"></param>
        public void Free(AssetInfo inAssetInfo)
        {
            int assetID = inAssetInfo.AssetID;
            var a = this.loaded[assetID];
            if (a != null)
            {
                a.RefCnt = a.RefCnt - 1;
                if (a.RefCnt <= 0)
                {
                    this.loaded[assetID] = null;
                    this.loadedSize = this.loadedSize - 1;
                    var touch = this.NextSerial();
                    var resCacheObj = new ResCacheObj();
                    resCacheObj.Asset = a.Asset;
                    resCacheObj.Touch = touch;
                    resCacheObj.AssetInfo = inAssetInfo;
                    this.cached[assetID] = resCacheObj;
                    this.Purge();
                }
            }
            else
            {
                Nlog.Err($"Cache.Free not in Loaded {inAssetInfo.AssetPath}");
            }
        }


        public void Purge()
        {
            var maxSize = Util.FactorSize(this.MaxSize, Res.CacheFactor);
            while (this.cachedSize > maxSize)
            {
                int eldestAssetID;
                ResCacheObj eldestItem = null;
                foreach (var cacheKV in this.cached)
                {
                    var assetID = cacheKV.Key;
                    var item = cacheKV.Value;
                    if (eldestItem == null || item.Touch < eldestItem.Touch)
                    {
                        eldestAssetID = assetID;
                        eldestItem = item;
                    }
                }

                if (eldestItem != null)
                {
                    // this.RealFree(eldestAssetID, eldestItem);
                }

                break;
            }
        }

        private void RealFree(int eldestAssetID, ResCacheObj eldestItem)
        {
        }


        private int NextSerial()
        {
            this.serial = this.serial + 100;
            return this.serial;
        }
    }
}