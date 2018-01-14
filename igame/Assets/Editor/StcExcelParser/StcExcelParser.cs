using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEditor;

public class StcExcelParser
{

    public static void ParseExcel()
    {
#if UNITY_EDITOR
        //FileStream stream = null;
        //UnityEngine.Object[] arr = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.TopLevel);
        //string stcPath = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf('/')) + "/" + AssetDatabase.GetAssetPath(arr[0]);
        //Debug.Log(stcPath);
        //if (!stcPath.EndsWith(".xlsx"))
        //{
        //    Debug.LogError("选中了非xlsx格式的文件！！");
        //    return;
        //}
        //try
        //{
        //    List<List<string>> datas = new List<List<string>>();
        //    stream = File.Open(stcPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        //    Excel.IExcelDataReader excelReader = Excel.ExcelReaderFactory.CreateOpenXmlReader(stream);
        //    string fileName = stcPath.Substring(stcPath.LastIndexOf('/') + 1);
        //    string tableName = fileName.Substring(0, fileName.LastIndexOf('.'));
        //    tableName = StringUtil.UpperFirstLetter(tableName);
        //    while (excelReader.Read())
        //    {
        //        List<string> columnData = new List<string>();
        //        for (int i = 0; i < excelReader.FieldCount; i++)
        //        {
        //            string value = excelReader.IsDBNull(i) ? "" : excelReader.GetString(i);
        //            columnData.Add(value);
        //        }
        //        datas.Add(columnData);
        //    }

        //    //读取模板文件
        //    string code = "";
        //    using (FileStream fsRead = new FileStream(Application.dataPath + "/Editor/StcExcelParser/VoModel.txt", FileMode.Open))
        //    {
        //        int fsLen = (int)fsRead.Length;
        //        byte[] heByte = new byte[fsLen];
        //        int r = fsRead.Read(heByte, 0, heByte.Length);
        //        code = System.Text.Encoding.UTF8.GetString(heByte);
        //        fsRead.Close();
        //    }

        //    string attr = "";
        //    Dictionary<string, string> array_dic = new Dictionary<string, string>();
        //    for (int j = 0; j < datas[1].Count; j++)
        //    {
        //        string name = datas[1][j].ToString();
        //        string type = datas[2][j].ToString();
        //        attr += "[XmlAttribute(\"" + name + "\")]" + "\n\t";
        //        if (type == "array_int")
        //        {
        //            attr += "public string" + " " + name + ";\n\t";
        //            array_dic.Add(name, "int");
        //        }
        //        else if (type == "array_float")
        //        {
        //            attr += "public string" + " " + name + ";\n\t";
        //            array_dic.Add(name, "float");
        //        }
        //        else if (type == "array_string")
        //        {
        //            attr += "public string" + " " + name + ";\n\t";
        //            array_dic.Add(name, "string");
        //        }
        //        else
        //        {
        //            attr += "public " + type + " " + name + ";\n\t";
        //        }
        //    }

        //    foreach (var item in array_dic)
        //    {
        //        attr += "public " + item.Value + "[] " + StringUtil.UpperFirstLetter(item.Key) + "\n\t";
        //        attr += "{\n\t\t";
        //        attr += "get\n\t\t";
        //        attr += "{\n\t\t\t";
        //        if (item.Value == "int")
        //            attr += "return StringUtil.StcStringToIntArr(this." + item.Key + ");\n\t\t";
        //        else if (item.Value == "float")
        //            attr += "return StringUtil.StcStringToFloatArr(this." + item.Key + ");\n\t\t";
        //        else if (item.Value == "string")
        //            attr += "return StringUtil.StcStringToStringArr(this." + item.Key + ");\n\t\t";
        //        attr += "}\n\t";
        //        attr += "}\n\t";
        //    }

        //    //constant表特殊处理
        //    if (tableName == "Constant")
        //    {
        //        code += "\npublic class ConstName\n";
        //        code += "{\n\t";

        //        for (int i = 3; i < datas.Count; i++)
        //        {
        //            string name = datas[i][0].ToString();
        //            code += "public static string " + name + " = " + "\"" + name + "\";\n\t";
        //        }

        //        code = code.Substring(0, code.Length - 1);
        //        code += "}";
        //    }


        //    attr = attr.Substring(0, attr.Length - 2);
        //    code = code.Replace("$className", tableName);
        //    code = code.Replace("$attr", attr);

        //    string outPutFile = Application.dataPath + "/Script/IGame/StcMgr" + "/" + tableName + "Vo.cs";
        //    //输出CS文件
        //    using (var f = File.Create(outPutFile))
        //    {
        //        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(code);
        //        f.Write(bytes, 0, bytes.Length);
        //        f.Close();
        //        Debug.LogWarning("生成代码成功！ ");
        //    }
        //}
        //catch (Exception ep)
        //{
        //    Debug.LogError(ep.ToString());
        //}
        //finally
        //{
        //    if (stream != null)
        //        stream.Close();
        //}
#endif
    }

    public static void ParseExcelToLua()
    {

    }

}
