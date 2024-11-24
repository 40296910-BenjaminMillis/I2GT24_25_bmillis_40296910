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

    int waveNumber; // Represents the number of enemy waves that has passed, Multiplies how many enemies are added after each wave
    int enemyCount; // The current number of enemies that exist
    bool isRunning; // Denotes if the wave manager is currently active

    // Reset values for a new game
    public void StartWaves() {
        waveNumber = 0;
        GetComponent<ArenaShapeManager>().SwitchArena();
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
                StartCoroutine(GetComponent<ArenaShapeManager>().SwitchArena());
                enemyCount = waveIntensity * waveNumber;
                StartCoroutine(SpawnEnemies());
            }
        }
    }

    IEnumerator SpawnEnemies(){
        yield return new WaitForSeconds(2);
        int rankCount = 0; // An enemys rank determines its worth during the wave
        RaycastHit spawnLocation;
        while(rankCount < enemyCount){
            int randomEnemy = Random.Range(0, enemySelection.Count); //Select a random enemy type to spawn, and check if they have the correct rank space to be added
            if(rankCount + enemySelection[randomEnemy].GetRank() <= enemyCount){
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


    //remove from enemy count when enemy is destroyed
    public void UpdateEnemyCount(int value){
        enemyCount += value;
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

    public int GetWaveNumber(){
        return waveNumber;
    }
}
