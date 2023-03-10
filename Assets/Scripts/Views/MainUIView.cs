using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainUIView : MonoBehaviour
{
    [SerializeField] private Button _pauseBtn;
    [SerializeField] private Button _firstContinueBtn;
    [SerializeField] private Button _secondContinueBtn;
    [SerializeField] private Button _menuBtn;
    [SerializeField] private Button _soundStateBtn;
    [SerializeField] private GameObject _pauseScreen;

    [Header("Sound Image")]
    [SerializeField] private Sprite _soundOn;
    [SerializeField] private Sprite _soundOff;
    [SerializeField] private Image _soundIconImage;
    
    public Button PauseBtn => _pauseBtn;
    public Button FirstContinueBtn => _firstContinueBtn;
    public Button SecondContinueBtn => _secondContinueBtn;
    public Button MenuBtn => _menuBtn;

    public GameObject PauseScreen => _pauseScreen;

    public Sprite SoundOn => _soundOn;

    public Sprite SoundOff => _soundOff;

    public Image SoundIconImage => _soundIconImage;

    public void Init(UnityAction setPause, UnityAction toMenu, UnityAction onSoundStateChange)
    {
        _pauseBtn.onClick.AddListener(setPause);
        _firstContinueBtn.onClick.AddListener(setPause);
        _secondContinueBtn.onClick.AddListener(setPause);
        _menuBtn.onClick.AddListener(toMenu);
        _soundStateBtn.onClick.AddListener(onSoundStateChange);
    }

    private void OnDestroy()
    {
        _pauseBtn.onClick.RemoveAllListeners();
        _firstContinueBtn.onClick.RemoveAllListeners();
        _secondContinueBtn.onClick.RemoveAllListeners();
        _menuBtn.onClick.RemoveAllListeners();
        _soundStateBtn.onClick.RemoveAllListeners();
    }
}
