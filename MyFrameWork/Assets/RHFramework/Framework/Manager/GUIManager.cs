using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public class GUIManager 
    {
        public static void LoadPanel(string panelName)
        {
            var canvasObj = GameObject.Find("Canvas");

            var homePanelPrefab = Resources.Load<GameObject>(panelName);
            var homePanelObj = Object.Instantiate(homePanelPrefab);

            homePanelObj.transform.SetParent(canvasObj.transform);

            var homeRectTrans = homePanelObj.transform as RectTransform;

            homeRectTrans.offsetMin = Vector2.zero;
            homeRectTrans.offsetMax = Vector2.zero;
            homeRectTrans.anchoredPosition3D = Vector3.zero;
            homeRectTrans.anchorMin = Vector2.zero;
            homeRectTrans.anchorMax = Vector2.one;
        }
    }
}