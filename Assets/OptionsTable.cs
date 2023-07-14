using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsTable : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    private void Start()
    {
        title = transform.parent.Find("Title").GetComponent<TextMeshProUGUI>();

        title.text = "Options";
    }
    public void OnBack()
    {
        AudioManager.Instance.SaveGameConfig();
    }
}
