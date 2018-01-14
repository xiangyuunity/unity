using UnityEngine;

public class SceneCameraCtl
{
    private  Transform _cameraTF;

    private readonly GameObject _cameraWorldPoint;

    private float _distance; //摄像机的距离

	private Vector3 _initOffPos = new Vector3(0,8,-8);//new Vector3(10, 20, 15);
    private readonly bool _isFollowPlayer = true; //是否开始跟随
    private readonly Vector3 _offPos = new Vector3(0f, 0.5f, 0f);

    private Transform _playerTF;

    public SceneCameraCtl()
    {
		_cameraTF = Camera.main.transform;
//        _cameraWorldPoint = SystemManager.cameraWorldPoint;
    }

    public Transform PlayerTransform
    {
        set { _playerTF = value; }
    }

	public void SetTransform(Transform trans)
	{
		_playerTF = trans;
	}

    private void OnRoundEvent(Vector3 vet1, Vector3 vet2, Vector2 off)
    {
        var x = off.x;
        var y = off.y/2;
        _cameraTF.RotateAround(_playerTF.position, Vector3.up, 1*x);

        var originalPos = _cameraTF.position;
        var originalRotation = _cameraTF.rotation;


        _cameraTF.RotateAround(_playerTF.position, _playerTF.right, -1*y);

        var rx = _cameraTF.eulerAngles.x;

        if (rx < 10 || rx > 80)
        {
            _cameraTF.position = originalPos;
            _cameraTF.rotation = originalRotation;
        }

        _initOffPos = _cameraTF.position - _playerTF.position;
    }

    private void OnScrollEvent(Vector3 vet1, Vector3 vet2, float off)
    {
        _distance = _initOffPos.magnitude;
        _distance += off;
        _distance = Mathf.Clamp(_distance, 8, 30);

        _initOffPos = _initOffPos.normalized*_distance;
    }

    public void LateUpdate()
    {
        if (_playerTF == null)
            return;


        if (_isFollowPlayer)
        {
            _cameraTF.position = _playerTF.position + _initOffPos;

			_cameraTF.LookAt (_playerTF.position + _offPos);//+ _playerTF.forward);
//			_playerTF.position + _initOffPos+ _playerTF.forward;
//			var euler = _playerTF.forward;
//            euler.x = 0;
////            if (_cameraWorldPoint != null)
////                _cameraWorldPoint.transform.position = _cameraTF.position;
////
//			_cameraTF.transform.eulerAngles = euler;
        }
    }
}