using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyPlayer:PlayerBase
{
	public override void Init(GameObject obj , int num)
	{
		base.Init (obj, num);
		roleObj.transform.position = Vector3.zero;
		RoleMgr.Instance.MoveEvent += MoveEvent;
	}

	void MoveEvent (Vector2 dir)
	{
		fsm.ChangeState (StateType.Move , new List<object>{id , dir});

//		roleObj.transform.position = 
	}

}

