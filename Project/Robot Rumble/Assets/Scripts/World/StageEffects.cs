using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEffects : MonoBehaviour
{
    [SerializeField] List<ParticleSystem> stageFlames = new List<ParticleSystem>();
    [SerializeField] List<ScrollingScreen> scrollingScreens = new List<ScrollingScreen>();
    [SerializeField] GameObject warningLights;
    [SerializeField] Vector3 warningLightSpinSpeed = new Vector3(0, 10f, 0);

    List<Audience> audiences;
    AudioPlayer audioPlayer;

    void Start(){
        audiences = new List<Audience>(FindObjectsOfType<Audience>());
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Update(){
        if(warningLights.activeSelf){
            warningLights.transform.Rotate(warningLightSpinSpeed * Time.deltaTime);
        }
    }

    // Effect to signal the end of a wave, when all enemies have been defeated
    public void WaveEndEffect(float time){
        PlayFlames();
        PlayAudience(time);

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

    // Effect to signal a boss has spawned in the arena. Replaces the usual wave end effect
    public void SpawnBossEffect(float time){
        PlayFlames();
        PlayAudience(time);

        // Enable spinning red lights
        StartCoroutine(StartWarningLights(time));

        // Update text to display "WARNING"
        foreach(ScrollingScreen screen in scrollingScreens){
            screen.ShowWarningText();
        }
    }


    IEnumerator StartWarningLights(float time){
        warningLights.SetActive(true);
        yield return new WaitForSeconds(time);
        warningLights.SetActive(false);
    }
    
    void PlayFlames(){
        foreach(ParticleSystem flame in stageFlames){
            flame.Play();
            audioPlayer.PlayFlamesClip(flame.transform.position);
        }
    }

    void PlayAudience(float time){
        // Make audience move faster for given time
        foreach(Audience audience in audiences){
            StartCoroutine(audience.Applause(time));
        }
    }
}
