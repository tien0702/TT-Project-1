using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CollectItem : Item
{
    [SerializeField] private int score;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character character = collision.transform.GetComponent<Character>();
        if (character != null)
        {
            AudioManager.Instance.PlaySFX("Collect");
            Destroy(gameObject);
            fx = Instantiate(FXPrefab, transform.position, Quaternion.identity, null);
            GameManager.Instance.AddScore(score);
        }
    }
}
