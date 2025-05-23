using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] List<Powerup> powerupList = new List<Powerup>();
    [SerializeField] int powerupScoreReq = 1500; // The base points requirement for a powerup to spawn
    int powerupScore; // Doubles each time this score it is met
    int score;

    void Start(){
        powerupScore = powerupScoreReq;
    }


    void FixedUpdate(){
        if(score >= powerupScore){
            SpawnPowerup();
            
            // Increase the threshold for the next spawn
            powerupScore *= 2;
        }
    }

    public void UpdateScore(int value){
        score += value;
    }

    public int GetScore(){
        return score;
    }

    public void ResetScore(){
        score = 0;
        powerupScore = powerupScoreReq;
    }

    void SpawnPowerup(){
        // Select from random a list of powerup objects
        Powerup randomPowerup = powerupList[Random.Range(0, powerupList.Count)];

        // Spawn the powerup at a random position in the air (dramatic fall?)
        Vector3 spawnLocation = new Vector3(Random.Range(-45, 45), 75, Random.Range(-45, 45));
        Instantiate(randomPowerup, spawnLocation, randomPowerup.transform.rotation);
    }

}
