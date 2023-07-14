using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDLayer : MonoBehaviour
{
    private static HUDLayer instance;
    public static HUDLayer Instance => instance;

    [SerializeField] private TextMeshProUGUI score;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(instance);
        }

        instance = this;
    }
    void Start()
    {
        score = transform.Find("Score").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        
    }
}
