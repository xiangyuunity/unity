//using UnityEngine;
//using UnityEngine.SceneManagement;
//
//public class GameStage : AESingleton<GameStage>, IGameStageBase
//{
//    private GameObject _projector;
//    private AsyncOperation _sceneLoadOperation;
//    private bool _secenIsLoadDone;
//
//    public void Begin()
//    {
//        loadScene();
//    }
//
//    public void Update()
//    {
//        if (!_secenIsLoadDone && _sceneLoadOperation != null && _sceneLoadOperation.isDone)
//        {
//            _secenIsLoadDone = true;
//            AEResources.LoadSingle("ArtRes/scene/Projector/Projector", OnPrjectorLoadDone);
//        }
//        if (_sceneLoadOperation != null && _sceneLoadOperation.isDone)
//        {
//       
//        }
//
//       
//    }
//
//    public void FixedUpdate()
//    {
//        
//    }
//
//	public void LateUpdate()
//	{
//	}
//
//    public void End()
//    {
//        
//    }
//
//    private void loadScene()
//    {
//        Debug.Log("加载场景 >>>>  ");
//        _sceneLoadOperation = SceneManager.LoadSceneAsync("battle_001", LoadSceneMode.Single);
//    }
//
//    private void OnPrjectorLoadDone(Object obj)
//    {
//        _projector = Object.Instantiate(obj as GameObject);
//    }
//}