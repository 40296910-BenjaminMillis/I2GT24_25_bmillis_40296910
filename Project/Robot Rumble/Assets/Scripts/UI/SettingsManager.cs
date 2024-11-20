using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] [Range(1, 10)] float sensitivityDefault;
    [SerializeField] Slider sensitivitySlider;

    [Header("Sounds")]
    [SerializeField] [Range(0, 1)] float audioDefault;
    [SerializeField] Slider audioSlider;
    [SerializeField] AudioPlayer audioPlayer;

    void Start(){
        if(!PlayerPrefs.HasKey("sensitivity")){
            PlayerPrefs.SetFloat("sensitivity", sensitivityDefault);
            PlayerPrefs.SetFloat("sfxVolume", audioDefault);
            FindObjectOfType<AudioPlayer>().SetVolumeLevel(PlayerPrefs.GetFloat("sfxVolume"));
        }
    }

    public void SetSensitivity(float sensitivity){
        PlayerPrefs.SetFloat("sensitivity", sensitivity);
        if(FindObjectOfType<PlayerControl>()){
            FindObjectOfType<PlayerControl>().SetLookSensitivity();
        }
    }

    public void SetVolume(float volume){
        PlayerPrefs.SetFloat("sfxVolume", volume);
        audioPlayer.SetVolumeLevel(volume);
    }

    public void LoadSettingsValues(){
        sensitivitySlider.value = PlayerPrefs.GetFloat("sensitivity");
        audioSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }
}
