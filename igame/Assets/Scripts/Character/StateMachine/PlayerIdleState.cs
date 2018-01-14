using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerStateBase
{
	public override bool OnEnter(List<object> datalist = null)
	{
		if (datalist != null && datalist [0] != null) 
		{
			int id = (int)datalist [0];
			RoleMgr.Instance.GetRoleById (id).PlayAni ("idle");
		}

		return true;
	}
	public override bool OnLeave(List<object> datalist = null)
	{
		Debug.Log ("idle value is leave");
		return true;
	}

}
