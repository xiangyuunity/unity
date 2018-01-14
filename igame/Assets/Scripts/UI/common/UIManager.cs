using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager : AESingleton<UIManager> {

	private  Dictionary<string ,UIBase> dic = new Dictionary<string, UIBase>();
	private  List<UIBase> uiList = new List<UIBase> ();

	private RectTransform uiCanvas;
	public void OpenUI(string path)
	{
		if (uiCanvas == null) {
			uiCanvas = GameObject.Find ("Canvas").GetComponent<RectTransform>();
		}
		if (dic.ContainsKey (path)) 
		{
			//显示panel∏∏
			if (uiList.Contains (dic [path]))
				uiList.Remove (dic [path]);
			uiList.Insert (uiList.Count, dic [path]);
			dic [path].OnShow ();
			dic [path].SetLayerIndex (uiList.Count - 1);
		} 
		else 
		{
			LoadUI (path , (obj)=>
				{
					
					GameObject uiPanel = obj as GameObject;
					GameObject uiObj = GameObject.Instantiate(uiPanel);
					uiObj.transform.SetParent(uiCanvas);
//					uiObj.transform.localScale = Vector3.one;
//					uiObj.transform.localPosition = Vector3.zero;

					UIBase uiCtl = GetUIBaseCtl(path);
					uiCtl.OnOpen(uiObj);
					dic.Add (path, uiCtl);
					uiList.Insert (uiList.Count, dic [path]);
					uiCtl.SetLayerIndex(uiList.Count - 1);
				});
		}

	}

	private UIBase GetUIBaseCtl(string path)
	{
		UIBase uiCtrl = null;
		switch (path) 
		{
		case "UIJoyStick":
				uiCtrl = new UIJoyStickCtl ();
			break;
		}
		return uiCtrl;
	}

	private void LoadUI(string path , Action<UnityEngine.Object> action)
	{
		string uiPath = "UI/" + path;
		AEResources.LoadSingle (uiPath, (obj) => {
			action(obj);
		});
	}

	public void CloseUI(string path , bool isDelete)
	{
		if (isDelete)
			DeletUI (path);
		else
			HideUI (path);
	}

	private void HideUI(string path)
	{
		if (dic.ContainsKey (path)) {
			dic [path].OnHide ();
		}
		if (uiList.Contains (dic [path]))
			uiList.Remove (dic [path]);
		
		
	}

	private  void DeletUI(string path)
	{
		if (dic.ContainsKey (path)) {
			dic [path].OnDelet ();
			dic.Remove (path);
		}
		if (uiList.Contains (dic [path]))
			uiList.Remove (dic [path]);
	}

	public void Update()
	{
		foreach (var item in dic) {
			item.Value.OnUpdate ();
		}
	}
}
