using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
	private Dictionary<StateType , PlayerStateBase> stateDic = new Dictionary<StateType, PlayerStateBase> (); 

	private StateType curStateType = StateType.None;

	private PlayerIdleState idleState;
	private PlayerDeadState deadState;
	private PlayerMoveState moveState;

	private static List<StateType> allState = new List<StateType>
	{
		StateType.Idle,
		StateType.Dead,
		StateType.Move,
		StateType.Walk,
		StateType.Relive,
		StateType.Atk,
	};
	private readonly Dictionary<StateType, List<StateType>> CheckChangeDic = new Dictionary<StateType, List<StateType>>
	{
		{StateType.Idle, allState},
		{StateType.Atk , allState},
	};

	#region 创建状态机
	public void Create(int id)
	{
		stateDic.Clear ();

		idleState = new PlayerIdleState ();
		deadState = new PlayerDeadState ();
		moveState = new PlayerMoveState ();

		SetStateDic (StateType.Idle, idleState);
		SetStateDic (StateType.Dead, deadState);
		SetStateDic (StateType.Move, moveState);

		ChangeState (StateType.Idle , new List<object>{id});
	}

	private void SetStateDic(StateType stateType , PlayerStateBase state)
	{
		if (!stateDic.ContainsKey (stateType)) 
		{
			stateDic.Add (stateType , state);
		}
	}
	#endregion

	#region 切换状态
	public void ChangeState(StateType stateType , List<object> datalist = null)
	{
		if (curStateType != StateType.None) {
			PlayerStateBase curState = GetState (curStateType);
			if (curState != null) {
				bool isLeave = curState.OnLeave (datalist);
				if (!isLeave)
					return;
			}

			PlayerStateBase state = GetNextState (curStateType , stateType);
			if (state!=null) 
			{
				state.OnEnter ();
				curStateType = stateType;
			}
		} 
		else
		{
			curStateType = stateType;
			PlayerStateBase curState = GetState (curStateType);
			if (curState != null)
				curState.OnEnter (datalist);
		}


	}
	private PlayerStateBase GetNextState(StateType curState , StateType nextState)
	{
		List<StateType> list = GetTransferStates (curState);

		if (list.Contains (nextState)) 
		{
			return GetState (nextState);
		}
		return null;
	}


	private List<StateType> GetTransferStates(StateType state)
	{
		List<StateType> list = null;
		var isHas = CheckChangeDic.TryGetValue(state, out list);
		if (isHas)
		{
			return list;
		}
		return null;
	}
	#endregion

	#region 获得当前的状态
	public PlayerStateBase GetState(StateType state)
	{
		if (stateDic.ContainsKey (state))
			return stateDic [state];
		return null;
	}
	#endregion
}