using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIController : MonoBehaviour, IEntityObserver
{
    [SerializeField] private Transform loseGameLayer;
    void Start()
    {
        loseGameLayer = HUDLayer.Instance.transform.Find("LoseGameLayer");
        Player.Instance.MainCharacter.GetComponent<Character>().Attach(EntityObserverType.OnDie, this);
    }
    public void OnDie()
    {
        loseGameLayer.gameObject.SetActive(true);
    }

    public void OnRebornHP()
    {

    }

    public void OnRecoveryHP()
    {

    }

    public void OnTakeDamage()
    {

    }
}
