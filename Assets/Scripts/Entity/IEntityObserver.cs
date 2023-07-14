using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntityObserver
{
    public void OnRecoveryHP();
    public void OnRebornHP();
    public void OnTakeDamage();
    public void OnDie();
}
