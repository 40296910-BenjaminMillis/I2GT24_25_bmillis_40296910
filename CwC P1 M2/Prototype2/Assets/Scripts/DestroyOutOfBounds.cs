using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    float topBound = 30;
    float lowerBound = -10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Destroy any projectiles that miss and are out of bounds 
        if(transform.position.z > topBound){
            Destroy(gameObject);
        }
        //If an object goes past the player, game over
        else if(transform.position.z < lowerBound){
            Debug.Log("Game Over");
            Destroy(gameObject);
        }
    }
}
