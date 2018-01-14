using UnityEngine;
using System.Collections;
using System.IO;
public class UICodeGenerator 
{
    public static void AddNeedTogenerate()
    {
        foreach (GameObject obj in UnityEditor.Selection.gameObjects)
        {
            if (null == obj)
            {
                continue;
            }
            if (obj == null)
            {
                return;
            }
            NeedTogenerate aaa = obj.AddComponent<NeedTogenerate>();
        }
    }
    public static void AddNeedClone()
    {
        foreach (GameObject obj in UnityEditor.Selection.gameObjects)
        {
            if (null == obj)
            {
                continue;
            }
            if (obj == null)
            {
                return;
            }
            NeedClone aaa = obj.AddComponent<NeedClone>();
        }

        //string aaaa = "21dsdasdad/";
       // int g = aaaa.LastIndexOf("ad");
      //  Debug.LogError(" " + g);
        //string bb = aaaa.Insert(aaaa.LastIndexOf('/'), "吴星星");
//        Debug.LogError(bb);
    }

    /*
    public  static void GenerateItem111111111(GameObject obj)
    {
       
           
        if (obj == null)
        {
            return;
        }
        else
        {

            string mystr;
            //读取
            using (FileStream fsRead = new FileStream(Application.dataPath + "/UIFile/ItemGeneratemould.txt", FileMode.Open))
            {
                int fsLen = (int)fsRead.Length;
                byte[] heByte = new byte[fsLen];
                int r = fsRead.Read(heByte, 0, heByte.Length);
                mystr = System.Text.Encoding.UTF8.GetString(heByte);
                Debug.LogWarning("" + mystr);
                fsRead.Close();
            }

            Transform[] trans = obj.transform.GetComponentsInChildren<Transform>();
            string variableName = "";
            string functionName = "";
            for (int i = 0; i < trans.Length; ++i)
            {
                if (trans[i].GetComponent<NeedTogenerate>() != null)
                {
                    NeedTogenerate needTogenerate = trans[i].GetComponent<NeedTogenerate>();
                    if (needTogenerate.m_generateTo == 1)
                    {
                        variableName += "public GameObject " + trans[i].gameObject.name + ";\n\t";
                        functionName += trans[i].gameObject.name + "=" + "_OPLogicBase.GetGameObject(\"" + trans[i].gameObject.name + "\");\n\t\t";
                    }
                }
            }

            mystr = mystr.Replace("$ClsName", obj.name);
            mystr = mystr.Replace("$Prop", variableName);
            mystr = mystr.Replace("$SaveData", functionName);

            string testFile = Application.dataPath + "Script/A3/UI/UIFile" + "/" + obj.name + ".cs";
            //输出CS文件
            using (var f = File.Create(testFile))
            {
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(mystr);
                f.Write(bytes, 0, bytes.Length);
                f.Close();
               // Debug.LogWarning("生成代码成功！ ");

            }

        }
        
    }*/

    //private static string myUIstr;
    //private static string UIvariableName1 = "";
  //  private static string UIfunctionName1 = "";

    public static void GenerateSelectUI()
    {
        string myUIstr = "";
        using (FileStream fsRead = new FileStream(Application.dataPath + "/UILuaFile/UICodeGenerator.txt", FileMode.Open))
        {
            
            int fsLen = (int)fsRead.Length;
            byte[] heByte = new byte[fsLen];
            int r = fsRead.Read(heByte, 0, heByte.Length);
            myUIstr = System.Text.Encoding.UTF8.GetString(heByte);
            Debug.LogWarning("" + myUIstr);
            fsRead.Close();
        }
        foreach (GameObject obj in UnityEditor.Selection.gameObjects)
        {
            if (null == obj)
            {
                continue;
            }
            if (obj == null)
            {
                return;
            }

            string UIvariableName = "";
            string UIfunctionName = "";
            string strUIItems = "";
            string[] strlist =  ParseNode(obj.transform);
            UIvariableName += strlist[0];
            UIfunctionName += strlist[1];
            Transform[] trans = obj.transform.GetComponentsInChildren<Transform>();
            for (int i = 0; i < trans.Length; ++i)
            {
                string[] names = trans[i].gameObject.name.Split('_');
                if (names.Length > 1 &&  names[0] == "Item")
                {
                    if (trans[i].GetComponent<NeedClone>() == null)
                    {
                        strUIItems += GenerateItem(trans[i].gameObject);
                        UIvariableName += "public " + trans[i].gameObject.name + " m_Item" + names[1] + " = new " + trans[i].gameObject.name + "();\n\t";
                        UIfunctionName += "GameObject " + trans[i].gameObject.name + "gameObject =" + "_OPLogicBase.GetGameObject(\"" + trans[i].gameObject.name + "\");\n\t\t";
                        UIfunctionName += "m_Item" + names[1] + ".setObj(" + trans[i].gameObject.name + "gameObject);\n\t\t";
                    }
                    //生成Item
                    else
                    {
                        NeedClone needClone = trans[i].GetComponent<NeedClone>();
                        strUIItems += GenerateItem(trans[i].gameObject);
                        if (needClone.m_itemNum > 1)
                        {

                            UIvariableName += "public " + trans[i].gameObject.name + "[] " + needClone.m_itemListName + "=new " + trans[i].gameObject.name + "[" + needClone.m_itemNum + "];\n\t";
                            UIfunctionName += "GameObject " + trans[i].gameObject.name + "gameObject =" + "_OPLogicBase.GetGameObject(\"" + trans[i].gameObject.name + "\");\n\t\t";
                            //UIvariableName += "protected List<" + trans[i].gameObject.name + "> " + needClone.m_itemListName + " = new List<" + trans[i].gameObject.name + ">();\n\t\t";
                            UIfunctionName += "" + trans[i].gameObject.name + " _" + trans[i].gameObject.name + "= new " + trans[i].gameObject.name + "();\n\t\t";
                            UIfunctionName += "_" + trans[i].gameObject.name + ".setObj(" + trans[i].gameObject.name + "gameObject);\n\t\t";
                            UIfunctionName += "_" + trans[i].gameObject.name + ".Setindex(0);\n\t\t";
                            UIfunctionName += needClone.m_itemListName + "[0]=_" + trans[i].gameObject.name + ";\n\t\t";
                            UIfunctionName += "for (int _HNum = 0; _HNum <" + needClone.m_hNum.ToString() + "; ++_HNum)\n\t\t{\n\t\t\t";
                            UIfunctionName += "for (int _WNum = 0; _WNum <" + needClone.m_WNum.ToString() + "; ++_WNum)\n\t\t\t{\n\t\t\t\t";
                            UIfunctionName += "if(_HNum==0 && _WNum==0){}\n\t\t\t\t";
                            UIfunctionName += "else\n\t\t\t\t{\n\t\t\t\t\t";
                            UIfunctionName += "GameObject itemgameobj = GameObject.Instantiate(" + trans[i].gameObject.name + "gameObject);\n\t\t\t\t\t";
                            UIfunctionName += "itemgameobj.transform.parent = " + trans[i].gameObject.name + "gameObject.transform.parent;\n\t\t\t\t\t";
                            UIfunctionName += "itemgameobj.transform.localScale = " + trans[i].gameObject.name + "gameObject.transform.localScale;\n\t\t\t\t\t";
                            UIfunctionName += "itemgameobj.transform.localPosition = new Vector3(" + trans[i].gameObject.name + "gameObject.transform.localPosition.x + _WNum * " + needClone.m_w.ToString() + ",\n\t\t\t\t\t";
                            UIfunctionName += "" + trans[i].gameObject.name + "gameObject.transform.localPosition.y - _HNum *" + needClone.m_h.ToString() + ", 0);\n\t\t\t\t\t";
                            UIfunctionName += "_" + trans[i].gameObject.name + "= new " + trans[i].gameObject.name + "();\n\t\t\t\t\t";
                            UIfunctionName += "_" + trans[i].gameObject.name + ".setObj(itemgameobj);\n\t\t\t\t\t";
                            UIfunctionName += "_" + trans[i].gameObject.name + ".Setindex(_HNum*" + needClone.m_WNum.ToString() + "+_WNum);\n\t\t\t\t\t";
                            UIfunctionName += needClone.m_itemListName + "[_HNum*" + needClone.m_WNum.ToString() + "+_WNum]=_" + trans[i].gameObject.name + ";\n\t\t\t\t";
                            UIfunctionName += "}\n\t\t\t";
                            UIfunctionName += "}\n\t\t";
                            UIfunctionName += "}\n\t\t";
                        }
                        else
                        {

                            UIvariableName += "public GameObject m_" + trans[i].gameObject.name + ";\n\t";
                            UIfunctionName += "m_" + trans[i].gameObject.name + "=" + "_OPLogicBase.GetGameObject(\"" + trans[i].gameObject.name + "\");\n\t\t";
                        }

                    }
                }

            }
            myUIstr = myUIstr.Replace("$ClsName", obj.name);
            myUIstr = myUIstr.Replace("$Prop", UIvariableName);
            myUIstr = myUIstr.Replace("$SaveData", UIfunctionName);
            myUIstr = myUIstr.Replace("$Items", strUIItems);
            myUIstr = myUIstr.Replace("$ItemVars", "");

            //Debug.LogWarning("节点数为------>" + trans.Length + "  ");

			string testFile = Application.dataPath + "/Script/IGame/View/UI//UIFile" + "/" + obj.name + ".cs";
            //输出CS文件
            using (var f = File.Create(testFile))
            {
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(myUIstr);
                f.Write(bytes, 0, bytes.Length);
                f.Close();
                Debug.LogWarning("生成代码成功！ ");

            }
        }
      
    }
    public static string GenerateItem(GameObject obj)
    {
        if (obj == null)
        {
            return "";
        }
        else
        {

            string mystr;
            //读取
            using (FileStream fsRead = new FileStream(Application.dataPath + "/UIFile/ItemGeneratemould.txt", FileMode.Open))
            {
                int fsLen = (int)fsRead.Length;
                byte[] heByte = new byte[fsLen];
                int r = fsRead.Read(heByte, 0, heByte.Length);
                mystr = System.Text.Encoding.UTF8.GetString(heByte);
                Debug.LogWarning("" + mystr);
                fsRead.Close();
            }
            string variableName = "";
            string functionName = "";
            for (int i = 0; i < obj.transform.childCount; ++i)
            {
                string[] tempstrlist = ParseNode(obj.transform.GetChild(i));

                variableName += tempstrlist[0];
                functionName += tempstrlist[1];
            }
           // string[] strlist = ParseNode(obj.transform);
           // variableName += strlist[0];
           // functionName += strlist[1];
            mystr = mystr.Replace("$ClsName", obj.name);
            mystr = mystr.Replace("$Prop", variableName);
            mystr = mystr.Replace("$SaveData", functionName);
            return mystr;
        }
    }
    public static string[] ParseNode(Transform child)
    {
        GameObject obj = child.gameObject;

        string[] names = obj.name.Split('_');
        string [] strlist=new string[2];
        string UIvariableName = "";
        string UIfunctionName = "";
        if (names.Length > 1)
        {
            string key = names[0];
            switch(key)
            {
                case "Label":
                    UIvariableName += "public UILabel " + obj.name + ";\n\t";
                    UIfunctionName += obj.name + "=" + "_OPLogicBase.GetGameObject(\"" + obj.name + "\").GetComponent<UILabel>();\n\t\t";
                    break;
                case "Sprite":
                    UIvariableName += "public UISprite " + obj.name + ";\n\t";
                    UIfunctionName += obj.name + "=" + "_OPLogicBase.GetGameObject(\"" + obj.name + "\").GetComponent<UISprite>();\n\t\t";
                    break;
                case "Btn":
                    UIvariableName += "public UIButton " + obj.name + ";\n\t";
                    UIfunctionName += obj.name + "=" + "_OPLogicBase.GetGameObject(\"" + obj.name + "\").GetComponent<UIButton>();\n\t\t";
                    break;
                case "Button":
                    UIvariableName += "public GameObject " + obj.name + ";\n\t";
                    UIfunctionName += obj.name + "=" + "_OPLogicBase.GetGameObject(\"" + obj.name + "\");\n\t\t";
                    break;
                case "Slider":
                    UIvariableName += "public UISlider " + obj.name + ";\n\t";
                    UIfunctionName += obj.name + "=" + "_OPLogicBase.GetGameObject(\"" + obj.name + "\").GetComponent<UISlider>();\n\t\t";
                    break;
                case "Texture":
                    UIvariableName += "public UITexture " + obj.name + ";\n\t";
                    UIfunctionName += obj.name + "=" + "_OPLogicBase.GetGameObject(\"" + obj.name + "\").GetComponent<UITexture>();\n\t\t";
                    break;
                case "Toggle":
                    UIvariableName += "public UIToggle " + obj.name + ";\n\t";
                    UIfunctionName += obj.name + "=" + "_OPLogicBase.GetGameObject(\"" + obj.name + "\").GetComponent<UIToggle>();\n\t\t";
                    break;
                case "GameObject":
                case "GameObj":
                    UIvariableName += "public GameObject " + obj.name + ";\n\t";
                    UIfunctionName += obj.name + "=" + "_OPLogicBase.GetGameObject(\"" + obj.name + "\");\n\t\t";
                    break;
                case "Widget":
                    UIvariableName += "public UIWidget " + obj.name + ";\n\t";
                    UIfunctionName += obj.name + "=" + "_OPLogicBase.GetGameObject(\"" + obj.name + "\").GetComponent<UIWidget>();\n\t\t";
                    break;
                case "Trans":
                    UIvariableName += "public Transform " + obj.name + ";\n\t";
                    UIfunctionName += obj.name + "=" + "_OPLogicBase.GetGameObject(\"" + obj.name + "\").transform;\n\t\t";
                    break;
                case "ScrollPage":
                    UIvariableName += "public UIScrollPage " + obj.name + ";\n\t";
                    UIfunctionName += obj.name + "=" + "_OPLogicBase.GetGameObject(\"" + obj.name + "\").GetComponent<UIScrollPage>();\n\t\t";
                    break;
                case "Input":
                    UIvariableName += "public UIInput " + obj.name + ";\n\t";
                    UIfunctionName += obj.name + "=" + "_OPLogicBase.GetGameObject(\"" + obj.name + "\").GetComponent<UIInput>();\n\t\t";
                    break;
                case "PlayTween":
                    UIvariableName += "public UIPlayTween " + obj.name + ";\n\t";
                    UIfunctionName += obj.name + "=" + "_OPLogicBase.GetGameObject(\"" + obj.name + "\").GetComponent<UIPlayTween>();\n\t\t";
                    break;
                case "ScrollView":
                    UIvariableName += "public UIScrollView " + obj.name + ";\n\t";
                    UIfunctionName += obj.name + "=" + "_OPLogicBase.GetGameObject(\"" + obj.name + "\").GetComponent<UIScrollView>();\n\t\t";
                    break;
                case "Grid":
                    UIvariableName += "public UIGrid " + obj.name + ";\n\t";
                    UIfunctionName += obj.name + "=" + "_OPLogicBase.GetGameObject(\"" + obj.name + "\").GetComponent<UIGrid>();\n\t\t";
                    break;
                case "ScrollBar":
                    UIvariableName += "public UIScrollBar " + obj.name + ";\n\t";
                    UIfunctionName += obj.name + "=" + "_OPLogicBase.GetGameObject(\"" + obj.name + "\").GetComponent<UIScrollBar>();\n\t\t";
                    break;
                case "Camera":
                    UIvariableName += "public Camera " + obj.name + ";\n\t";
                    UIfunctionName += obj.name + "=" + "_OPLogicBase.GetGameObject(\"" + obj.name + "\").GetComponent<Camera>();\n\t\t";
                    break;
                case "Table":
                    UIvariableName += "public UITable " + obj.name + ";\n\t";
                    UIfunctionName += obj.name + "=" + "_OPLogicBase.GetGameObject(\"" + obj.name + "\").GetComponent<UITable>();\n\t\t";
                    break;
                case "ProgressBar":
                    UIvariableName += "public UIProgressBar " + obj.name + ";\n\t";
                    UIfunctionName += obj.name + "=" + "_OPLogicBase.GetGameObject(\"" + obj.name + "\").GetComponent<UIProgressBar>();\n\t\t";
                    break;
            }
        }

        if (obj.transform.GetComponent<NeedClone>() != null )
        {
           // GenerateItem(obj);
        }
        else if (names.Length > 1 &&  names[0] == "Page")
        {

        }
        else
        {
            for (int i = 0; i < child.childCount; ++i)
            {
                string[] tempstrlist = ParseNode(child.GetChild(i));

                UIvariableName += tempstrlist[0];
                UIfunctionName += tempstrlist[1];
            }
        }
        strlist[0] = UIvariableName;
        strlist[1] = UIfunctionName;
        return strlist;
    }

    /*
      //classMarkName = resultName.Substring(resultName.IndexOf("_") + 1);
    public static void aaa()
    {
         string mystr;
        //读取
         using (FileStream fsRead = new FileStream(Application.dataPath + "/UIFile/UICodeGenerator.txt", FileMode.Open))
        {
            int fsLen=(int)fsRead.Length;
            byte[] heByte=new byte[fsLen];
            int r=fsRead.Read(heByte,0,heByte.Length);
            mystr=System.Text.Encoding.UTF8.GetString(heByte);
            Debug.LogWarning("" + mystr);
            fsRead.Close();
        }
        foreach (GameObject obj in UnityEditor.Selection.gameObjects)
        {
            if (null == obj)
            {
                continue;
            }
            if (obj == null)
            {
                return;
            }
            else
            {
                Transform[] trans = obj.transform.GetComponentsInChildren<Transform>();
                
                string variableName = "";
                string functionName = "";
                functionName += "if (_OPLogicBase.m_gameObj == null){\n\t\t\t";
                functionName += "GameObject uiobj = Resources.Load(\"UI/" + obj.name + "\") as GameObject;\n\t\t\t";
                functionName += "uiobj = GameObject.Instantiate(uiobj);\n\t\t\t";
                functionName += "GameObject rootObj = GameObject.Find(\"UI Root\");\n\t\t\t";
                functionName += "Transform cameraTran = rootObj.transform.FindChild(\"Camera\").transform;\n\t\t\t";
                functionName += "if (rootObj != null){\n\t\t\t\t";
                functionName += "uiobj.transform.parent = cameraTran;\n\t\t\t\t";
                functionName += "_OPLogicBase.m_gameObj = uiobj;\n\t\t\t";
                functionName += "}\n\t\t";
                functionName += "}\n\t\t";
                for (int i = 0; i < trans.Length; ++i)
                {
                    if (trans[i].GetComponent<NeedTogenerate>() != null)
                    {
                        NeedTogenerate needTogenerate = trans[i].GetComponent<NeedTogenerate>();
                        if (needTogenerate.m_generateTo==0)
                        {
                            variableName += "public GameObject " + trans[i].gameObject.name + ";\n\t";
                            functionName += trans[i].gameObject.name + "=" + "_OPLogicBase.GetGameObject(\"" + trans[i].gameObject.name + "\");\n\t\t";
                        }
                    }

                    if (trans[i].GetComponent<NeedClone>() != null)
                    {
                        GenerateItem(trans[i].gameObject);
                        NeedClone needClone = trans[i].GetComponent<NeedClone>();
                        variableName += "public " + trans[i].gameObject.name + "[] " + needClone.m_itemListName + "=new " + trans[i].gameObject.name + "[" + needClone.m_itemNum + "];\n\t";
                        functionName += "GameObject " + trans[i].gameObject.name + "gameObject =" + "_OPLogicBase.GetGameObject(\"" + trans[i].gameObject.name + "\");\n\t\t";
                        functionName += "" + trans[i].gameObject.name + " _" + trans[i].gameObject.name + "= new " + trans[i].gameObject.name + "();\n\t\t";
                        functionName += "_" + trans[i].gameObject.name + ".setObj(" + trans[i].gameObject.name + "gameObject);\n\t\t";
                        functionName += "_" + trans[i].gameObject.name + ".Setindex(0);\n\t\t";
                        functionName += needClone.m_itemListName +"[0]=_" + trans[i].gameObject.name + ";\n\t\t";
                       
                       
                        functionName += "for (int _HNum = 0; _HNum <" + needClone.m_hNum.ToString() + "; ++_HNum){\n\t\t";
                        functionName += "for (int _WNum = 0; _WNum <" + needClone.m_WNum.ToString() + "; ++_WNum){\n\t\t";
                        functionName += "if(_HNum==0 && _WNum==0){}\n\t\t";
                        functionName += "else{\n\t\t\t\t";
                        functionName += "GameObject itemgameobj = GameObject.Instantiate(" + trans[i].gameObject.name + "gameObject);\n\t\t\t\t";
                        functionName += "itemgameobj.transform.parent = " + trans[i].gameObject.name + "gameObject.transform.parent;\n\t\t\t\t";
                        functionName += "itemgameobj.transform.localScale = " + trans[i].gameObject.name + "gameObject.transform.localScale;\n\t\t\t\t";
                        functionName += "itemgameobj.transform.localPosition = new Vector3(" + trans[i].gameObject.name + "gameObject.transform.localPosition.x + _WNum * " + needClone.m_w.ToString() + ",\n\t\t\t\t";
                        functionName += "" + trans[i].gameObject.name + "gameObject.transform.localPosition.y - _HNum *" + needClone.m_h.ToString() + ", 0);\n\t\t\t\t";
                        functionName += "_" + trans[i].gameObject.name + "= new " + trans[i].gameObject.name + "();\n\t\t\t\t";
                        functionName += "_" + trans[i].gameObject.name + ".setObj(itemgameobj);\n\t\t\t\t";
                        functionName += "_" + trans[i].gameObject.name + ".Setindex(_HNum*" + needClone.m_WNum.ToString() + "+_WNum);\n\t\t\t\t";
                        functionName += needClone.m_itemListName + "[_HNum*" + needClone.m_WNum.ToString() + "+_WNum]=_" + trans[i].gameObject.name + ";\n\t\t\t";
                        functionName += "}\n\t\t";
                        functionName += "}\n\t\t";
                        functionName += "}\n\t\t";
                      
                    }
                }
               // BackpackItem _BackpackItem = new BackpackItem();
               // _BackpackItem.setObj();
                mystr = mystr.Replace("$ClsName", obj.name);
                mystr = mystr.Replace("$Prop", variableName);
                mystr = mystr.Replace("$SaveData", functionName);

                Debug.LogWarning("节点数为------>" + trans.Length + "  ");

                string testFile = Application.dataPath + "/UIFile/Generate" + "/" + obj.name + ".cs";
                //输出CS文件
                using (var f = File.Create(testFile))
                {
                    byte[] bytes = System.Text.Encoding.UTF8.GetBytes(mystr);
                    f.Write(bytes, 0, bytes.Length);
                    f.Close();
                    Debug.LogWarning("生成代码成功！ ");

                }
            }
            
        }
    }*/
}
