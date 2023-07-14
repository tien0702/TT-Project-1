using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartIcon : MonoBehaviour
{
    public Sprite enableIcon;
    public Sprite disableIcon;
    public bool IsEnable => isEnable;

    bool isEnable;
    Image image;
    private void Awake()
    {
        isEnable = false;
        image = GetComponent<Image>();
        image.sprite = disableIcon;
    }

    public void Toggle() => image.sprite = (isEnable) ? (enableIcon) : (disableIcon);
    public void SetState(bool enable) => image.sprite = (enable) ? (enableIcon) : (disableIcon);
}
