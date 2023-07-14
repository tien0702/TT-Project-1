using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryItem : Item
{
    [SerializeField] private int healthAmount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.transform.GetComponent<Damageable>();
        if (damageable != null)
        {
            AudioManager.Instance.PlaySFX("Collect");
            damageable.Recovery(healthAmount);
            Destroy(gameObject);
            fx = Instantiate(FXPrefab, transform.position, Quaternion.identity, null);
        }
    }
}
