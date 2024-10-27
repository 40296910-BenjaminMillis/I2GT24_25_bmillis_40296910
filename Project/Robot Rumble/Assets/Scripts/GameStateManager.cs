using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    GameObject currentPlayer;
    ScoreManager scoreManager;
    WaveManager waveManager;
    void Start() {
        scoreManager = GetComponent<ScoreManager>();
        waveManager = GetComponent<WaveManager>();
    }


    public void StartGame(){
        // Spawn the player (could do with this being its own script?)
        RaycastHit spawnLocation;
        Physics.Raycast(new Vector3(0, 40, -40), Vector3.down, out spawnLocation, Mathf.Infinity);
        currentPlayer = Instantiate(player, spawnLocation.point + Vector3.up, transform.rotation);

        // Remove any existing enemies
        waveManager.ClearEnemies();

        // Reset score and start waves
        scoreManager.ResetScore();
        waveManager.StartWaves();
    }

    //called when player is no longer in existance
    public void EndGame(){
        // Disable enemy waves
        waveManager.EndWaves();

        // Go to game over UI
        UIManager uiManager = FindObjectOfType<UIManager>();
        uiManager.LoadGameOver();
    }

}
