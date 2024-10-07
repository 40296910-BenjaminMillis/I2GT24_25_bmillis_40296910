using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    float spawnRangex = 20;
    float spawnRangez = 20;
    public GameObject[] animalPrefabs;
    //public int animalIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S)){
            //Randomly gens animal index and spawn position
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangex, spawnRangex),
            0, spawnRangez);
            int animalIndex = Random.Range(0, animalPrefabs.Length);
            Instantiate(animalPrefabs[animalIndex], spawnPos,
            animalPrefabs[animalIndex].transform.rotation);
        }
    }
}
