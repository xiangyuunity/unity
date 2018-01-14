using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIJoyStickCtl : UIJoyStick 
{
	private Vector3 coreInitPos;
	private Vector3 bgTransInitPos;

	public override void OnOpen(GameObject obj)
	{
		base.OnOpen (obj);

		coreInitPos = coreTrans.transform.position;
		bgTransInitPos = bgTrans.transform.position;

		UGUIEventTrigger.Get(dragArea.gameObject).AddEventListener(EventTriggerType.Drag, OnDrag);
		UGUIEventTrigger.Get(dragArea.gameObject).AddEventListener(EventTriggerType.PointerDown, BeginDrag);
		UGUIEventTrigger.Get(dragArea.gameObject).AddEventListener(EventTriggerType.PointerUp, EndDrag);
	}

	public override void OnShow()
	{
		base.OnShow ();
	}

	public override void OnDelet()
	{
		base.OnDelet ();

	}
	public override void OnHide()
	{
		base.OnHide ();
	}

	public override void OnUpdate()
	{
		base.OnUpdate ();
	}

	private void BeginDrag(BaseEventData baseData)
	{
		PointerEventData data = baseData as PointerEventData;
		coreTrans.position = data.position;
		bgTrans.position = data.position;
	}

	private void OnDrag(BaseEventData baseData)  
	{  
		PointerEventData data = baseData as PointerEventData;
		Vector3 curPos = data.position;
		Vector3 delVec = curPos - coreTrans.position;
		coreTrans.position = curPos;
	} 

	private void EndDrag(BaseEventData baseData)
	{
		coreTrans.position = coreInitPos;
		bgTrans.position = bgTransInitPos;
	}
}
