using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : Trap
{
    [SerializeField] private float timeOn;

    bool isOn = false;
    Animator animator;
    protected new void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    protected new void OnTriggerStay2D(Collider2D collision)
    {
        if (isOn) base.OnTriggerStay2D(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isOn) EndOff();
    }

    public void EndOff()
    {
        animator.SetTrigger("HitTrigger");
    }

    public void EndHit()
    {
        animator.SetTrigger("OnTrigger");
        Invoke("EndOn", timeOn);
        isOn = true;
    }

    public void EndOn()
    {
        animator.SetTrigger("OffTrigger");
        isOn = false;
    }
}
