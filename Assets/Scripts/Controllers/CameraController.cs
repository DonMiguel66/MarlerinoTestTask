using System;
using UnityEngine;


public class CameraController : IController
{
    private Vector2 _cameraToPlayer;
    private Vector2 _clampedPos;
    private float _distance;
    private float _xLeft;
    private float _xRight;
    private float _yTop;
    private float _yBot;
    private Vector3 _leftBot;
    private Vector3 _rightTop;
    
    private Transform _playerTransform;
    private Transform _cameraTransform;
    private Camera _camera;
    
    public bool HasLeftContact { get; private set; }
    public bool HasRightContact { get; private set; }

    public float XLeft => _xLeft;

    public float XRight => _xRight;

    public float YTop => _yTop;
    public float YBot => _yBot;

    public Vector2 ClampedPos => _clampedPos;

    public Camera Camera => _camera;

    public Action OnCameraMove { get; set; }

    public CameraController(Transform player, Camera camera)
    {
        _camera = camera;
        _playerTransform = player;
        _cameraTransform = camera.transform;
        
        _distance = -Vector3.Project(_cameraToPlayer, _camera.transform.forward).y;
        _leftBot = _camera.ViewportToWorldPoint(new Vector3(0, 0, _distance));
        _rightTop = _camera.ViewportToWorldPoint(new Vector3(1, 1, _distance));
        
        _xLeft = _leftBot.x;
        _xRight = _rightTop.x;
        _yTop = _rightTop.y;
        _yBot = _leftBot.y;
        
    }

    public void Execute()
    {
        _cameraToPlayer = _playerTransform.position - _camera.transform.position;

        _clampedPos = _playerTransform.position;
        _clampedPos.x = Mathf.Clamp(ClampedPos.x, XLeft, XRight);
        
        //Debug.Log(ClampedPos);
        if (_playerTransform.position.y > _cameraTransform.position.y)
        {
            _cameraTransform.position = new Vector3(_cameraTransform.position.x, _playerTransform.position.y, _cameraTransform.position.z); 
        }
        CheckClampedPlayerPos();
    }

    private void CheckClampedPlayerPos()
    {
        HasLeftContact = false;
        HasRightContact = false;

        if (ClampedPos.x >= XRight)
            HasRightContact = true;
        if (ClampedPos.x <= XLeft)
            HasLeftContact = true;
    }
    
    public bool CheckClampedObjectPos(Vector3 clampedPos)
    {
        clampedPos.x = Mathf.Clamp(clampedPos.x, XLeft, XRight);
        if (clampedPos.x >= XRight)
            return true;
        if (clampedPos.x <= XLeft)
            return true;
        return false;
    }

    public void Dispose()
    {
    }
}