using RHFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PicBtnJson2Go : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var data = JsonDataUtil.ReadDataFromPath<PicBtnList>(string.Format("{0}/{1}.json", Application.streamingAssetsPath, "picButtonData"));

        GameObject bg = new GameObject();
        bg.name = "bgImg";
        bg.transform.SetParent(transform);
        var img = bg.AddComponent<Image>();

        var rectBg = bg.GetComponent<RectTransform>();
        rectBg.anchoredPosition = Vector2.zero;
        img.sprite = Resources.Load<Sprite>(data.BgName);
        img.SetNativeSize();

        var btnPre = Resources.Load<GameObject>("ImgBtn");
        foreach (PicButtonData item in data.dataList)
        {
            var go = Instantiate(btnPre, bg.transform);
            go.GetComponentInChildren<Text>().text = item.name;
            go.GetComponent<RectTransform>().anchoredPosition = item.localPos;
        }

    }
}
