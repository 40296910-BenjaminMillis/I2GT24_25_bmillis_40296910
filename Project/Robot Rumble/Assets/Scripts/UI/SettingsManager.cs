using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] AudioPlayer audioPlayer;

    public void SetVolume(float volume){
        audioPlayer.SetVolumeLevel(volume);
    }
}
