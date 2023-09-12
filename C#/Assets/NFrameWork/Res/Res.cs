using System;
using System.Collections;
using System.IO;
using NFrameWork.File;
using UnityEngine.Networking;

namespace NFrameWork.Res
{
    /// <summary>
    /// Res是最底层的接口，
    /// 无论Prefab,AssetBundle,Sprite,Asset 都统一使用Load接口来加载，
    /// Load要与Free一一对应，无论加载成功与否，都会返回，应该都要记得调用free，同时只准load完成后才允许free
    /// 自带cache机制，策略是lru
    /// </summary>
    public class Res
    {
        public static int CacheFactor = 1;
        public static int AllAccess = 0;
        public static int AllHit = 0;
        public static string LoadingPath = String.Empty;
        private static Res instance;

        public static Res Ins
        {
            get
            {
                if (instance == null)
                {
                    instance = new Res();
                }

                return instance;
            }
        }

        /// <summary>
        /// 验证是否下本体，如果不在，下载资源，缓存请求
        /// </summary>
        /// <param name="inAssetInfo"></param>
        public IEnumerator LoadABToLocal(AssetInfo inAssetInfo, int inTimeOut = 0)
        {
            if (Res.LoadingPath == string.Empty)
            {
                yield return null;
            }

            var cdnPath = Path.Combine(Res.LoadingPath, inAssetInfo.AssetPath);
            var req = new UnityWebRequest(cdnPath, UnityWebRequest.kHttpVerbGET);
            req.downloadHandler = new DownloadHandlerBuffer();
            req.disposeDownloadHandlerOnDispose = true;
            req.timeout = inTimeOut;
            yield return req.SendWebRequest();
            if (req.result != UnityWebRequest.Result.Success)
            {
                req.Dispose();
                req = null;
            }
            else
            {
                string saveUrl = Path.Combine(UnityEngine.Application.persistentDataPath, "/", inAssetInfo.AssetPath);
                if (FileUtils.WriteAllBytes(saveUrl, req.downloadHandler.data))
                {
                }
            }
        }


        public void Load(AssetInfo assetInfo,
            System.Action<Object, string, Asset, System.Action<Object, string>> assetLoadDone, Asset asset,
            System.Action<Object, string> callback)
        {
        }

        public void Free(AssetInfo assetInfo)
        {
            throw new System.NotImplementedException();
        }
    }
}