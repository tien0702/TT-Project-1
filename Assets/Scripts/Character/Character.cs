using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Damageable
{
    public CharacterData characterData;
    Animator animator;
    new void Awake()
    {
        base.Awake();
        if (characterData != null) LoadCharacterWithData(characterData);
        animator = GetComponent<Animator>();
    }

    new void Start()
    {
    }

    public  void OnEndHit()
    {
        animator.SetTrigger("Idle");
    }

    public void LoadCharacterWithData(CharacterData characterData)
    {
        this.characterData = characterData;
        base.Start();
        maxHealth = characterData.MaxHealth;
        health = characterData.CurrentHealth;
    }
}
