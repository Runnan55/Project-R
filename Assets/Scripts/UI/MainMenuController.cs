using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainCanvas, optionsCanvas;
    [SerializeField] private Slider fxSlider;
    [SerializeField] private Slider musicSlider;

    public AudioSource MusicAudioSource;
    public AudioSource FxAudioSource;
    [SerializeField] private float startDelay = 2.0f;

    public Animator animator;

    private void Awake()
    {
        GameManager.Instance.GetComponent<SoundManager>().SetVolumes();
    }

    private void Start()
    {
        OnBackClicked();
        animator.SetBool("Fade", true);
        GameManager.Instance.GetComponent<SoundManager>().PlayMusic(AudioMusic.menuMusic, MusicAudioSource, true);
    }

    #region MainMenuMethods

    public void OnPlayClicked()
    {
        animator.SetBool("Fade", false);
        SoundManager.Instance.PlayFx(AudioFx.start, FxAudioSource, false);
        Invoke("LoadScene", startDelay);
    }

    private void LoadScene()
    {
        SoundManager.Instance.PlayMusic(AudioMusic.levelMusic, MusicAudioSource, true);
        SoundManager.Instance.PlayMusic(AudioMusic.ambience, MusicAudioSource, true);
        SceneManager.LoadScene(AppScenes.Level);
    }

    public void OnOptionsClicked()
    {
        mainCanvas.SetActive(false);
        optionsCanvas.SetActive(true);
        GameManager.Instance.GetComponent<SoundManager>().PlayFx(AudioFx.click, FxAudioSource, false);
    }

    public void OnExitClicked()
    {
        GameManager.Instance.GetComponent<SoundManager>().PlayFx(AudioFx.click, FxAudioSource, false);
        Debug.Log("Game was exited");
        SoundManager.Instance.SaveVolumes();
        Application.Quit();
    }

    #endregion

    #region OptionsMenuMethods

    public void OnFxVolumeChanged()
    {
        GameManager.Instance.GetComponent<SoundManager>().SetFxVolume(SliderToFaderFloatConvertion(fxSlider.value));
        GameManager.Instance.GetComponent<SoundManager>().PlayFx(AudioFx.click, FxAudioSource, false);
    }

    public void OnMusicVolumeChanged()
    {
        SoundManager.Instance.SetMusicVolume(SliderToFaderFloatConvertion(musicSlider.value));
    }

    private float SliderToFaderFloatConvertion(float initialValue)
    {
        return initialValue * 80 - 80;
    }
    
    public void OnBackClicked()
    {
        GameManager.Instance.GetComponent<SoundManager>().SaveVolumes();
        GameManager.Instance.GetComponent<SoundManager>().PlayFx(AudioFx.click, FxAudioSource, false);
        optionsCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }

    #endregion
    
}
