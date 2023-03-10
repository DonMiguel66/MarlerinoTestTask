using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIController : IDisposable
{
    private MainUIView _mainUIView;
    private AudioSource _audioSource;

    private bool _onPause;

    public MainUIController(MainUIView mainUIView, AudioSource audioSource)
    {
        _mainUIView = mainUIView;
        _audioSource = audioSource;
        _mainUIView.Init(SetPause,SetMenu,SoundStateChange);
        if (PlayerPrefs.GetInt("IsMusicPlaying") == 1)
        {
            PlayMusic();
            _mainUIView.SoundIconImage.sprite = _mainUIView.SoundOn;
        }
        else
        {
            _mainUIView.SoundIconImage.sprite = _mainUIView.SoundOff;
        }
    }

    private void SetPause()
    {
        if (_onPause)
        {
            Time.timeScale = 1;
            _onPause = false;
            _mainUIView.PauseScreen.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            _onPause = true;
            _mainUIView.PauseScreen.SetActive(true);
        }
    }

    private void SetMenu()
    {
        SceneManager.LoadScene("StartScene");
    }

    private void PlayMusic()
    {
        _audioSource.Play();
        _mainUIView.SoundIconImage.sprite = _mainUIView.SoundOn;
        Debug.Log("Music Playing");
    }

    private void StopMusic()
    {
        _audioSource.Stop();
        _mainUIView.SoundIconImage.sprite = _mainUIView.SoundOff;
        Debug.Log("Music Stopped");
    }
    
    public void SoundStateChange()
    {
        if (PlayerPrefs.GetInt("IsMusicPlaying") == 1)
        {
            StopMusic();
            PlayerPrefs.SetInt("IsMusicPlaying", 0);
        }
        else if(PlayerPrefs.GetInt("IsMusicPlaying") == 0)
        {
            PlayerPrefs.SetInt("IsMusicPlaying", 1);
            PlayMusic();
        }
        else
        {
            return;
        }
        Debug.Log(PlayerPrefs.GetInt("IsMusicPlaying"));
    }
    
    public void Dispose()
    {
        
    }
}
