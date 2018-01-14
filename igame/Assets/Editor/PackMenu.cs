using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PackMenu
{
//    [MenuItem("iGame/打开主场景", false, 1)]
//    static void SwitchMainScene()
//    {
//        EditorSceneManager.OpenScene("Assets/Scene/main.unity", OpenSceneMode.Single);
//    }
//
//    [MenuItem("iGame/打开战斗场景", false, 2)]
//    static void SwitchBattleScene()
//    {
//        EditorSceneManager.OpenScene("Assets/Resources/ArtRes/scene/Scenes/battle_001.unity", OpenSceneMode.Single);
//    }

    //[MenuItem("iGame/PackUI/PackSelectUI")]
    //static void PackSelectUI()
    //{
    //    PackUI.PackSelectUI();
    //}

    //[MenuItem("iGame/PackUI/PackAllUI")]
    //static void PackAllUI()
    //{
    //    PackUI.PackAllUI();
    //}

    //[MenuItem("iGame/PackAtlas/PackAllAtlas")]
    //static void PackAllAtlas()
    //{
    //    PackAtlas.PackAllAtlas();
    //}
    ////[MenuItem("PackUI/AddNeedTogenerate")]
    ////static void AddNeedTogenerate()
    ////{
    ////    UICodeGenerator.AddNeedTogenerate();
    ////}
    //[MenuItem("iGame/AddNeedClone")]
    //static void AddNeedClone()
    //{
    //    UICodeGenerator.AddNeedClone();
    //}
    [MenuItem("UITools/Generate Select UI C#", false, 51)]
    static void GenerateSelectUI()
    {
        UICodeGenerator.GenerateSelectUI();
    }

//    [MenuItem("iGame/Generate Select UI Lua", false, 52)]
//    static void GenerateSelectUILua()
//    {
//        UILuaGenerator.GenerateSelectUI();
//    }

}
