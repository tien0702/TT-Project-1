using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

[CreateAssetMenu(fileName = "New Map Data", menuName = "Data/Map Data")]
public class MapData : ScriptableObject
{
    [Header("Map Infomation")]
    public string PrefabName;
    public string MapName;

    [Header("Map Condition")]
    public MapConditionData OriginalCondition;
    private MapConditionData CacheConditionData;

    public MapConditionData GetCacheConditionData()
    {
        if(CacheConditionData != null) return CacheConditionData;

        if (File.Exists(this.GetPath())) LoadCache();
        else CacheConditionData = ScriptableObject.CreateInstance("MapConditionData") as MapConditionData;
        
        return CacheConditionData;
    }

    public string GetPath()
    {
        string path = Application.dataPath +  "/Data/MapDatas/" + "Cache" + PrefabName + ".json";
        return path;
    }

    public void SetCacheConditionData(MapConditionData newCache)
    {
        CacheConditionData = newCache;
        WriteToFile();
    }

    void WriteToFile()
    {
        if(CacheConditionData == null)
        {
            Debug.Log("Write file failed, because CacheConditionData is null");
            return;
        }

        string json = JsonConvert.SerializeObject(CacheConditionData);
        File.WriteAllText(this.GetPath(), json);
    }

    void LoadCache()
    {
        string content = File.ReadAllText(this.GetPath());
        CacheConditionData = JsonConvert.DeserializeObject<MapConditionData>(content);
    }
}
