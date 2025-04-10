// using System.Collections;
// using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{
    [Header("Options")]
    public Slider volumenFX;
    public Slider volumenMaster;
    public Toggle mute;
    public AudioMixer mixer;
    public AudioSource fxSource;
    public AudioClip clickSound;
    private float lastVolume;

    private void Awake()
    {
        volumenFX.onValueChanged.AddListener(ChangeVolumeFx);
        volumenMaster.onValueChanged.AddListener(ChangeVolumeMaster);
    }


    [Header("Panels")]

    public GameObject[] panels;

    public void OpnePanel(GameObject panel)
    {


        foreach (GameObject p in panels)
        {
            p.SetActive(false);
        }
        panel.SetActive(true);
        PlaySoundButton();
    }
    public void ChangeVolumeMaster(float v)
    {
        mixer.SetFloat("VolMaster", v);
    }

    public void ChangeVolumeFx(float v)
    {
        mixer.SetFloat("VolFx", v);
    }

    public void PlaySoundButton()
    {
        UnityEngine.Debug.Log("Click");
        fxSource.PlayOneShot(clickSound);
    }
    public void Setmute()
    {

        if (mute.isOn)
        {
            mixer.GetFloat("VolMaster", out lastVolume);
            mixer.SetFloat("VolMaster", -80);
        }
        else
        {
            mixer.SetFloat("VolMaster", lastVolume);
        }
    }


}
