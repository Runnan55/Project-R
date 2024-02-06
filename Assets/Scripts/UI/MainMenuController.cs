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
    [SerializeField] private Slider ambienceSlider;
    [SerializeField] private Slider musicSlider;

    public AudioSource MusicAudioSource;
    public AudioSource FxAudioSource;
    [SerializeField] private float startDelay = 2.0f;

    public Animator animator;

    private void Awake()
    {
        //GameManager.Instance.GetComponent<SoundManager>().SetVolumes();
    }

    private void Start()
    {
        OnBackClicked();
        animator.SetBool("Fade", true);
        GameManager.Instance.GetComponent<SoundManager>().PlayMusic(AudioMusic.MenuMusic, MusicAudioSource, true);      //olaaaaaaaaaaaaaaaaaaaaaaaa
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
        SoundManager.Instance.PlayMusic(AudioMusic.LevelMusic, MusicAudioSource, true);
        SceneManager.LoadScene(AppScenes.LOADING_SCENE);
    }

    public void OnOptionsClicked()
    {
        mainCanvas.SetActive(false);
        optionsCanvas.SetActive(true);
        //SoundManager.Instance.PlayAmbience(AudioAmbience.click, AmbienceAudioSource, false);
    }

    public void OnExitClicked()
    {
        //SoundManager.Instance.PlayAmbience(AudioAmbience.click, AmbienceAudioSource, false);
        Debug.Log("Game was exited");
        SoundManager.Instance.SaveVolumes();
        Application.Quit();
    }

    #endregion

    #region OptionsMenuMethods

    public void OnAmbienceVolumeChanged()
    {
        //SoundManager.Instance.SetAmbienceVolume(SliderToFaderFloatConvertion(ambienceSlider.value));
        //SoundManager.Instance.PlayAmbience(AudioAmbience.click, AmbienceAudioSource, false);
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
        //SoundManager.Instance.SaveVolumes();
        //SoundManager.Instance.PlayAmbience(AudioAmbience.click, AmbienceAudioSource, false);
        optionsCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }

    #endregion
    
}
