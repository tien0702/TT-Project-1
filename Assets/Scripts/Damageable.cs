using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour, IEntitySubject
{
    private Dictionary<EntityObserverType, List<IEntityObserver>> observers;
    public int maxHealth;
    protected int health;
    public bool isInvulnerable { get; set; }
    protected float timeSinceLastHit = 0.0f;
    public float invulnerabiltyTime;

    public int CurrentHealth => health;
    public int MaxHealth => maxHealth;

    protected virtual void Awake()
    {
        observers = new Dictionary<EntityObserverType, List<IEntityObserver>>();
        foreach (EntityObserverType type in System.Enum.GetValues(typeof(EntityObserverType)))
        {
            observers.Add(type, new List<IEntityObserver>());
        }
    }

    protected virtual void Start()
    {
        Reborn();
    }
    protected virtual void Update()
    {
        if (isInvulnerable)
        {
            timeSinceLastHit += Time.deltaTime;
            if (timeSinceLastHit > invulnerabiltyTime)
            {
                timeSinceLastHit = 0.0f;
                isInvulnerable = false;
            }
        }
    }
    public void Reborn()
    {
        health = maxHealth;
        isInvulnerable = false;
        timeSinceLastHit = 0.0f;
        NotifyAllObserver(EntityObserverType.OnRebornHP);
    }
    public void Recovery(int amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
        NotifyAllObserver(EntityObserverType.OnRecoveryHP);
    }
    public void TakeDamage(int amount)
    {
        if (health <= 0) return;
        if (isInvulnerable) return;
        health = Mathf.Max(0, health - amount);
        NotifyAllObserver(EntityObserverType.OnTakeDamage);
        GetComponent<Animator>().SetTrigger("HitTrigger");
        if (health == 0) 
            NotifyAllObserver(EntityObserverType.OnDie);
    }

    public void Attach(EntityObserverType type, IEntityObserver ob) => observers[type].Add(ob);

    public void Dettach(EntityObserverType type, IEntityObserver ob) => observers[type].Remove(ob);

    public void NotifyAllObserver(EntityObserverType type)
    {
        var obs = observers[type];
        switch (type)
        {
            case EntityObserverType.OnRecoveryHP:
                obs.ForEach(ob => ob.OnRecoveryHP());
                break;
            case EntityObserverType.OnRebornHP:
                obs.ForEach(ob => ob.OnRebornHP());
                break;
            case EntityObserverType.OnTakeDamage:
                obs.ForEach(ob => ob.OnTakeDamage());
                break;
            case EntityObserverType.OnDie:
                obs.ForEach(ob => ob.OnDie());
                break;
        }
    }

}
