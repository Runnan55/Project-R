using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;


//  ***** ENUMS *****

public enum AudioFx
{
    ejemplofx,
    ejemplofx2,
}

public enum AudioMusic
{
    MenuMusic,
    LevelMusic,
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private List<AudioClip> m_fxClips;
    [SerializeField] private List<AudioClip> m_musicClips;

    [SerializeField] private AudioSource fxAudioSource;
    [SerializeField] private AudioSource musicAudioSource;

    [SerializeField] private AudioMixer audioMixer;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else
            Destroy(gameObject);
            
        DontDestroyOnLoad(gameObject);
    }


    //  ***** PLAYS *****

    public void PlayAudioClip(AudioClip audioClip, AudioSource audioSource)
    {
        audioSource.PlayOneShot(audioClip);
    }

    public void PlayAudioSource(AudioSource audioSource)
    {
        audioSource.Play();
    }

    public void PlayFx(AudioFx audioFx, bool isLooping = true)
    {
        fxAudioSource.clip = m_fxClips[(int)audioFx];
        fxAudioSource.Play();
        SetAudioSourceLoop(fxAudioSource, isLooping);
    }
    
    public void PlayFx(AudioFx audioFx, AudioSource audioSource, bool isLooping = true)
    {
        audioSource.clip = m_fxClips[(int)audioFx];
        audioSource.Play();
        SetAudioSourceLoop(audioSource, isLooping);
    }

    public void PlayMusic(AudioMusic audioMusic, bool isLooping = true)
    {
        musicAudioSource.clip = m_musicClips[(int)audioMusic];
        musicAudioSource.Play();
        SetAudioSourceLoop(musicAudioSource, isLooping);
    }

    public void PlayMusic(AudioMusic audioMusic, AudioSource audioSource, bool isLooping = true)
    {
        audioSource.clip = m_musicClips[(int)audioMusic];
        audioSource.Play();
        SetAudioSourceLoop(audioSource, isLooping);
    }


    //  ***** AUDIOSOURCES *****

    public void SetAudioSourceLoop(AudioSource audioSource, bool isLoop)
    {
        audioSource.loop = isLoop;
    }

    public void StopAudioSource(AudioSource audioSource)
    {
        audioSource.Stop();
    }

    public void PauseAudioSource(AudioSource audioSource)
    {
        audioSource.Pause();
    }

    public void MuteAudioSource(AudioSource audioSource)
    {
        audioSource.mute = true;
    }

    public void UnMuteAudioSource(AudioSource audioSource)
    {
        audioSource.mute = false;
    }

    public void ToggleMuteAudioSource(AudioSource audioSource)
    {
        audioSource.mute = !audioSource.mute;
    }
    
    
    //  ***** VOLUMES *****

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void SetFxVolume(float volume)
    {
        audioMixer.SetFloat("FxVolume", volume);
    }

    public void SaveVolumes()
    {
        float tempValue;
        if (audioMixer.GetFloat("MusicVolume", out tempValue))
            PlayerPrefs.SetFloat("MusicVolume", tempValue);

        if (audioMixer.GetFloat("FxVolume", out tempValue))
            PlayerPrefs.SetFloat("FxVolume", tempValue);
            
        PlayerPrefs.Save();
    }

    public void SetVolumes()
    {
        audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
        audioMixer.SetFloat("FXVolume", PlayerPrefs.GetFloat("FXVolume"));

        PlayerPrefs.Save();
    }
}

/*
Para reproducir sonidos en otros scripts con el soundmanager:

 1. Añadir los sonidos en el inspector del gameobject del soundmanager

 2. Añadir los sonidos al enum al principio del script del soundmanager

 3. Referenciar en el script desde el que se quiere reproducir el sonido

    Ejs:
    
    //Audio
    public AudioSource MusicAudioSource; 

       void Start()
    {
        //Music AudioSource
        GameObject m_MusicAudioSource = GameObject.Find("MusicAudioSource");
        MusicAudioSource = m_MusicAudioSource.GetComponent<AudioSource>();
    }

    ///

    //Audio
    public AudioSource FxAudioSource; 

       void Start()
    {
        //Fx AudioSource
        GameObject m_FxAudioSource = GameObject.Find("FxAudioSource");
        FxAudioSource = m_FxAudioSource.GetComponent<AudioSource>();
    }

 4. Reproducir sonido (musicofx.nombresonido, audiosource, loop)

    Ejs:

    SoundManager.Instance.PlayMusic(AudioMusic.MenuMusic, MusicAudioSource, true);

    ///

    SoundManager.Instance.PlayFx(AudioFx.click, FxAudioSource, false);
*/