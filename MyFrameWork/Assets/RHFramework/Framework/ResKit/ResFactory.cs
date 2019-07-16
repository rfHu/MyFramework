using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RHFramework
{
    class ResFactory
    {
        public static Res Create(string assetName, string assetBundleName)
        {
            Res res = null;

            if (assetBundleName != null)
            {
                res = new AssetRes(assetName, assetBundleName);
            }
            else if (assetName.StartsWith("resources://"))
            {
                res = new ResourceRes(assetName);
            }
            else
            {
                res = new AssetBundleRes(assetName);
            }

            return res;
        }
    }
}
