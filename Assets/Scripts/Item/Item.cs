using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] protected GameObject FXPrefab;

    protected GameObject fx;
    void Start()
    {
        FXPrefab = Resources.Load<GameObject>("Prefabs/FX/CollectFX");
    }
}
