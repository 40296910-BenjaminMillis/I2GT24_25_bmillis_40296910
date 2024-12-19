using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    GameObject currentPlayer;
    ScoreManager scoreManager;
    WaveManager waveManager;
    Leaderboard leaderboard;
    void Start() {
        scoreManager = GetComponent<ScoreManager>();
        waveManager = GetComponent<WaveManager>();
        leaderboard = FindObjectOfType<Leaderboard>();
        GetComponent<StageEffects>().MainMenuEffect();
        Application.targetFrameRate = 60;
    }


    public void StartGame(){
        // Spawn the player
        RaycastHit spawnLocation;
        Physics.Raycast(new Vector3(0, 40, -40), Vector3.down, out spawnLocation, Mathf.Infinity);
        currentPlayer = Instantiate(player, spawnLocation.point + Vector3.up, transform.rotation);

        // Remove any existing enemies
        waveManager.ClearEnemies();

        // Reset score and start waves
        scoreManager.ResetScore();
        waveManager.StartWaves();
    }

    // Called when player is no longer in existence
    public void EndGame(){
        // Disable enemy waves, keep enemies but remove projectiles and powerups
        waveManager.EndWaves();
        waveManager.ClearProjectiles();
        waveManager.ClearPowerups();

        // Go to game over UI
        UIManager uiManager = FindObjectOfType<UIManager>();
        uiManager.LoadGameOver();
        leaderboard.GetLeaderboard();

        GetComponent<StageEffects>().MainMenuEffect();
    }

}
