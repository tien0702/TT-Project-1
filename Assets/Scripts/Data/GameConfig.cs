using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class GameConfig : ScriptableObject
{
    [Range(0f, 1f)] public float MusicVolume;
    [Range(0f, 1f)] public float SFXVolume;
    public bool IsMuteMusic;
    public bool IsMuteSFX;

    public static GameConfig LoadFromFile()
    {
        string content = File.ReadAllText(GameConfig.GetPath());
        var config = JsonConvert.DeserializeObject<GameConfig>(content);
        return config;
    }

    public static void SaveToFile(GameConfig config)
    {
        string jsoncontent = JsonConvert.SerializeObject(config);
        File.WriteAllText(GameConfig.GetPath(), jsoncontent);
    }

    public static string GetPath()
    {
        return Application.dataPath + "/Data/GameData/GameConfig.json";
    }
}
