using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaShapeManager : MonoBehaviour
{
    [SerializeField] List<ArenaMover> arenaShapes = new List<ArenaMover>();
    [SerializeField] int changeInterval = 3;

    ArenaMover currentArena;
 
    void Start() {
        currentArena = arenaShapes[Random.Range(0, arenaShapes.Count)];
        currentArena.RiseArena();
    }

    public IEnumerator SwitchArena(){
        if(GetComponent<WaveManager>().GetWaveNumber() % changeInterval == 0){
            currentArena.LowerArena();
            yield return new WaitForSeconds(1);
            while(true){
                int randomArena = Random.Range(0, arenaShapes.Count);
                if(arenaShapes[randomArena] != currentArena){
                    currentArena = arenaShapes[randomArena];
                    currentArena.RiseArena();
                    break;
                }
            }
        }
    }
}
