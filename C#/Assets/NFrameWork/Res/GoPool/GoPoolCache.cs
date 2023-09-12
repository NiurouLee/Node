using System.Collections.Generic;
using UnityEngine;

namespace NFrameWork.Res
{
    public class GoPoolCache
    {
        public int UsingCnt;
        public int PoolCnt;
        public float PoolTouch;
        public Dictionary<UnityEngine.GameObject, float> pool;

        public GoPoolCache()
        {
            pool = new Dictionary<GameObject, float>();
        }
    }
}