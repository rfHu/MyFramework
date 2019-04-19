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
            roomTypeList.dataList.Add(new RoomType { id = "2", name = "卧室" });
            JsonDataUtil.SaveDataToPath(roomTypeList, Application.streamingAssetsPath, "RoomType");

            var styleTypeLst = new StyleTypeList();
            styleTypeLst.dataList.Add(new StyleType { id = "0", name = "欧式贵族风格" });
            styleTypeLst.dataList.Add(new StyleType { id = "1", name = "中式复古风格" });
            styleTypeLst.dataList.Add(new StyleType { id = "2", name = "现代纯色风格" });
            JsonDataUtil.SaveDataToPath(styleTypeLst, Application.streamingAssetsPath, "StyleType");

            var spaceDataList = new ADTSpaceDataList();
            spaceDataList.dataList.Add(new ADTSpaceData { id = "0", data = new SpaceDataValue { Name = "星空间", TypeID = "0", StyleID = "2" } });
            spaceDataList.dataList.Add(new ADTSpaceData { id = "1", data = new SpaceDataValue { Name = "星空间", TypeID = "1", StyleID = "2" } });
            spaceDataList.dataList.Add(new ADTSpaceData { id = "2", data = new SpaceDataValue { Name = "星空间", TypeID = "2", StyleID = "2" } });
            spaceDataList.dataList.Add(new ADTSpaceData { id = "3", data = new SpaceDataValue { Name = "金玉满堂", TypeID = "0", StyleID = "1" } });
            spaceDataList.dataList.Add(new ADTSpaceData { id = "4", data = new SpaceDataValue { Name = "金玉满堂", TypeID = "2", StyleID = "1" } });
            spaceDataList.dataList.Add(new ADTSpaceData { id = "5", data = new SpaceDataValue { Name = "水墨丹青", TypeID = "0", StyleID = "1" } });
            spaceDataList.dataList.Add(new ADTSpaceData { id = "6", data = new SpaceDataValue { Name = "水墨丹青", TypeID = "1", StyleID = "1" } });
            spaceDataList.dataList.Add(new ADTSpaceData { id = "7", data = new SpaceDataValue { Name = "水墨丹青", TypeID = "2", StyleID = "1" } });
            spaceDataList.dataList.Add(new ADTSpaceData { id = "8", data = new SpaceDataValue { Name = "罗马假日", TypeID = "0", StyleID = "0" } });
            spaceDataList.dataList.Add(new ADTSpaceData { id = "9", data = new SpaceDataValue { Name = "罗马假日", TypeID = "2", StyleID = "0" } });
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
        public class SpaceDataValue
        {
            public string Name;
            public string TypeID;
            public string StyleID;
            public string ImgPath;
        }

        class ADTSpaceDataList : JsonDataList<ADTSpaceData> { }
    }
}