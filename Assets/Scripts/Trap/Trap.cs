using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float tickFrequence;

    float elapsedTime;
    bool canSenDamage;
    protected void Start()
    {
        elapsedTime = 0;
        canSenDamage = true;
    }

    protected void Update()
    {
        if (!canSenDamage)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= tickFrequence)
                canSenDamage = true;
        }
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (!canSenDamage) return;
        Character character = collision.transform.GetComponent<Character>();
        if (character == null) return;
        SenDamage(character);
    }

    protected void SenDamage(Character character)
    {
        character.TakeDamage(damage);
        ResetTickFrequence();
    }

    protected void ResetTickFrequence()
    {
        canSenDamage = false;
        elapsedTime = 0;
    }
}
