using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameBtn : MonoBehaviour
{
    private bool isOpen = false;
    GameObject pauseMenu;
    private void Start()
    {
        pauseMenu = HUDLayer.Instance.transform.Find("PauseMenu").gameObject;
    }
    public void Toggle() => pauseMenu.SetActive(isOpen = !isOpen);
}
