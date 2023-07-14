using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance => instance;

    public PlayerData Data;
    public List<GameObject> CharPrefabs;
    public GameObject MainCharacter => mainCharacter;

    private GameObject mainCharacter;

    private void Awake()
    {
        if(instance != null && instance != this) Destroy(instance);
        instance = this;
        var character = CharPrefabs.Find(c => c.GetComponent<Character>().characterData.characterID == Data.CharacterSelectedID);
        mainCharacter = Instantiate(character, Vector2.zero, Quaternion.identity, transform);
    }

    private void Start()
    {
    }
}
