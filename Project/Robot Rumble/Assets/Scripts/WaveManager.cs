using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    
    [SerializeField] List<EnemyBehaviour> enemySelection = new List<EnemyBehaviour>(); // A list that the wave manager chooses from to spawn new enemies
    [SerializeField] int xSpread;
    [SerializeField] int zSpread;
    [SerializeField] ParticleSystem spawnEffect;

    int waveNumber; // Represents the number of enemy waves that has passed, Multiplies how many enemies are added after each wave
    int enemyCount; // The current number of enemies that exist
    bool isRunning; // Denotes if the wave manager is currently active

    // Reset values for a new game
    public void StartWaves() {
        waveNumber = 0;
        enemyCount = 0; 
        isRunning = true; 
    }

    public void EndWaves(){
        isRunning = false;
        //Keep the enemies from the current game to view from the game over screen
    }

    void Update()
    {
        if(isRunning){
            // When the enemy count reaches 0, the wave number increases and new enemies are spawned
            if(enemyCount <= 0){
                waveNumber++;
                enemyCount = 3 * waveNumber;

                int rankCount = 0; // An enemys rank determines its worth during the wave
                RaycastHit spawnLocation;
                while(rankCount < enemyCount){
                    int randomEnemy = Random.Range(0, enemySelection.Count-1); //Select a random enemy type to spawn

                    //Move the spawner to a random location and cast a ray to spawn the enemy
                    Physics.Raycast(new Vector3(Random.Range(-45, 45), 40, Random.Range(-45, 45)), Vector3.down, out spawnLocation, Mathf.Infinity);

                    rankCount += enemySelection[randomEnemy].GetRank();

                    ParticleSystem instance = Instantiate(spawnEffect, spawnLocation.point, spawnEffect.transform.rotation);
                    Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);

                    Instantiate(enemySelection[randomEnemy], spawnLocation.point + Vector3.up, transform.rotation);
                    Debug.Log(spawnLocation.point);
                }
            }
        }
    }

    //remove from enemy count when enemy is destroyed
    //(in the future, enemies could be worth more than 1 point, so keeping this open for now)
    public void UpdateEnemyCount(int value){
        enemyCount += value;
        Debug.Log("enemy count: " + enemyCount);
    }

    // Remove all enemies currently in the game. Used after game over
    public void ClearEnemies(){
        EnemyBehaviour[] allEnemies = FindObjectsOfType<EnemyBehaviour>();
        Debug.Log(allEnemies.Length);
    	foreach(EnemyBehaviour enemy in allEnemies) {
        	Destroy(enemy.gameObject);
    	}
    }


    public int GetWaveNumber(){
        return waveNumber;
    }
}
