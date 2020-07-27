using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioManager sound;
    public Slider effectSlider;
    public Slider bgmSlider;

    private void OnEnable()
    {
        SetData();
    }

    private void Start()
    {
        sound = FindObjectOfType<AudioManager>();

        effectSlider.value = PlayerPrefs.GetFloat("CurrEffectVol", 50);
        bgmSlider.value = PlayerPrefs.GetFloat("CurrBGMVol", 50);
    }

    public void SetEffectsVolume(float vol)
    {
        //Debug.Log("Effects Volume level = " + vol);
        sound.SetEffectsAudio(vol);
        PlayerPrefs.SetFloat("CurrEffectVol", vol);
    }

    public void SetBGMVolume(float vol)
    {
        //Debug.Log("BGM Volume level = " + vol);
        sound.SetBGMAudio(vol);
        PlayerPrefs.SetFloat("CurrBGMVol", vol);
    }

    public void GetData()
    {
        effectSlider.value = PlayerPrefs.GetFloat("CurrEffectVol", 50);
        bgmSlider.value = PlayerPrefs.GetFloat("CurrBGMVol", 50);
    }

    public void SetData()
    {
        SetEffectsVolume(PlayerPrefs.GetFloat("CurrEffectVol", 50));
        SetBGMVolume(PlayerPrefs.GetFloat("CurrBGMVol", 50)); ;
    }
}
