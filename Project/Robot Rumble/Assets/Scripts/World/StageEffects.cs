using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEffects : MonoBehaviour
{
    [SerializeField] List<ParticleSystem> stageFlames = new List<ParticleSystem>();

    AudioPlayer audioPlayer;

    void Start(){
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Update(){
        // will manage a scrolling info screen
    }

    public void WaveEndEffect(){
        foreach(ParticleSystem flame in stageFlames){
            flame.Play();
            audioPlayer.PlayFlamesClip(flame.transform.position);
        }
        audioPlayer.PlayWaveEnd(Vector3.zero);
    }

}
