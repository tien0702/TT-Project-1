using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour, IEntityObserver
{
    private static MapManager instance;
    public static MapManager Instance => instance;
    public MapData DataMap => data.MData;
    
    public GameData data;
    GameObject map;
    MapConditionData conditionCache;

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(instance);
        instance = this;
        LoadMapByName(data.MData.PrefabName);
    }

    public void LoadMapByName(string mapName)
    {
        GameObject mapPrefab = Resources.Load<GameObject>("Prefabs/Maps/" + mapName);
        if (map != null) Destroy(map);
        map = Instantiate(mapPrefab, Vector2.zero, Quaternion.identity, transform);
        conditionCache = ScriptableObject.CreateInstance("MapConditionData") as MapConditionData;
        Player.Instance.MainCharacter.GetComponent<Character>().Attach(EntityObserverType.OnTakeDamage, this);
    }
    public MapData Summary()
    {
        conditionCache.Score = GameManager.Instance.Score;
        MapData originData = data.MData;
        conditionCache.HasBeenPassed = true;
        data.MData.SetCacheConditionData(conditionCache);
        return originData;
    }

    public void OnRecoveryHP()
    {
    }

    public void OnRebornHP()
    {
    }

    public void OnTakeDamage()
    {
        conditionCache.AmountDamageReceived += 1;
    }

    public void OnDie()
    {
    }
}
