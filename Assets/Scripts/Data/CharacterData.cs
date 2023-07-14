using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Data", menuName = "Entity/Create Character Data")]
public class CharacterData : ScriptableObject
{
    [Header("Infor")]
    public int characterID;
    public string CharName;
    public int MaxHealth;
    public int CurrentHealth;

    [Header("Dash")]
    public int DashPower;
    public bool DidDash;
    public float DashTime;
    public float DashBootTime;

    [Header("Run")]
    public float RunSpeed;
    [Range(0f, 1f)] public float RunRate;

    [Header("Jump")]
    public float JumpPower;
    public float JumpTime;
    public float JumpMuiltiplier;
    public float AirJumpSpeed;
    [Range(0f, 1f)] public float AirJumpRate;

    [Header("Double Jump")]
    public float DoubleJumpPower;
    public float DoubleJumpTime;
    public float DoubleJumpMuiltiplier;
    public float DoubleAirJumpSpeed;
    [Range(0f, 1f)] public float DoubleAirJumpRate;

    [Header("Fall")]
    public float FallSpeed;
    [Range(0f, 1f)] public float FallRate;
    public float FallMuiltiplier;

    [Header("FallDoubleJump")]
    public float FallDoubleJumpSpeed;
    [Range(0f, 1f)] public float FallDoubleJumpRate;
    public float FallDoubleJumpMuiltiplier;
    public float TimeFly;
}
