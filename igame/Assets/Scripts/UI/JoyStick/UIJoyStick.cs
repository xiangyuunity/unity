using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;  

public class UIJoyStick : UIBase {

	protected Transform bgTrans;
	protected Transform coreTrans;
	protected Transform dragArea;

	public override void OnOpen(GameObject obj)
	{
		
		base.OnOpen (obj);
		bgTrans = uiObj.transform.Find ("bg");
		coreTrans = uiObj.transform.Find ("core");
		dragArea = uiObj.transform.Find ("DragArea");

	}
		

	public override void OnShow()
	{

	}

	public override void OnDelet()
	{


	}
	public override void OnHide()
	{

	}

	public override void OnUpdate()
	{

	}
}
