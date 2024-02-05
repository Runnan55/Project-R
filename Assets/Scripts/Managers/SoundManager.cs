using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public enum AudioFX
{
    BallHit,
    ScoredGoal,
    VictoryCheers,
    DefeatBoss
}

public enum AudioMusic
{
    MenuMusic,
    LevelMusic
}

public enum AudioAmbience
{
    start,
    click,
    death,
    attack,
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private List<AudioClip> m_fxClips;
    [SerializeField] private List<AudioClip> m_musicClips;
    [SerializeField] private List<AudioClip> m_fxAmbienceClips;

    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource ambienceAudioSource;

    [SerializeField] private AudioMixer audioMixer;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void PlayAudioClip(AudioClip audioClip, AudioSource audioSource)
    {
        audioSource.PlayOneShot(audioClip);
    }

    public void PlayAudioSource(AudioSource audioSource)
    {
        audioSource.Play();
    }

    public void PlayFx(AudioFX audioFX, AudioSource audioSource)
    {
        audioSource.PlayOneShot(m_fxClips[(int)audioFX]);
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

    public void PlayAmbience(AudioAmbience audioAmbience, bool isLooping = true)
    {
        ambienceAudioSource.clip = m_fxAmbienceClips[(int)audioAmbience];
        ambienceAudioSource.Play();
        SetAudioSourceLoop(ambienceAudioSource, isLooping);
    }

    public void PlayAmbience(AudioAmbience audioAmbience, AudioSource audioSource, bool isLooping = true)
    {
        audioSource.clip = m_fxAmbienceClips[(int)audioAmbience];
        audioSource.Play();
        SetAudioSourceLoop(audioSource, isLooping);
    }

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

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void SetAmbienceVolume(float volume)
    {
        audioMixer.SetFloat("AmbienceVolume", volume);
    }

    public void SetFXVolume(float volume)
    {
        audioMixer.SetFloat("FXVolume", volume);
    }

    public void SaveVolumes()
    {
        float tempValue;
        if (audioMixer.GetFloat("MusicVolume", out tempValue))
            PlayerPrefs.SetFloat("MusicVolume", tempValue);
        if (audioMixer.GetFloat("AmbienceVolume", out tempValue))
            PlayerPrefs.SetFloat("AmbienceVolume", tempValue);
        if (audioMixer.GetFloat("FXVolume", out tempValue))
            PlayerPrefs.SetFloat("FXVolume", tempValue);
        PlayerPrefs.Save();
    }

    public void SetVolumes()
    {
        audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
        audioMixer.SetFloat("AmbienceVolume", PlayerPrefs.GetFloat("AmbienceVolume"));
        audioMixer.SetFloat("FXVolume", PlayerPrefs.GetFloat("FXVolume"));

        PlayerPrefs.Save();
    }
}
