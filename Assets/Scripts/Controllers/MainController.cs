using System;
using UnityEngine;
using UnityEngine.Serialization;

public class MainController : MonoBehaviour, IDisposable
{
    [SerializeField] private float _horizontalPlayerDirectionSpeed;
    [SerializeField] private float _verticalPlayerForceSpeed;
    [SerializeField] private PlayerObjectView _playerView;
    [SerializeField] private RestartZoneView _restartZoneView;
    [SerializeField] private Camera _camera;
    [SerializeField] private PlatformObjectView _platformObjectView;
    [SerializeField] private Transform _platformSpawnPosition;
    [SerializeField] private ScoreSystemView _scoreSystemView;
    [FormerlySerializedAs("_uiView")] [SerializeField] private MainUIView mainUIView;
    [SerializeField] private SpriteAnimatorConfig _playerConfig;
    [SerializeField] private AudioSource _mainAudioSource;

    [Range(1f, 10f)] [SerializeField] private float _spawnRangeMin;
    [Range(1f, 10f)] [SerializeField] private float _spawnRangeMax;
    
    private PlayerController _playerController;
    private SpriteAnimatorController _playerAnimator;
    private CameraController _cameraController;
    private PlatformGenerator _platformGenerator;
    private ScoreSystemController _scoreSystemController;
    private MainUIController _mainUIController;
    void Awake()
    {
        _playerConfig = Resources.Load<SpriteAnimatorConfig>("PlayerAnimCfg");
        if (_playerConfig)
            _playerAnimator = new SpriteAnimatorController(_playerConfig);
        //_playerAnimator.StartAnimation(_playerView.SpriteRenderer, PlayerAnimState.Idle, true);
        _mainUIController = new MainUIController(mainUIView,_mainAudioSource);
        _scoreSystemController = new ScoreSystemController(_scoreSystemView);
        _cameraController = new CameraController(_playerView.Transform, _camera);
        _playerController = new PlayerController(_playerView,_playerAnimator,_restartZoneView, _horizontalPlayerDirectionSpeed, _verticalPlayerForceSpeed, _cameraController, _scoreSystemController);
        _platformGenerator = new PlatformGenerator(_platformObjectView, _platformSpawnPosition.position, _cameraController,_spawnRangeMin, _spawnRangeMax);
    }

    private void FixedUpdate()
    {
        _playerController.Execute();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
