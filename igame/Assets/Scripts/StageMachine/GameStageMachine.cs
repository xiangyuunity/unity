public enum GameStage
{
    LOGIN, //登录
    CREATE, //创角
    BATTLE //战斗
}

public class GameStageMachine:AESingleton<GameStageMachine>
{
    protected IGameStageBase currentStage;

    public IGameStageBase CurrStage
    {
        get { return currentStage; }
    }

    public virtual void Init()
    {
//        SceneMgr.Instance.Init();
		ChangeStage(GameStage.BATTLE);
    }

    public IGameStageBase GetStage(GameStage type)
    {
        currentStage = null;
        switch (type)
        {
            case GameStage.LOGIN:
                currentStage = LoginStage.Instance;
                break;
            case GameStage.CREATE:
                break;
			case GameStage.BATTLE:
				currentStage = BattleStage.Instance;
                break;
        }
        return currentStage;
    }

    public virtual void ChangeStage(GameStage type)
    {
        if (currentStage != null)
            currentStage.End();

        currentStage = GetStage(type);
        currentStage.Begin();
    }

    public void Update()
    {
        if (currentStage != null)
            currentStage.Update();
    }

    public void FixedUpdate()
    {
        if (currentStage != null)
            currentStage.FixedUpdate();
    }

	public void LateUpdate()
	{
		if (currentStage != null)
			currentStage.LateUpdate ();
	}

    public virtual void Destroy()
    {
    }
}