using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance => instance;

    [SerializeField] private Sound[] SFXSounds;
    [SerializeField] private Sound[] MusicSounds;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    public GameConfig Config => gameConfig;

    private GameConfig gameConfig;
    void Awake()
    {
        musicSource = transform.Find("MusicSource").GetComponent<AudioSource>();
        SFXSource = transform.Find("SFXSource").GetComponent<AudioSource>();
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            if (!File.Exists(GameConfig.GetPath()))
            {
                gameConfig = ScriptableObject.CreateInstance<GameConfig>();
                GameConfig.SaveToFile(gameConfig);
            }
            else
            {
                gameConfig = GameConfig.LoadFromFile();
            }
        }

    }

    private void Start()
    {

        musicSource.volume = gameConfig.MusicVolume;
        SFXSource.volume = gameConfig.SFXVolume;

        musicSource.mute = gameConfig.IsMuteMusic;
        SFXSource.mute = gameConfig.IsMuteSFX;

        PlayMusic("Theme", true);
    }

    public void PlayMusic(string name, bool loop)
    {
        Sound sound = Array.Find(MusicSounds, s => s.name == name);
        if(sound == null)
        {
            Debug.Log(string.Format("Sound {0} Not Found", name));
        }
        else
        {
            musicSource.clip = sound.clip;
            musicSource.loop = loop;
            musicSource.Play();
        }
    }

    public void StopMusic(string name)
    {
        Sound sound = Array.Find(MusicSounds, s => s.name == name);
        if (sound == null)
        {
            Debug.Log(string.Format("Sound {0} Not Found", name));
        }
        else
        {
            musicSource?.Stop();
        }
    }

    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(SFXSounds, s => s.name == name);
        if (sound == null)
        {
            Debug.Log(string.Format("Sound {0} Not Found", name));
        }
        else
        {
            SFXSource.PlayOneShot(sound.clip);
        }
    }

    public void SetMusicVolume(float value)
    {
        gameConfig.MusicVolume = value;
        musicSource.volume = value;
    }

    public void SetSFXVolume(float value)
    {
        gameConfig.SFXVolume = value;
        SFXSource.volume = value;
    }
    public void ToggleMusic()
    {
        gameConfig.IsMuteMusic = !gameConfig.IsMuteMusic;
        musicSource.mute = gameConfig.IsMuteMusic;
    }
    public void ToggleSFX()
    {
        gameConfig.IsMuteSFX = !gameConfig.IsMuteSFX;
        SFXSource.mute = gameConfig.IsMuteSFX;
    }

    public void SaveGameConfig()
    {
        GameConfig.SaveToFile(gameConfig);
    }
}
