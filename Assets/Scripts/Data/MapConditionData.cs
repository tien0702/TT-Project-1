using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Map Condition Data", menuName = "Data/Map Condition Data")]
public class MapConditionData : ScriptableObject
{
    public bool HasBeenPassed;
    public int AmountDamageReceived;
    public int Score;
}
