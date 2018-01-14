using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateStager : AESingleton<CreateStager>, IGameStageBase
{
    private AsyncOperation _sceneLoadOperation;
    private bool _secenIsLoadDone;

    public void Begin()
    {
        Debug.Log("CreateStage---Begin");
        loadScene();
    }

    public void Update()
    {
        if (!_secenIsLoadDone && _sceneLoadOperation != null && _sceneLoadOperation.isDone)
        {
            _secenIsLoadDone = true;
        }
    }

    public void FixedUpdate()
    {
    }


	public void LateUpdate()
	{
	}

    public void End()
    {
    }

    private void loadScene()
    {
        Debug.Log("加载场景 >>>> CreateStage ");
        _sceneLoadOperation = SceneManager.LoadSceneAsync("fight001", LoadSceneMode.Single);
    }
}