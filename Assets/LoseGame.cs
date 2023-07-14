using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseGame : MonoBehaviour, IEntityObserver
{
    private void OnEnable()
    {
        Time.timeScale = 0.0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1.0f;
    }

    public void OnRecoveryHP()
    {
    }

    public void OnRebornHP()
    {
    }

    public void OnTakeDamage()
    {
    }

    public void OnDie()
    {
        gameObject.SetActive(true);
        Debug.Log("character on die");
    }
    public void Replay()
    {
        SceneManager.LoadScene("GameScene");
    }
}
