using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    TextMeshProUGUI title;
    void Start()
    {
        title = transform.parent.Find("Title").GetComponent<TextMeshProUGUI>();
        OnActive();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnActive()
    {
        gameObject.SetActive(true);
        if (title == null)
        {
            title = transform.parent.Find("Title").GetComponent<TextMeshProUGUI>();
        }
        title.text = "Main Menu";
    }

    public void OnQuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
