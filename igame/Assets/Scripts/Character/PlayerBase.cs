using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase  
{
	protected int id;
	private Animation ani;
	protected GameObject roleObj;
	protected StateMachine fsm;

	public virtual void  Init(GameObject obj , int num)
	{
		id = num;
		roleObj = obj;
		ani = obj.GetComponent<Animation> ();
		fsm = PlayerStateMgr.Instance.Create (id);
	}

	public virtual void ChangeState()
	{
		fsm.ChangeState (StateType.Move , new List<object>{id});
	}

	public virtual void PlayAni(string name , float crossTime = 0.2f)
	{
		if(ani!=null)
		ani.CrossFade(name , crossTime);
	}

	public virtual void Move(Vector2 dir , float speed)
	{
//		roleObj.transform.position =  Vector3 (dir.x, dir.y , 0);
	}
}
