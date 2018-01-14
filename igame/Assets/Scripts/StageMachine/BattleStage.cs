
using UnityEngine;

public class BattleStage : AESingleton<BattleStage>, IGameStageBase
{
	private AsyncOperation _sceneLoadOperation;
	private bool _secenIsLoadDone;
	private GameObject scene;

	private SceneCameraCtl sceneCtl;

	public void Begin()
	{
		Init ();
	}

	private void Init()
	{
		sceneCtl = new SceneCameraCtl ();
		RoleMgr.Instance.CreateRole(RoleMgr.RoleType.Role , (obj)=>{
			sceneCtl.SetTransform(obj.transform);
		});
	}

	public void Update()
	{
		RoleMgr.Instance.Update ();
	}

	public void FixedUpdate()
	{
		
	}

	public void LateUpdate()
	{
		if (sceneCtl != null)
			sceneCtl.LateUpdate ();
	}

	public void End()
	{
	}

}