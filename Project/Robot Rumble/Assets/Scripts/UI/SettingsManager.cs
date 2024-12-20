using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] [Range(5, 100)] float sensitivityDefault; // Affects the speed the player camera moves
    [SerializeField] Slider sensitivitySlider;

    [Header("SFX")]
    [SerializeField] [Range(0, 1)] float sfxDefault;
    [SerializeField] Slider audioSlider;
    [SerializeField] AudioPlayer audioPlayer;

    [Header("Music")]
    [SerializeField] [Range(0, 1)] float musicDefault;
    [SerializeField] Slider musicSlider;
    [SerializeField] MusicPlayer musicPlayer;

    public void StartSettings(){
        //If player prefs has no settings stored, set the settings to a default value
        if(!PlayerPrefs.HasKey("musicVolume")){
            PlayerPrefs.SetFloat("sensitivity", sensitivityDefault);
        }

        if(!PlayerPrefs.HasKey("musicVolume")){
            PlayerPrefs.SetFloat("musicVolume", musicDefault);
        }
        musicPlayer.SetVolumeLevel(PlayerPrefs.GetFloat("musicVolume"));

        if(!PlayerPrefs.HasKey("sfxVolume")){
            PlayerPrefs.SetFloat("sfxVolume", sfxDefault);            
        }
        audioPlayer.SetVolumeLevel(PlayerPrefs.GetFloat("sfxVolume"));
    }

    public void SetSensitivity(float sensitivity){
        PlayerPrefs.SetFloat("sensitivity", sensitivity);
        if(FindObjectOfType<PlayerControl>()){
            FindObjectOfType<PlayerControl>().SetLookSensitivity();
        }
    }

    public void SetSfxVolume(float volume){
        PlayerPrefs.SetFloat("sfxVolume", volume);
        audioPlayer.SetVolumeLevel(volume);
    }

    public void SetMusicVolume(float volume){
        PlayerPrefs.SetFloat("musicVolume", volume);
        musicPlayer.SetVolumeLevel(volume);
    }

    public void LoadSettingsValues(){
        sensitivitySlider.value = PlayerPrefs.GetFloat("sensitivity");
        audioSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
}
