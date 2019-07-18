using System.Collections.Generic;

namespace RHFramework
{
    public class AssetBundleData
    {
        public string Name;

        public List<AssetData> AssetDataList = new List<AssetData>();

        public string[] DenpendnecyBundleNames;
    }
}