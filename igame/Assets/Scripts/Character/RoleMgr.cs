using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoleMgr:AESingleton<RoleMgr>
{
	public enum RoleType
	{
		Role,
		Monster,
	}

	public delegate void PlayAni ();
	public event PlayAni PlayAniEvent;

	public delegate void Move (Vector2 vec);
	public event Move MoveEvent;

	private int id = 0;

	private Dictionary<int , PlayerBase> dic = new Dictionary<int, PlayerBase> ();

	private string rolePath = "ArtRes/model/skinned_mesh/role/Ninja/Ninja_chick_black";

	private MyPlayer myPlayer;

	public void CreateRole(RoleType roleType , Action<GameObject> action = null)
	{
		int id = GetId ();
		PoolMgr.Instance.GetNew (rolePath, (obj) => {
			if(id == 1)
			{
				myPlayer = new MyPlayer();
				myPlayer.Init(obj , id);
				if(action!=null)
					action(obj);
				return;
			}

			PlayerBase role = new PlayerBase();	
			dic.Add(id , role);
			role.Init(obj , id);
			if(action!=null)
				action(obj);
		});
	}

	public PlayerBase GetRoleById(int id)
	{
		if (id == 1)
			return myPlayer;
		if(dic.ContainsKey(id))
			return dic [id];
		return null;
	}


	private int GetId()
	{
		id += 1;
		return id;
	}


	public void Update()
	{
		
	}
}
