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
  
private void Awake(){
    volumenFX.onValueChanged.AddListener(ChangeVolumeFx);
    volumenMaster.onValueChanged.AddListener(ChangeVolumeMaster);
}
   

    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject optionPanel;
    public GameObject levelSelectPanel;

    public void OpnePanel(GameObject panel){
        mainPanel.SetActive(false);
        optionPanel.SetActive(false);
        levelSelectPanel.SetActive(false);

        panel.SetActive(true);
    }
 public void ChangeVolumeMaster( float v){
        mixer.SetFloat("VolMaster",v);
 }

  public void ChangeVolumeFx( float v){
        mixer.SetFloat("VolFx",v);
 }

   public void PlaySoundButton(){

        fxSource.PlayOneShot(clickSound);
    }
    


}
