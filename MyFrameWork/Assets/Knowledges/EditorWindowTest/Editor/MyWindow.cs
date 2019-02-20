using UnityEngine;
using System.Collections;
using UnityEditor;//注意要引用
public class MyWindow : EditorWindow
{
    [MenuItem("MyKnowledge/MyWindow", false, 2)]//在unity菜单Window下有MyWindow选项
    static void Init()
    {
        Rect rect = new Rect(0, 0, 500, 500);
        MyWindow myWindow = (MyWindow)EditorWindow.GetWindowWithRect(typeof(MyWindow), rect, false, "MyWindow");//创建窗口
        myWindow.Show();//展示
    }

    private bool isTrue;

    void OnGUI()
    {
        EditorGUILayout.LabelField(EditorWindow.focusedWindow.ToString());
        EditorGUILayout.LabelField(isTrue.ToString());
        isTrue = EditorGUILayout.ToggleLeft("Max", isTrue);
        isTrue = EditorGUILayout.ToggleLeft("Min", isTrue);
    }
}