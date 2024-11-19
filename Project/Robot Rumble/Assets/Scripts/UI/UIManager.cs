using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Screens")]
    [SerializeField] Canvas menuUI;
    [SerializeField] Canvas gameUI;
    [SerializeField] Canvas gameOverUI;
    [SerializeField] Canvas pauseUI;

    [Header("State Manager")]
    [SerializeField] GameStateManager gameStateManager;

    [Header("Camera")]
    [SerializeField] Camera menuCamera;
    [SerializeField] Vector3 menuCameraPanSpeed = new Vector3(0.25f, 0, 0);

    [Header("Player Information")]
    [SerializeField] Slider healthBar;
    [SerializeField] GameObject dashCooldownBar;
    PlayerControl player;
    PlayerHealth playerHealth;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreManager scoreManager;

    float pauseCooldown = 0;

    void Start() {
        menuUI.enabled = true;
        gameUI.enabled = false;
        gameOverUI.enabled = false;
        pauseUI.enabled = false;
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void Update(){
        //while the menu is up, pan the menu camera around the arena
        if(menuUI.enabled || gameOverUI.enabled){
            menuCamera.transform.LookAt(Vector3.zero);
            menuCamera.transform.Translate(menuCameraPanSpeed);
        }
        else if(gameUI.enabled){
            if(Input.GetKey(KeyCode.Tab) && pauseCooldown <=0){
                TogglePauseMenu();
                pauseCooldown = 0.3f;
            }
            pauseCooldown -= Time.unscaledDeltaTime;

            healthBar.value = playerHealth.GetHealth();
            scoreText.text = scoreManager.GetScore().ToString();
            dashCooldownBar.GetComponent<Slider>().value = player.getDashCooldown();
            if(dashCooldownBar.GetComponent<Slider>().value == 0)
                dashCooldownBar.SetActive(false);
            else
                dashCooldownBar.SetActive(true);

            if(gameStateManager.GetComponent<WaveManager>().GetWaveNumber().ToString() != waveText.text){
                waveText.text = gameStateManager.GetComponent<WaveManager>().GetWaveNumber().ToString();
            }
        }
    }

    public void StartGame(){
        //disable menu and game over ui
        menuUI.enabled = false;
        gameOverUI.enabled = false;
        menuCamera.enabled = false;

        //gameManager Setup
        gameStateManager.StartGame();

        //set up game UI
        player = FindObjectOfType<PlayerControl>();
        dashCooldownBar.GetComponent<Slider>().maxValue = player.getDashDelay();
        playerHealth = player.GetComponent<PlayerHealth>();
        healthBar.maxValue = playerHealth.GetHealth();
        healthBar.value = playerHealth.GetHealth();

        ToggleCursorOff();
        gameUI.enabled = true;
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void LoadGameOver(){
        //disable the game UI, enable game over UI
        gameUI.enabled = false;
        ToggleCursorOn();
        gameOverUI.enabled = true;
        menuCamera.enabled = true;
        
        //score text
        finalScoreText.text = "SCORE: " + scoreManager.GetScore();
    }

    public void LoadMainMenu(){
        if(FindObjectOfType<PlayerControl>())
            Destroy(FindObjectOfType<PlayerControl>().gameObject);

        menuCamera.enabled = true;
        ToggleCursorOn();
        Time.timeScale = 1f;
        gameOverUI.enabled = false;
        gameUI.enabled = false;
        pauseUI.enabled = false;
        menuUI.enabled = true;
        gameStateManager.GetComponent<WaveManager>().ClearEnemies();
    }

    public void TogglePauseMenu(){
        if(!pauseUI.enabled){
            ToggleCursorOn();
            Time.timeScale = 0f;
            pauseUI.enabled = true;
        }
        else{
            ToggleCursorOff();
            Time.timeScale = 1f;
            pauseUI.enabled = false;
        }
    }

    void ToggleCursorOn(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void ToggleCursorOff(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
