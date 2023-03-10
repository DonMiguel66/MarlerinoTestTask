using System;
using System.IO;
using UnityEngine;

public class ScoreSystemController: IController
{
    //private int _scores;
    private Scores _currentScores;
    private Scores _topScores;
    private ScoreSystemView _scoreSystemView;
    //public int Scores => _scores;

    private DataManager<Scores> _dataManager = new DataManager<Scores>();
    private readonly string _jsonScoresPath = Path.Combine(Application.persistentDataPath, "TopScores.json");

    public ScoreSystemController(ScoreSystemView scoreSystemView)
    {
        _currentScores = new Scores();
        _topScores = new Scores();
        Debug.Log("ScoreAwake");
        _scoreSystemView = scoreSystemView;
        if (!File.Exists(_jsonScoresPath)) return;
        _topScores = _dataManager.LoadFromJson(_jsonScoresPath);
        _scoreSystemView.TopScoresLabel.text = _topScores.value.ToString();
    }

    public void Execute()
    {
        _currentScores.value += 1;
        _scoreSystemView.ScoresLabel.text = _currentScores.value.ToString();
    }

    public void SaveScores()
    {
        if(_currentScores.value > _topScores.value)
        {
            _dataManager.SaveToJson(_currentScores, _jsonScoresPath);
            _scoreSystemView.TopScoresLabel.text = _currentScores.value.ToString();
        }
        _scoreSystemView.RestartGame();
    }

    public void Dispose()
    {
        _dataManager.SaveToJson(_currentScores,_jsonScoresPath);
    }
}

[Serializable]
public class Scores
{
    public int value;
}