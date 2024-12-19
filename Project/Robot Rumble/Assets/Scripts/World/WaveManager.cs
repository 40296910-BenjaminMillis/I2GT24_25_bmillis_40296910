using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    
    [SerializeField] List<EnemyBehaviour> enemySelection = new List<EnemyBehaviour>(); // A list that the wave manager chooses from to spawn new enemies
    [SerializeField] BossBehaviour boss;
    [SerializeField] int bossSpawnWave = 10;
    [SerializeField] int xSpread;
    [SerializeField] int zSpread;
    [SerializeField] ParticleSystem spawnEffect;
    [SerializeField] int waveIntensity = 3; // Affects how many enemies can spawn each round. Multiplied after each round
    [SerializeField] float spawnWaitTime = 2.5f; // Time before spawning enemies

    int waveNumber; // Represents the number of enemy waves that has passed, Multiplies how many enemies are added after each wave
    int enemyRankCount; // The total ranking of enemies that are allowed to exist
    int enemyCount; // The current number of enemies that exist
    bool isRunning; // Denotes if the wave manager is currently active
    BossBehaviour currentBoss;

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
                FindObjectOfType<PlayerHealth>().UpdateHealth(1); // Heal player 1 health every wave
                ClearProjectiles();
                waveNumber++;
                StartCoroutine(GetComponent<ArenaShapeManager>().SwitchArena());
                enemyRankCount = waveIntensity * waveNumber;
                Debug.Log("Enemy rank count: " + enemyRankCount + ", Enemy count: " + enemyCount); 

                if(waveNumber % bossSpawnWave == 0 && currentBoss == null){
                    Debug.Log("Spawning boss");
                    SpawnBoss();
                    GetComponent<StageEffects>().SpawnBossEffect(spawnWaitTime);
                }

                else{
                    GetComponent<StageEffects>().WaveEndEffect(spawnWaitTime);
                }
                StartCoroutine(SpawnEnemies());
            }
        }
    }

    IEnumerator SpawnEnemies(){
        yield return new WaitForSeconds(spawnWaitTime);
        int rankCount = 0; // An enemys rank determines its worth during the wave
        RaycastHit spawnLocation;
        if(FindObjectOfType<PlayerControl>()){
            Transform playerTransform = FindObjectOfType<PlayerControl>().transform;
            while(rankCount < enemyRankCount){
                int randomEnemy = Random.Range(0, enemySelection.Count); //Select a random enemy type to spawn, and check if they have the correct rank space to be added
                int waveSpawnMin = (int)(enemySelection[randomEnemy].GetRank()*1.5f);

                if(rankCount + enemySelection[randomEnemy].GetRank() <= enemyRankCount && waveSpawnMin <= waveNumber){
                    //Move the spawner to a random location and cast a ray to spawn the enemy
                    Physics.Raycast(new Vector3(Random.Range(-xSpread, xSpread), 40, Random.Range(-zSpread, zSpread)), Vector3.down, out spawnLocation, Mathf.Infinity);

                    rankCount += enemySelection[randomEnemy].GetRank();

                    ParticleSystem instance = Instantiate(spawnEffect, spawnLocation.point, spawnEffect.transform.rotation);
                    Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);

                    Instantiate(enemySelection[randomEnemy], spawnLocation.point + Vector3.up, transform.rotation);
                    Debug.Log("Enemy spawned at: " + spawnLocation.point);
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
    }

    void SpawnBoss(){
        currentBoss = Instantiate(boss, boss.transform.position, boss.transform.rotation);
    }

    // Remove all enemies currently in the game. Used after game over
    public void ClearEnemies(){
        EnemyBehaviour[] allEnemies = FindObjectsOfType<EnemyBehaviour>();
    	foreach(EnemyBehaviour enemy in allEnemies) {
        	Destroy(enemy.gameObject);
    	}
        if(currentBoss)
            Destroy(currentBoss.gameObject);
                // BossBehaviour[] allBosses = FindObjectsOfType<BossBehaviour>();
    	// foreach(BossBehaviour boss in allBosses) {
        //     Destroy(boss);
        // }
    }

    public void ClearProjectiles(){
        Projectile[] allProjectiles = FindObjectsOfType<Projectile>();
    	foreach(Projectile projectile in allProjectiles) {
        	Destroy(projectile.gameObject);
    	}
        FindObjectOfType<LineRenderer>().enabled = false;
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
