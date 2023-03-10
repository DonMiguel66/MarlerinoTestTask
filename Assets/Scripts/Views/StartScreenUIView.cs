using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StartScreenUIView : MonoBehaviour
{
    [SerializeField] private Button _playBtn;
    [SerializeField] private Button _quitBtn;

    public void InitView(UnityAction onStart, UnityAction onQuit)
    {
        _playBtn.onClick.AddListener(onStart);
        _quitBtn.onClick.AddListener(onQuit);
    }
}
