using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLevelManager : MonoBehaviour
{
    private static MapLevelManager instance;
    public static MapLevelManager Instance => instance;

    private MapData mapData;
    private MapData dataInGame;
}
