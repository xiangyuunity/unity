using UnityEditor;
using UnityEngine;

public class ParserMenu
{
    [MenuItem("iGame/Parse Stc Excel To C#", false, 101)]
    static void ParseSelectExcel()
    {
        StcExcelParser.ParseExcel();
    }

    [MenuItem("iGame/Parse Stc Excel To Lua", false, 102)]
    static void ParseSelectExcelToLua()
    {
        StcExcelParser.ParseExcelToLua();
    }

    //[MenuItem("Tools/testselect")]
    //public static void testselect()
    //{
    //    UnityEngine.Object[] arr = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.TopLevel);
    //    Debug.LogError(Application.dataPath);
    //    Debug.LogError(Application.dataPath.Substring(0, Application.dataPath.LastIndexOf('/')) + "/" + AssetDatabase.GetAssetPath(arr[0]));
    //}
}
