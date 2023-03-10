using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class StartController : MonoBehaviour
    {
        [SerializeField] private StartScreenUIView _startScreenUIView;

        private void Awake()
        {
            _startScreenUIView.InitView(StartGame, Application.Quit);
        }

        private void StartGame()
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}