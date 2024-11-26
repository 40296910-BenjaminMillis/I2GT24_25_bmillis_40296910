using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEffects : MonoBehaviour
{
    [SerializeField] List<ParticleSystem> stageFlames = new List<ParticleSystem>();
    [SerializeField] List<ScrollingScreen> scrollingScreens = new List<ScrollingScreen>();

    AudioPlayer audioPlayer;

    void Start(){
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Update(){
        
    }

    // Effect to signal the end of a wave, when all enemies have been defeated
    public void WaveEndEffect(){
        foreach(ParticleSystem flame in stageFlames){
            flame.Play();
            audioPlayer.PlayFlamesClip(flame.transform.position);
        }

        foreach(ScrollingScreen screen in scrollingScreens){
            screen.ShowWaveText();
        }

        audioPlayer.PlayWaveEnd(Vector3.zero);
    }

    // Effect during the start of a new wave
    public void WaveStartEffect(){
        foreach(ScrollingScreen screen in scrollingScreens){
            screen.ShowEnemyCountText();
        }
    }

    // Effect when an enemy has been defeated
    public void UpdateEnemyCount(int oldCount, int newCount){
        foreach(ScrollingScreen screen in scrollingScreens){
            screen.UpdateEnemyCountText(oldCount.ToString(), newCount.ToString());
        }
    }

    // Effect playing during the main menu
    public void MainMenuEffect(){
        foreach(ScrollingScreen screen in scrollingScreens){
            screen.ShowMenuText();
        }
    }

}
