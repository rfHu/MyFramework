using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RHFramework
{
    public enum UILayer
    {
        Bg,
        Common,
        Top
    }

    public class GUIManager : MonoBehaviour
    {
        #region Canvas相关属性和方法
        private static GameObject mPrivateUIRoot;

        public static GameObject UIRoot
        {
            get
            {
                if (mPrivateUIRoot == null)
                {
                    var uirootPrefab = Resources.Load<GameObject>("UIRoot");
                    mPrivateUIRoot = GameObject.Instantiate(uirootPrefab);
                    mPrivateUIRoot.name = "UIRoot";
                }

                return mPrivateUIRoot;
            }
        }

        public static void SetResolution(float width, float height, float matchWidthOrHeight)
        {
            var canvasScaler = UIRoot.GetComponent<CanvasScaler>();
            canvasScaler.referenceResolution = new Vector2(width, height);
            canvasScaler.matchWidthOrHeight = matchWidthOrHeight;
        }
        #endregion

        public static Dictionary<string, GameObject> mpanelsDict = new Dictionary<string, GameObject>();

        public static void UnloadPanel(string panelName)
        {
            if (mpanelsDict.ContainsKey(panelName))
            {
                Destroy(mpanelsDict[panelName]);
            }
        }

        public static GameObject LoadPanel(string panelName, UILayer layer)
        {
            var panelPrefab = Resources.Load<GameObject>(panelName);
            var panel = Instantiate(panelPrefab);
            panel.name = panelName;

            mpanelsDict.Add(panel.name, panel);

            switch (layer)
            {
                case UILayer.Bg:
                    panel.transform.SetParent(UIRoot.transform.Find("Bg"));
                    break;
                case UILayer.Common:
                    panel.transform.SetParent(UIRoot.transform.Find("Common"));
                    break;
                case UILayer.Top:
                    panel.transform.SetParent(UIRoot.transform.Find("Top"));
                    break;
                default:
                    break;
            }

            var panelRectTrans = panel.transform as RectTransform;

            panelRectTrans.offsetMin = Vector2.zero;
            panelRectTrans.offsetMax = Vector2.zero;
            panelRectTrans.anchoredPosition3D = Vector3.zero;
            panelRectTrans.anchorMin = Vector2.zero;
            panelRectTrans.anchorMax = Vector2.one;

            return panel;
        }
    }
}