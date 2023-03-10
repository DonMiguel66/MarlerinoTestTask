using UnityEngine;

public class PlayerController: IController
{
    private float _horizontalDirectionSpeed;
    private float _verticalDirectionForceSpeed;
    private Rigidbody2D _rigidBody;
    private Vector2 _moveDirection;

    private PlayerObjectView _playerView;
    private RestartZoneView _restartZoneView;
    private SpriteRenderer _playerSpriteRenderer;
    private readonly ContactPooler _contactPooler;
    private CameraController _cameraController;
    private ScoreSystemController _scoreSystemController;
    private SpriteAnimatorController _playerAnimatorController;
    private Camera _mainCamera;
    private bool _gameFailed;
    public PlayerController(PlayerObjectView playerView, SpriteAnimatorController playerAnimatorController,RestartZoneView restartZoneView,float horizontalDirectionSpeed, float verticalDirectionForceSpeed, CameraController cameraController, ScoreSystemController scoreSystemController)
    {
        _playerView = playerView;
        _playerAnimatorController = playerAnimatorController;
        _playerSpriteRenderer = playerView.SpriteRenderer;
        _rigidBody = playerView.RigidBody;
        _horizontalDirectionSpeed = horizontalDirectionSpeed;
        _verticalDirectionForceSpeed = verticalDirectionForceSpeed;
        _restartZoneView = restartZoneView;
        _restartZoneView.OnRestartZoneContact += OnGameFailed;
        _contactPooler = new ContactPooler(_playerView.Collider);
        _cameraController = cameraController;
        _scoreSystemController = scoreSystemController;
        _mainCamera = _cameraController.Camera;
        _playerView.RigidBody.velocity = Vector2.up * _verticalDirectionForceSpeed;
    }

    public void Execute()
    {
        _contactPooler.Execute();
        //_cameraController.SetPlayerInCameraViewport();
        _cameraController.Execute();
        Move();
        AnimationChange();
    }
    
    private void Move()
    {
#if UNITY_ANDROID
        if (Input.acceleration.x < 0)
        {
            _playerSpriteRenderer.flipX = false;
        }
        if (Input.acceleration.x > 0)
        {
            _playerSpriteRenderer.flipX = true;     
        }
        _rigidBody.velocity = new Vector2(Input.acceleration.x * _horizontalDirectionSpeed, _rigidBody.velocity.y); 
#endif

#if UNITY_EDITOR
        if (Input.GetAxis("Horizontal")<0)
        {
            _playerSpriteRenderer.flipX = true;
        }
        if (Input.GetAxis("Horizontal")>0)
        {
            _playerSpriteRenderer.flipX = false;
        }
        _moveDirection = _playerView.transform.right * Input.GetAxis("Horizontal") + _playerView.transform.forward;
        _moveDirection *= _horizontalDirectionSpeed;
        _rigidBody.velocity = _moveDirection + Vector2.up * _rigidBody.velocity.y;
#endif
        if (_contactPooler.IsGrounded)
        {
            if(_playerView.RelativeVelocity > 0)
            {
                _playerView.RigidBody.velocity = Vector2.up * _verticalDirectionForceSpeed;
                _cameraController.OnCameraMove?.Invoke();
            }
        }
        if(_cameraController.HasRightContact)
            _playerView.Transform.position = new Vector2(_cameraController.XLeft, _cameraController.ClampedPos.y);
        if(_cameraController.HasLeftContact)
            _playerView.Transform.position = new Vector2(_cameraController.XRight, _cameraController.ClampedPos.y);
        
        if(_playerView.RigidBody.velocity.y>0)
        {
            //_playerAnimatorController.StartAnimation(_playerView.SpriteRenderer, PlayerAnimState.Jump, true);
            _scoreSystemController.Execute();
        }
    }

    private void AnimationChange()
    {
        _playerAnimatorController.Execute();
        if(!_gameFailed)
            _playerAnimatorController.StartAnimation(_playerView.SpriteRenderer, _playerView.RigidBody.velocity.y > 0 ? PlayerAnimState.Jump : PlayerAnimState.Fall, true);
    }
    private void OnGameFailed()
    {
        _gameFailed = true;
        _playerView.RigidBody.isKinematic = true;
        _playerView.RigidBody.velocity = Vector2.zero;
        _playerAnimatorController.StartAnimation(_playerView.SpriteRenderer, PlayerAnimState.Failed, false);
        _scoreSystemController.SaveScores();
    }

    public void Dispose()
    {
        _restartZoneView.OnRestartZoneContact -= OnGameFailed;
    }
}
