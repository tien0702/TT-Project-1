using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HealthBar : MonoBehaviour, IEntityObserver
{
    [SerializeField] private HeartIcon HeartIconPrefab;
    [SerializeField] Character character;
    public List<HeartIcon> healthIcons;
    
    void Awake()
    {

    }

    void Start()
    {
        character = Player.Instance.MainCharacter.GetComponent<Character>();
        healthIcons = new List<HeartIcon>();
        character.Attach(EntityObserverType.OnRebornHP, this);
        character.Attach(EntityObserverType.OnRecoveryHP, this);
        character.Attach(EntityObserverType.OnTakeDamage, this);
        character.Attach(EntityObserverType.OnDie, this);
        UpdateHearts();
    }

    public void OnDie()
    {
    }

    public void OnRebornHP()
    {
        TurnOnHealth();
    }

    public void OnRecoveryHP()
    {
        TurnOnHealth();
    }

    public void OnTakeDamage()
    {
        TurnOnHealth();
    }

    void UpdateHearts()
    {
        int maxHeartIcons = character.maxHealth;

        int amountHearts = Mathf.Abs(maxHeartIcons - healthIcons.Count);
        if(maxHeartIcons > healthIcons.Count) IncreaseHearts(amountHearts);
        else ReduceHearts(amountHearts);
        TurnOnHealth();
    }

    void TurnOnHealth()
    {
        int health = character.CurrentHealth;
        healthIcons.ForEach(icon => icon.SetState(false));
        healthIcons.GetRange(0, health).ForEach(icon => icon.SetState(true));
    }
    
    void IncreaseHearts(int amount)
    {
        while((amount--) != 0) healthIcons.Add(Instantiate(HeartIconPrefab, transform));
    }

    void ReduceHearts(int amount)
    {
        int range = Mathf.Max(healthIcons.Count, healthIcons.Count - amount);
        healthIcons.RemoveRange(healthIcons.Count, Mathf.Max(0, range));
    }
}
