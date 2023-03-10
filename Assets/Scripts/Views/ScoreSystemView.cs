using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreSystemView : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoresLabel;
    [SerializeField] private TMP_Text _topScoresLabel;

    public TMP_Text ScoresLabel
    {
        get => _scoresLabel;
        set => _scoresLabel = value;
    }

    public TMP_Text TopScoresLabel
    {
        get => _topScoresLabel;
        set => _topScoresLabel = value;
    }

    public void RestartGame()
    {
        StartCoroutine(RestartGameCoroutine());
    }
    
    private IEnumerator RestartGameCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("MainScene");
    }
}