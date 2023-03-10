using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : IController
{
    private PlatformObjectView _platform;
    private float _spawnCountTime;
    private Vector3 _spawnPosition;
    private CameraController _cameraController;
    private float _spawnRangeMin;
    private float _spawnRangeMax;
    
    public PlatformGenerator(PlatformObjectView platformObjectView, Vector3 spawnPosition, CameraController cameraController, float spawnRangeMin, float spawnRangeMax)
    {
        _platform = platformObjectView;
        _spawnPosition = spawnPosition;
        _cameraController = cameraController;
        _spawnRangeMax = spawnRangeMax;
        _spawnRangeMin = spawnRangeMin;
        InitiatePlatforms();
        _cameraController.OnCameraMove += Execute;
    }

    private void InitiatePlatforms()
    {
        var countOfPlatforms = Random.Range(10, 15);
        for (int i = 0; i < countOfPlatforms; i++)
        {
            //_spawnPosition.y += Random.Range(3f, 5f);
            SpawnPlatform();
        }
    }

    public void Execute()
    {
        var countOfPlatforms = Random.Range(2,4);
        for (int i = 0; i < countOfPlatforms; i++)
        {
            SpawnPlatform();
        }
    }
    private void SpawnPlatform()
    {
        var localPlatformScale = _platform.Collider.bounds.max;
        _spawnPosition.y += Random.Range(_spawnRangeMin, _spawnRangeMax);
        _spawnPosition.x = Random.Range(_cameraController.XLeft+localPlatformScale.x, _cameraController.XRight-localPlatformScale.x);
        if (!_cameraController.CheckClampedObjectPos(_spawnPosition))
        {
            PlatformObjectView platformObjectView = Object.Instantiate(_platform, _spawnPosition, Quaternion.identity);
            platformObjectView.SetSprite();
        }
    }
    
    public void Dispose()
    {
        _cameraController.OnCameraMove -= Execute;
    }
}