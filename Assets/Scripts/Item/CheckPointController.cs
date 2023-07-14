using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    Animator animator;
    bool isTrigger;
    GameObject winGameLayer;
    MapTable table;
    private void Start()
    {
        animator = GetComponent<Animator>();
        isTrigger = false;
        winGameLayer = HUDLayer.Instance.transform.Find("WinGameLayer").gameObject;
        table = winGameLayer.transform.Find("MapTable").GetComponent<MapTable>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || isTrigger) return;
        animator.SetTrigger("CPOutTrigger");
        isTrigger = true;
    }

    private void OnCPOutEnd()
    {
        animator.SetTrigger("CPIdleTrigger");
        winGameLayer.SetActive(true);
        table.LoadInfoForMap(MapManager.Instance.Summary());
    }
}
