using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateBase  
{

	public virtual bool OnEnter(List<object> datalist = null)
	{
		return true;
	}
	public virtual bool OnLeave(List<object> datalist = null)
	{
		return true;
	}
			
}
