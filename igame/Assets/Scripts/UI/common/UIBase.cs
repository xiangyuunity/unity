using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase 
{
//	public UIImageButton CloseBtn;

	protected GameObject uiObj;

	public virtual void OnOpen(GameObject obj)
	{
		uiObj = obj;
	
	}

	public virtual void OnShow()
	{
		
	}

	public virtual void OnDelet()
	{
	

	}
	public virtual void OnHide()
	{
		
	}

	public virtual void OnUpdate()
	{
		
	}

	public virtual void SetLayerIndex(int index)
	{
		uiObj.transform.SetSiblingIndex (index);
	}
		
}
