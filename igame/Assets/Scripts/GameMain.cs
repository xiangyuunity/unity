using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour 
{
	private float nowTime= 0 ;

	private bool isChange = false;
	// Use this for initialization
	void Start () 
	{
		GameStageMachine.Instance.Init ();
//		UIManager.Instance.OpenUI("UIJoyStick");
	}
	
	// Update is called once per frame
	void Update () 
	{
		GameStageMachine.Instance.Update ();
	}

	void FixedUpdate()
	{
		GameStageMachine.Instance.FixedUpdate ();
		TimerManager.Instance.FixedUpdate ();
	}

	void LateUpdate()
	{
		GameStageMachine.Instance.LateUpdate ();
	}
}
