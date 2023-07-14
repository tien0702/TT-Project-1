using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;
    public GameData GData;
    [SerializeField] private TextMeshProUGUI scoreText;

    public int Score => score;
    
    int score;

    private void Awake()
    {
        instance = this;
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
    }

    public void AddScore(int score)
    {
        this.score += score;
        scoreText.SetText(string.Format("Score: " + this.score));
    }
    public void NextScene(int sceneID)
    {
        var loadingScene = GameObject.Find("LoadingSceneLayout").GetComponent<LoadingScene>();
        loadingScene.LoadScene(sceneID);
    }

    public void PlayGame()
    {
        Debug.Log("Player game");
    }

    public bool LoadResources()
    {
        return true;
    }
}
