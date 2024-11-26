using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    
    [SerializeField] List<EnemyBehaviour> enemySelection = new List<EnemyBehaviour>(); // A list that the wave manager chooses from to spawn new enemies
    [SerializeField] int xSpread;
    [SerializeField] int zSpread;
    [SerializeField] ParticleSystem spawnEffect;
    [SerializeField] int waveIntensity = 3; // Affects how many enemies can spawn each round. Multiplied after each round
    [SerializeField] float spawnWaitTime = 2.5f; // Time before spawning enemies

    int waveNumber; // Represents the number of enemy waves that has passed, Multiplies how many enemies are added after each wave
    int enemyRankCount; // The total ranking of enemies that are allowed to exist
    int enemyCount; // The current number of enemies that exist
    bool isRunning; // Denotes if the wave manager is currently active

    // Reset values for a new game
    public void StartWaves() {
        waveNumber = 0;
        GetComponent<ArenaShapeManager>().SwitchArena();
        enemyRankCount = 0; 
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
            if(enemyRankCount <= 0){
                ClearProjectiles();
                waveNumber++;
                StartCoroutine(GetComponent<ArenaShapeManager>().SwitchArena());
                enemyRankCount = waveIntensity * waveNumber;
                Debug.Log(enemyRankCount + " " + enemyCount); 
                StartCoroutine(SpawnEnemies());
                GetComponent<StageEffects>().WaveEndEffect();
            }
        }
    }

    IEnumerator SpawnEnemies(){
        yield return new WaitForSeconds(spawnWaitTime);
        int rankCount = 0; // An enemys rank determines its worth during the wave
        RaycastHit spawnLocation;
        if(FindObjectOfType<PlayerControl>()){
            while(rankCount < enemyRankCount){
                int randomEnemy = Random.Range(0, enemySelection.Count); //Select a random enemy type to spawn, and check if they have the correct rank space to be added
                if(rankCount + enemySelection[randomEnemy].GetRank() <= enemyRankCount){
                    //Move the spawner to a random location and cast a ray to spawn the enemy
                    Physics.Raycast(new Vector3(Random.Range(-xSpread, xSpread), 40, Random.Range(-zSpread, zSpread)), Vector3.down, out spawnLocation, Mathf.Infinity);

                    rankCount += enemySelection[randomEnemy].GetRank();

                    ParticleSystem instance = Instantiate(spawnEffect, spawnLocation.point, spawnEffect.transform.rotation);
                    Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);

                    Instantiate(enemySelection[randomEnemy], spawnLocation.point + Vector3.up, transform.rotation);
                    Debug.Log(spawnLocation.point);
                    enemyCount++;
                }
            }
            GetComponent<StageEffects>().WaveStartEffect();
        }
    }


    //remove from enemy count when enemy is destroyed
    public void UpdateEnemyCount(int value){
        enemyRankCount += value;
        GetComponent<StageEffects>().UpdateEnemyCount(enemyCount, enemyCount-1);
        enemyCount--;
        Debug.Log("enemy count: " + enemyCount);
    }

    // Remove all enemies currently in the game. Used after game over
    public void ClearEnemies(){
        EnemyBehaviour[] allEnemies = FindObjectsOfType<EnemyBehaviour>();
    	foreach(EnemyBehaviour enemy in allEnemies) {
        	Destroy(enemy.gameObject);
    	}
    }

    public void ClearProjectiles(){
        Projectile[] allProjectiles = FindObjectsOfType<Projectile>();
    	foreach(Projectile projectile in allProjectiles) {
        	Destroy(projectile.gameObject);
    	}
    }

    public void ClearPowerups(){
        Powerup[] allPowerups = FindObjectsOfType<Powerup>();
    	foreach(Powerup powerup in allPowerups) {
        	Destroy(powerup.gameObject);
    	}
    }

    public void ClearAll(){
        ClearEnemies();
        ClearProjectiles();
        ClearPowerups();
    }

    public int GetWaveNumber(){
        return waveNumber;
    }

    public int GetEnemyCount(){
        return enemyCount;
    }
}
