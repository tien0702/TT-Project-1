using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPos : MonoBehaviour
{
    void Start()
    {
        var player = Player.Instance.MainCharacter;
        player.transform.position = this.transform.position;
        var character = player.GetComponentInChildren<Character>();
        character.transform.position = this.transform.position;
    }
}
