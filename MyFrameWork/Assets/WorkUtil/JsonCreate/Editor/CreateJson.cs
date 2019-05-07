using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework {
    public class CreateJson : MonoBehaviour
    {
        [UnityEditor.MenuItem("MyKnowledge/CreateJson")]
        static void MenuCilcked()
        {
            var roomTypeList = new RoomTypeList();
            roomTypeList.dataList.Add(new RoomType { id = "0", name = "起居室" });
            roomTypeList.dataList.Add(new RoomType { id = "1", name = "厨房" });
            roomTypeList.dataList.Add(new RoomType { id = "2", name = "卫生间" });
            JsonDataUtil.SaveDataToPath(roomTypeList, Application.streamingAssetsPath, "RoomType");

            var styleTypeLst = new StyleTypeList();
            styleTypeLst.dataList.Add(new StyleType { id = "0", name = "新中式" });
            JsonDataUtil.SaveDataToPath(styleTypeLst, Application.streamingAssetsPath, "StyleType");

            var spaceDataList = new ADTSpaceDataList();
            spaceDataList.dataList.Add(new ADTSpaceData { id = "NeoChinese_Living room", data = new SpaceDataValue {id = "NeoChinese_Living room", name = "新中式 起居室", typeID = "0", styleID = "0" , imgPath = "Imgs/UI/NeoChinese_Living room" } });
            spaceDataList.dataList.Add(new ADTSpaceData { id = "NeoChinese_Kitchen", data = new SpaceDataValue { id = "NeoChinese_Kitchen", name = "新中式 厨房", typeID = "1", styleID = "0", imgPath = "Imgs/UI/NeoChinese_Kitchen" } });
            spaceDataList.dataList.Add(new ADTSpaceData { id = "NeoChinese_MainBathroom", data = new SpaceDataValue { id = "NeoChinese_MainBathroom", name = "新中式 主洗手间", typeID = "2", styleID = "0", imgPath = "Imgs/UI/NeoChinese_MainBathroom" } });
            spaceDataList.dataList.Add(new ADTSpaceData { id = "NeoChinese_PublicBathroom", data = new SpaceDataValue { id = "NeoChinese_PublicBathroom", name = "新中式 公共卫生间", typeID = "2", styleID = "0", imgPath = "Imgs/UI/NeoChinese_PublicBathroom" } });
            JsonDataUtil.SaveDataToPath(spaceDataList, Application.streamingAssetsPath, "SpaceData");


            Debug.Log("finish");
        }

        [Serializable]
        class RoomType : JsonDataIDName { }

        [Serializable]
        class RoomTypeList : JsonDataList<RoomType> { }

        [Serializable]
        class StyleType : JsonDataIDName { }

        [Serializable]
        class StyleTypeList : JsonDataList<StyleType> { }

        [Serializable]
        public class ADTSpaceData : JsonDataKV<SpaceDataValue> { }

        [Serializable]
        public class SpaceDataValue : JsonDataIDName
        {
            public string typeID;
            public string styleID;
            public string imgPath;
        }

        class ADTSpaceDataList : JsonDataList<ADTSpaceData> { }
    }
}