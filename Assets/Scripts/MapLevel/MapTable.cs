using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class MapTable : MonoBehaviour
{
    public static readonly int MaxStar = 3;

    [SerializeField] private Sprite starEnable;
    [SerializeField] private Sprite starDisable;
    [SerializeField] private Color textEnableColor;
    [SerializeField] private Color textDisableColor;

    public MapData MapDataInTable => data;
    List<Image> stars = new List<Image>();
    List<TextMeshProUGUI> conditionTexts = new List<TextMeshProUGUI>();
    TextMeshProUGUI mapName;
    MapData data;

    private void Awake()
    {
        transform.Find("Stars").GetComponentsInChildren<Image>().ToList().
            ForEach(star => stars.Add(star.GetComponent<Image>()));
        mapName = transform.Find("MapName").GetComponent<TextMeshProUGUI>();
        conditionTexts.Add(transform.Find("Condition 1").GetComponent<TextMeshProUGUI>());
        conditionTexts.Add(transform.Find("Condition 2").GetComponent<TextMeshProUGUI>());
        conditionTexts.Add(transform.Find("Condition 3").GetComponent<TextMeshProUGUI>());
    }

    public void LoadInfoForMap(MapData MData)
    {
        data = MData;
        LoadTextInfo(MData);

        SetFalseConditionsText();
        DisableStars();

        SetTrueTextCondition(MData);
        EnableStar(MData);
    }

    void SetFalseConditionsText() => conditionTexts.ForEach(condition => condition.color = Color.gray);

    void DisableStars() => stars.ForEach(star => star.sprite = starDisable);

    int CalculateStars(MapData MData)
    {
        int countStars = 0;
        MapConditionData conditionData = MData.GetCacheConditionData();
        MapConditionData originData = MData.OriginalCondition;

        if (conditionData.HasBeenPassed) countStars += 1;
        if (conditionData.AmountDamageReceived <= originData.AmountDamageReceived && conditionData.HasBeenPassed) countStars += 1;
        if (conditionData.Score >= originData.Score) countStars += 1;
        return countStars;
    }

    void LoadTextInfo(MapData MData)
    {
        mapName.text = MData.MapName;
        conditionTexts[0].text = "Complete Map";
        conditionTexts[1].text = string.Format("Take damage {0} times less", MData.OriginalCondition.AmountDamageReceived);
        conditionTexts[2].text = string.Format("Score of {0} or more", MData.OriginalCondition.Score);
    }

    void SetTrueTextCondition(MapData MData)
    {
        MapConditionData conditionData = MData.GetCacheConditionData();
        MapConditionData originData = MData.OriginalCondition;

        if (conditionData.HasBeenPassed) conditionTexts[0].color = Color.cyan;
        if (conditionData.AmountDamageReceived <= originData.AmountDamageReceived && 
            (conditionData.HasBeenPassed || SceneManager.GetActiveScene().name == "GameScene")) 
            conditionTexts[1].color = Color.cyan;
        if (conditionData.Score >= originData.Score) conditionTexts[2].color = Color.cyan;
    }

    void EnableStar(MapData MData) => stars.GetRange(0, CalculateStars(MData)).ForEach(star => star.sprite = starEnable);
}
