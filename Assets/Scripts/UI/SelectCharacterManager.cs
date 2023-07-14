using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectCharacterManager : MonoBehaviour
{
    public List<Character> characters;
    public PlayerData data;

    private CharacterPortrait portrait;
    int currentIndex;
    TextMeshProUGUI title;
    void Start()
    {
        portrait = GetComponentInChildren<CharacterPortrait>();
        currentIndex = characters.IndexOf(characters.Find(c => c.characterData.characterID == data.CharacterSelectedID));
        UpdatePortrait();
    }

    public void NextCharacter()
    {
        ++currentIndex;
        if (currentIndex == characters.Count) currentIndex = 0;
        UpdatePortrait();
    }
    public void BackCharacter()
    {
        --currentIndex;
        if (currentIndex < 0) currentIndex = characters.Count - 1;
        UpdatePortrait();
    }

    public void Continue()
    {
        data.CharacterSelectedID = characters[currentIndex].GetComponent<Character>().characterData.characterID;
    }

    private void UpdatePortrait()
    {
        portrait.SetPortrait(characters[currentIndex]);
    }

    public Character GetCurrentSelect()
    {
        return characters[currentIndex];
    }

    private void OnEnable()
    {
        if(title == null)
        {
            title = transform.parent.Find("Title").GetComponent<TextMeshProUGUI>();
        }
        title.text = "Select Character";
    }
}
