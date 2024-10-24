using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    
    [SerializeField] List<EnemyBehaviour> enemySelection = new List<EnemyBehaviour>(); // A list that the wave manager chooses from to spawn new enemies

    int waveNumber; // Represents the number of enemy waves that has passed, Multiplies how many enemies are added after each wave
    int enemyCount; // The current number of enemies that exist

    void Start() {
        waveNumber = 0;
        enemyCount = 0;  
    }

    void Update()
    {
        //when the enemy count reaches 0: 
            //the wave number increases
            //and new enemies are spawned
        if(enemyCount <= 0){
            waveNumber++;
            enemyCount = 2 * waveNumber;

            //for each enemy type, spawn a random amount of each
            int enemyRange = enemySelection.Count-1;
            int rankCount = 0; 
            while(rankCount < enemyCount){
                int randomEnemy = Random.Range(0, enemyRange);
                rankCount += enemySelection[randomEnemy].GetRank();
                Instantiate(enemySelection[randomEnemy], new Vector3(Random.Range(-20, 20), 2, Random.Range(-20, 20)), transform.rotation);
            }
            
        }
    }

    //remove from enemy count when enemy is destroyed
    //(in the future, enemies could be worth more than 1 point, so keeping this open for now)
    public void UpdateEnemyCount(int value){
        enemyCount += value;
        Debug.Log("enemy count: " + enemyCount);
    }
}
