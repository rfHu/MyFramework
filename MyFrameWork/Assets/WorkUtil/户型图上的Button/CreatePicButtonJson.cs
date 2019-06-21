using RHFramework;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CreatePicButtonJson : MonoBehaviour
{
    private void Start()
    {
        var bg = transform.GetChild(0);
        var buttons = bg.GetComponentsInChildren<Button>();

        var listData = new PicBtnList();
        listData.BgName = bg.GetComponent<Image>().sprite.name;
        foreach (var button in buttons)
        {
            PicButtonData temp = new PicButtonData();
            temp.name = button.transform.GetComponentInChildren<Text>().text;
            temp.localPos = button.GetComponent<RectTransform>().anchoredPosition;
            listData.dataList.Add(temp);
        }

        JsonDataUtil.SaveDataToPath(listData, Application.streamingAssetsPath, "picButtonData");
    }
}

[Serializable]
class PicBtnList : JsonDataList<PicButtonData>
{
    public string BgName;
}

[Serializable]
public class PicButtonData : JsonDataIDName
{
    public Vector2 localPos; 
}