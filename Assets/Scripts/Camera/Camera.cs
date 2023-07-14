using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private CinemachineConfiner confiner;
    [SerializeField] private Transform player;
    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        confiner = GetComponent<CinemachineConfiner>();

        player = Player.Instance.MainCharacter.GetComponent<Character>().transform;
        virtualCamera.Follow = player;

        confiner.m_BoundingShape2D = GameObject.Find("Confiner").GetComponent<PolygonCollider2D>();
    }
}
