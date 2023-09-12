using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NFrameWork.NLog;
using NFrameWork.NTask;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NFrameWork.Res
{
    /// <summary>
    /// Res之上是GoPool
    /// 这一层是控制场景对象的cache,因为GameObject.Instantiate 太耗了
    /// 可以使用SetActive来pool这个场景对象，但有时候SetActive也太耗了，可以使用SetPosition放到摄像机视锥体以外
    /// 允许设置额外的资源数量max_extra_res_size,用于控制内存大小
    /// 跟Res接口一样，load，free，但这里如果load失败，不需要调用free
    /// </summary>
    public class GoPool
    {
        private static List<GoPool> AllNeedUpdate;

        private static Dictionary<string, GoPool> All;

        public static int AllAccess = 0;

        public static int AllHit = 0;

        public static Vector3 FarawayPosition = new Vector3(10000, 10000, 10000);

        public static void UpdateAll(float deltaTime)
        {
            foreach (var pool in AllNeedUpdate)
            {
                pool.Update(deltaTime);
            }
        }


        //-----------------------------------以下为私有-----------------------


        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }

        public Transform Parent { get; private set; }

        public int MaxExtraGoSize { get; private set; }

        public int MaxExtraResSize { get; private set; }

        public int Serial { get; private set; }

        public int UsingResCount { get; private set; }

        public int ExtraResCount { get; private set; }

        public int UsingObjectCount { get; private set; }

        public int ExtraObjetCount { get; private set; }

        public bool PoolBySetPosition { get; set; }

        public List<AssetInfo> timeOutKeys;

        private float timeOutCurAccumulate = 0;

        private bool timeOutNeedMoreFree = false;

        private float timeOutSeconts = 0;

        private Dictionary<AssetInfo, GoPoolCache> Caches = new Dictionary<AssetInfo, GoPoolCache>();


        public static GoPool Create(string inName, Transform inParent, int inMaxExtraGoSize, int inMaxExtraResSize)
        {
            if (All.TryGetValue(inName, out var inGoPool))
            {
                Nlog.Err($"Pool create fail name duplicated,name:{inName}");
                return inGoPool;
            }
            else
            {
                var pool = new GoPool(inName, inParent, inMaxExtraGoSize, inMaxExtraResSize);
                All.Add(inName, pool);
                return pool;
            }
        }


        /// <summary>
        /// 构造私有，需要统一添加到PoolGo的All中统一调度，
        /// </summary>
        /// <param name="inName">名字</param>
        /// <param name="inParent">父Transform</param>
        /// <param name="inMaxExtraGoSize">最大</param>
        /// <param name="inMaxExtraResSize"></param>
        private GoPool(string inName, Transform inParent, int inMaxExtraGoSize, int inMaxExtraResSize)
        {
            this.Name = inName;
            this.Parent = inParent;
            this.MaxExtraGoSize = inMaxExtraGoSize;
            this.MaxExtraResSize = inMaxExtraResSize;
            this.Serial = 0;
            this.UsingResCount = 0;
            this.ExtraResCount = 0;
            this.UsingObjectCount = 0;
            this.ExtraObjetCount = 0;

        }

        /// <summary>
        /// 设置池子策略
        /// </summary>
        /// <param name="inMaxExtraGoSize">最大缓存go数量</param>
        /// <param name="inMaxExtraResSize">最大缓存资源数量</param>
        /// <param name="inTimeOutSecond">时间</param>
        public void SetCachePolicy(int inMaxExtraGoSize, int inMaxExtraResSize, float inTimeOutSecond)
        {
            this.MaxExtraGoSize = inMaxExtraGoSize;
            this.MaxExtraResSize = inMaxExtraResSize;
            if (MaxExtraGoSize > 0 && inTimeOutSecond >= 1)
            {
                inTimeOutSecond = Mathf.Floor(inTimeOutSecond);
                if (inTimeOutSecond > 99)
                {
                    inTimeOutSecond = 99;
                }

                this.timeOutCurAccumulate = UnityEngine.Random.Range(0, 1);
                this.timeOutNeedMoreFree = false;
                this.timeOutKeys = new List<AssetInfo>();
                this.timeOutSeconts = inTimeOutSecond;
                AllNeedUpdate.Add(this);
            }
            else
            {
                this.timeOutKeys = null;
                AllNeedUpdate.Remove(this);
            }
        }

        private float NextSerial()
        {
            this.Serial = this.Serial + 100;
            return this.timeOutSeconts + this.Serial;
        }


        private void Update(float inDeltaTime)
        {
            if (this.ExtraObjetCount == 0)
            {
                return;
            }

            this.timeOutCurAccumulate += inDeltaTime;

            if (this.timeOutCurAccumulate > 1)
            {
                this.timeOutCurAccumulate = 0;
                this.timeOutNeedMoreFree = false;
                AssetInfo timeOutAssetInfo = null;
                GoPoolCache timeOutGoGoPoolCache = null;
                GameObject timeOutGo = null;

                foreach (var cacheKV in this.Caches)
                {
                    var goCache = cacheKV.Value;
                    var pool = goCache.pool;
                    var assetInfo = cacheKV.Key;
                    var has = false;
                    foreach (var goKV in pool)
                    {
                        var go = goKV.Key;
                        var touch = goKV.Value;
                        float timeLeft = touch % 100;
                        if (timeLeft > 0)
                        {
                            pool[go] = touch - 1;
                        }
                        else if (timeOutGo == null)
                        {
                            timeOutGo = go;
                            timeOutAssetInfo = assetInfo;
                            timeOutGoGoPoolCache = goCache;
                        }
                        else
                        {
                            has = true;
                        }
                    }

                    if (has)
                    {
                        this.timeOutKeys.Add(assetInfo);
                        this.timeOutNeedMoreFree = true;
                    }
                    else
                    {
                        this.timeOutKeys.Remove(assetInfo);
                    }
                }

                if (timeOutGo != null)
                {
                    this.RealFree(timeOutAssetInfo, timeOutGoGoPoolCache, timeOutGo);
                }
            }

            else if (this.timeOutNeedMoreFree)
            {
                AssetInfo firstAssetInfo = null;
                GoPoolCache timeOutGoGoPoolCache = null;
                GameObject timeOutGo = null;
                var has = false;
                foreach (var assetInfo in this.timeOutKeys)
                {
                    has = true;
                    firstAssetInfo = assetInfo;
                    var goCache = this.Caches[assetInfo];
                    if (goCache != null)
                    {
                        foreach (var goKV in goCache.pool)
                        {
                            var go = goKV.Key;
                            var touch = goKV.Value;
                            var timeLeft = touch % 100;
                            if (timeLeft <= 0)
                            {
                                timeOutGo = go;
                                timeOutGoGoPoolCache = goCache;
                                break;
                            }
                        }
                    }

                    break;
                }

                this.timeOutNeedMoreFree = has;
                if (timeOutGo != null)
                {
                    this.RealFree(firstAssetInfo, timeOutGoGoPoolCache, timeOutGo);
                }
                else if (firstAssetInfo != null)
                {
                    this.timeOutKeys.Remove(firstAssetInfo);
                }
            }
        }

        public NTask<GameObject> Load(AssetInfo inAssetInfo)
        {
            return this.DoLoad(inAssetInfo);
        }

        private NTask<GameObject> DoLoad([NotNull] AssetInfo inAssetInfo)
        {
            UnityEngine.GameObject eldestGo = null;
            float eldestTouch;
            var cache = this.Caches[inAssetInfo];
            if (cache == null)
            {
                cache = new GoPoolCache();
                cache.UsingCnt = 1;
                this.Caches.Add(inAssetInfo, cache);
                this.UsingResCount += 1;
                this.UsingObjectCount += 1;
            }
            else
            {
                cache.UsingCnt += 1;
                this.UsingObjectCount += 1;
                foreach (var kv in cache.pool)
                {
                    eldestGo = kv.Key;
                    eldestTouch = kv.Value;
                }
            }


            GoPool.AllAccess += 1;
            if (eldestGo != null)
            {
                GoPool.AllHit += 1;
                cache.pool[eldestGo] = 0;
                this.ExtraObjetCount -= 1;
                if (cache.UsingCnt == 1)
                {
                    this.UsingResCount += 1;
                    this.ExtraResCount -= 1;
                }

                eldestGo.SetActive(true);
            }
            else
            {
                return null;
                // await Res.Ins.Load(inAssetInfo, this.ResLoadDone, this, inAssetInfo, cache, callback);

            }
            return null;
        }


        public void Free(AssetInfo inAssetInfo, GameObject inGo)
        {
            if (this.DoFree(inAssetInfo, inGo))
            {
                this.Purge();
            }
        }

        private bool DoFree(AssetInfo inAssetInfo, GameObject inGo)
        {
            if (inAssetInfo == null)
            {
                Nlog.Err("Pool.Free PrefabAssetInfo is null");
                return false;
            }

            if (inGo == null)
            {
                Nlog.Err($"Pool.Free GameObject is null,AssetInfo:{inAssetInfo}");
                return false;
            }

            var cache = this.Caches[inAssetInfo];
            if (cache == null)
            {
                Nlog.Err($"Pool.Free Cache is null,AssetInfo:{inAssetInfo}");
                return false;
            }

            if (cache.UsingCnt <= 0)
            {
                Nlog.Err($"Pool.Free Cache UsingCnt<=0,AssetInfo:{inAssetInfo}");
                return false;
            }

            var old = cache.pool[inGo];

            if (old > 0)
            {
                Nlog.Err($"Pool.Free Cache Pool Has This GamObject,AssetInfo{inAssetInfo}");
                return false;
            }

            if (this.PoolBySetPosition)
            {
                inGo.transform.position = FarawayPosition;
            }
            else
            {
                inGo.SetActive(false);
            }

            if (this.Parent != null)
            {
                inGo.transform.SetParent(this.Parent, false);
            }

            var touch = this.NextSerial();
            cache.pool[inGo] = touch;
            cache.PoolTouch = touch;
            cache.UsingCnt = cache.UsingCnt - 1;
            cache.PoolCnt = cache.PoolCnt + 1;
            this.UsingObjectCount = this.UsingObjectCount - 1;
            this.ExtraObjetCount = this.ExtraObjetCount + 1;
            if (cache.UsingCnt == 0)
            {
                this.UsingResCount = this.UsingResCount - 1;
                this.ExtraResCount = this.ExtraResCount + 1;
            }

            return true;
        }

        private void Purge()
        {
            int maxExtraGoSize = Util.FactorSize(this.MaxExtraGoSize, Res.AllAccess);
            int maxExtraResSize = Util.FactorSize(this.MaxExtraResSize, Res.CacheFactor);
            while (true)
            {
                if (!this.PurgeNext(maxExtraGoSize, maxExtraResSize))
                {
                    break;
                }
            }
        }

        private bool PurgeNext(int inMaxExtraGoSize, int inMaxExtraResSize)
        {
            if (this.ExtraResCount > inMaxExtraResSize)
            {
                AssetInfo eldestPoolAssetInfo = null;
                GoPoolCache eldestPoolGoPoolCache = null;

                foreach (var cacheKV in Caches)
                {
                    var ai = cacheKV.Key;
                    var aiCache = cacheKV.Value;
                    if (aiCache.UsingCnt == 0 && aiCache.PoolCnt > 0)
                    {
                        if (eldestPoolGoPoolCache == null || aiCache.PoolTouch < eldestPoolGoPoolCache.PoolTouch)
                        {
                            eldestPoolAssetInfo = ai;
                            eldestPoolGoPoolCache = aiCache;
                        }
                    }
                }

                foreach (var objectKV in eldestPoolGoPoolCache.pool)
                {
                    var go = objectKV.Key;
                    GameObject.Destroy(go);
                    Res.Ins.Free(eldestPoolAssetInfo);
                    this.ExtraObjetCount = this.ExtraObjetCount - 1;
                }

                this.Caches[eldestPoolAssetInfo] = null;
                this.ExtraResCount = this.ExtraObjetCount - 1;
                return this.ExtraResCount > inMaxExtraResSize || this.ExtraObjetCount > inMaxExtraGoSize;
            }
            else if (this.ExtraObjetCount > inMaxExtraGoSize)
            {
                float eldestTouch = 0;
                AssetInfo eldestAssetInfo = null;
                GoPoolCache eldestGoPoolCache = null;
                UnityEngine.Object eldestGo = null;

                foreach (var cacheKV in this.Caches)
                {
                    var ai = cacheKV.Key;
                    var aiCache = cacheKV.Value;
                    foreach (var goKV in aiCache.pool)
                    {
                        var go = goKV.Key;
                        var touch = goKV.Value;
                        if (go == null || touch < eldestTouch)
                        {
                            eldestAssetInfo = ai;
                            eldestGoPoolCache = aiCache;
                            eldestGo = go;
                            eldestTouch = touch;
                        }
                    }
                }

                // this.RealFree(eldestAssetInfo, eldestCache, eldestGo);
                return this.ExtraObjetCount > inMaxExtraGoSize;
            }
            else
            {
                return false;
            }
        }


        private void RealFree(AssetInfo inAssetInfo, GoPoolCache inGoPoolCache, GameObject inGo)
        {
            GameObject.Destroy(inGo);
            Res.Ins.Free(inAssetInfo);
            this.ExtraObjetCount = this.ExtraObjetCount - 1;
            inGoPoolCache.PoolCnt = inGoPoolCache.PoolCnt - 1;
            if (inGoPoolCache.UsingCnt == 0 && inGoPoolCache.PoolCnt == 0)
            {
                this.Caches[inAssetInfo] = null;
                this.ExtraResCount = this.ExtraResCount - 1;
            }
            else
            {
                inGoPoolCache.pool.Remove(inGo);
            }
        }


        public void Clear()
        {
            int old = this.MaxExtraResSize;
            this.MaxExtraGoSize = 0;
            this.Purge();
            this.MaxExtraGoSize = old;
        }


        private void ResLoadDone(Object inObjet, string inErr, Asset inAssetInfo, Action<Object, string> callback)
        {
            if (inObjet == null)
            {
                Res.Ins.Free(inAssetInfo.AssetInfo);
            }
        }


        public void ClearAll()
        {
            foreach (var cacheKv in Caches)
            {
                var assetInfo = cacheKv.Key;
                var cache = cacheKv.Value;

                foreach (var goKv in cache.pool)
                {
                    var go = goKv.Key;
                    GameObject.Destroy(go);
                    Res.Ins.Free(assetInfo);
                    this.ExtraObjetCount -= 1;
                }

                this.ExtraResCount -= 1;
            }

            this.Caches.Clear();
        }
    }
}