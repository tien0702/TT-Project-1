using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SelectMapManager : MonoBehaviour
{
    public List<MapData> mapLevelDatas = new List<MapData>();

    [SerializeField] private MapTable map;
    [SerializeField] private int CurrentSelect = 0;
    [SerializeField] GameData gameData;
    TextMeshProUGUI title;
    void Start()
    {
        title = transform.parent.Find("Title").GetComponent<TextMeshProUGUI>();
        map = transform.Find("MapTable").GetComponent<MapTable>();
        UpdateMapInfo();
    }
    void UpdateMapInfo() => map.LoadInfoForMap(mapLevelDatas[CurrentSelect]);

    public void NextMap()
    {
        CurrentSelect++;
        if(CurrentSelect >= mapLevelDatas.Count) CurrentSelect = 0;
        UpdateMapInfo();
    }

    public void BackMap()
    {
        CurrentSelect--;
        if(CurrentSelect < 0) CurrentSelect = mapLevelDatas.Count - 1;
        UpdateMapInfo();
    }
    public void StartGame()
    {
        gameData.MData = mapLevelDatas[CurrentSelect];
        SceneManager.LoadScene("GameScene");
    }

    private void OnEnable()
    {
        if (title == null)
        {
            title = transform.parent.Find("Title").GetComponent<TextMeshProUGUI>();
        }
        title.text = "Select Map";
    }
}
