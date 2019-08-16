using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public class HotUpdateMgr : MonoSingleton<HotUpdateMgr>
    {
        public bool HasNewVersionRes
        {
            get
            {
                var remoteResVersion = 1;
                var localResVersion = 0;
                return remoteResVersion > localResVersion;
            }
        }
    }
}