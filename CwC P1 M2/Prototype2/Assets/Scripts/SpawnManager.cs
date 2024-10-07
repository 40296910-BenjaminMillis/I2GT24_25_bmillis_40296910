using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    float spawnRangex = 20;
    float spawnRangez = 20;

    float startDelay = 2;
    float spawnInterval = 1.5f;

    public GameObject[] animalPrefabs;
    //public int animalIndex;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.S)){
        //     SpawnRandomAnimal();
        // }
    }

    //Randomly gens animal index and spawn position
    void SpawnRandomAnimal(){
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangex, spawnRangex), 0, spawnRangez);
        Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation);
    }
}
