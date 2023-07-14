using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundTable : MonoBehaviour
{
    [SerializeField] private Sprite musicOnIcon;
    [SerializeField] private Sprite musicOffIcon;
    [SerializeField] private Sprite sfxOnIcon;
    [SerializeField] private Sprite sfxOffIcon;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    Image musicBtnImg, sfxBtnImg;

    private void Start()
    {
        musicBtnImg = transform.Find("MusicBtn").GetComponent<Image>();
        sfxBtnImg = transform.Find("SFXBtn").GetComponent<Image>();

        musicBtnImg.sprite = (AudioManager.Instance.Config.IsMuteMusic) ? (musicOffIcon) : (musicOnIcon);
        sfxBtnImg.sprite = (AudioManager.Instance.Config.IsMuteSFX) ? (sfxOffIcon) : (sfxOnIcon);

        musicSlider = transform.Find("MusicSlider").GetComponent<Slider>();
        sfxSlider = transform.Find("SFXSlider").GetComponent<Slider>();


        if (sfxSlider == null) Debug.Log("NUll");
        sfxSlider.value = AudioManager.Instance.Config.SFXVolume;
        musicSlider.value = AudioManager.Instance.Config.MusicVolume;
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
        musicBtnImg.sprite = (AudioManager.Instance.Config.IsMuteMusic) ? (musicOffIcon) : (musicOnIcon);
        musicBtnImg.SetNativeSize();
    }
    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
        sfxBtnImg.sprite = (AudioManager.Instance.Config.IsMuteSFX) ? (sfxOffIcon) : (sfxOnIcon);
        sfxBtnImg.SetNativeSize();
    }

    public void OnBack()
    {
        AudioManager.Instance.SaveGameConfig();
    }
    public void ChangeMusicVolume()
    {
        if (!AudioManager.Instance.Config.IsMuteMusic && musicSlider.value == 0) ToggleMusic();
        else if (AudioManager.Instance.Config.IsMuteMusic) ToggleMusic();

        AudioManager.Instance.SetMusicVolume(musicSlider.value);
    }
    public void ChangeSFXVolume()
    {
        if (!AudioManager.Instance.Config.IsMuteSFX && sfxSlider.value == 0) ToggleSFX();
        else if (AudioManager.Instance.Config.IsMuteSFX) ToggleSFX();
        AudioManager.Instance.SetSFXVolume(sfxSlider.value);
    }
}
