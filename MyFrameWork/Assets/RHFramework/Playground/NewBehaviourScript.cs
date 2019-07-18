using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RHFramework
{
    public class NewBehaviourScript : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("RHFramework/Playground")]
        static void Test()
        {
            var resData = ResData.Instance;
            resData.AssetBundleDatas.Clear();


            var assetBundleNames = UnityEditor.AssetDatabase.GetAllAssetBundleNames();

            foreach (var assetBundleName in assetBundleNames)
            {
                var assetPaths = UnityEditor.AssetDatabase.GetAssetPathsFromAssetBundle(assetBundleName);

                var assetBundleData = new AssetBundleData()
                {
                    Name = assetBundleName,
                    DenpendnecyBundleNames = UnityEditor.AssetDatabase.GetAssetBundleDependencies(assetBundleName, false)
                };

                foreach (var assetPath in assetPaths)
                {
                    var assetData = new AssetData()
                    {
                        Name = assetPath.Split('/')
                        .Last()
                        .Split('.')
                        .First(),
                        OwnerBundleName = assetBundleName
                    };

                    assetBundleData.AssetDataList.Add(assetData);
                }

                resData.AssetBundleDatas.Add(assetBundleData);


            }

            resData.AssetBundleDatas.ForEach(abData => 
            {
                Debug.LogFormat("----{0}----", abData.Name);
                abData.AssetDataList.ForEach(assetData =>
                {
                    Debug.LogFormat("AB:{0} AssetData:{1}", abData.Name, assetData.Name);
                });

                foreach (var dependencyBundleName in abData.DenpendnecyBundleNames)
                {
                    Debug.LogFormat("AB:{0} Depend:{1}", abData.Name, dependencyBundleName);
                }
            });
        }
#endif
    }
}