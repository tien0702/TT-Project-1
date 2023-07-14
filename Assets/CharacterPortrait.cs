using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterPortrait : MonoBehaviour
{
    [SerializeField] private Image portrait;
    [SerializeField] private TextMeshProUGUI charInfoValue;
    private void Awake()
    {
        portrait = transform.Find("Portrait").GetComponent<Image>();
        Transform charInfo = transform.parent.Find("CharacterInfo");

        charInfoValue = charInfo.Find("InfoValue").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
    }

    public void SetPortrait(Character character)
    {
        portrait.sprite = character.GetComponent<SpriteRenderer>().sprite;
        UpdateInfo(character);
    }
    
    void UpdateInfo(Character character)
    {
        string valueStr = "";
        valueStr += character.characterData.CharName + '\n';
        valueStr += character.characterData.MaxHealth.ToString() + '\n';
        valueStr += character.characterData.RunSpeed.ToString() + '\n';
        valueStr += character.characterData.JumpPower.ToString() + '\n';
        valueStr += character.characterData.DashPower.ToString() + '\n';

        charInfoValue.text = valueStr;
    }
}
