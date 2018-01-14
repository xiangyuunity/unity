

public enum StateType
{
	None,
	Idle,
	Dead,
	Move,
	Walk,
	Relive,
	Atk,
}
/// <summary>
/// 静态创建函数
/// </summary>
public class PlayerStateMgr:AESingleton<PlayerStateMgr>
{

	public StateMachine Create(int id)
	{
		StateMachine stateMgr = new StateMachine ();
		stateMgr.Create (id);
		return stateMgr;
	}
}




