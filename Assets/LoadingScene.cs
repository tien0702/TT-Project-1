using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public Image FillBar;
    public GameObject LoadingPanel;

    private void Start()
    {
        LoadingPanel = transform.Find("LoadingPanel").gameObject;
        FillBar = LoadingPanel.transform.Find("LoadingBar").Find("Fill").GetComponent<Image>();
        LoadingPanel.SetActive(false);
        FillBar.fillAmount = 0;
    }

    public void LoadScene(int sceneID)
    {
        StartCoroutine(LoadSceneAsync(sceneID));
    }

    IEnumerator LoadSceneAsync(int sceneID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);
        LoadingPanel.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            FillBar.fillAmount = progress;
            yield return null;
        }
    }
}
