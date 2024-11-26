using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaShapeManager : MonoBehaviour
{
    [SerializeField] List<ArenaMover> arenaShapes = new List<ArenaMover>();
    [SerializeField] int changeInterval = 3; // Number of waves required for an arena switch

    ArenaMover currentArena;
 
    void Start() {
        currentArena = arenaShapes[Random.Range(0, arenaShapes.Count)];
        currentArena.RiseArena();
    }

    // Manage what arena is in play
    public IEnumerator SwitchArena(){
        if(GetComponent<WaveManager>().GetWaveNumber() % changeInterval == 0){
            currentArena.LowerArena(); // Lower the previous arena
            yield return new WaitForSeconds(1);
            while(true){
                int randomArena = Random.Range(0, arenaShapes.Count);
                if(arenaShapes[randomArena] != currentArena){ // Make sure that the same arena is not reselected twice in a row
                    currentArena = arenaShapes[randomArena];
                    currentArena.RiseArena(); // Raise the next arena
                    break;
                }
            }
        }
    }
}
