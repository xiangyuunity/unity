using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerStateBase
{

	public override bool OnEnter(List<object> datalist = null)
	{
		if (datalist!=null)
		{
			int id = (int)datalist [0];
			PlayerBase player = RoleMgr.Instance.GetRoleById(id);
			Vector3 dir = (Vector3)datalist [1];
		}

		return true;
	}
	public override bool OnLeave(List<object> datalist = null)
	{
		return true;
	}
}
