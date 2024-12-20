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
    [SerializeField] Canvas settingsUI;

    [Header("State Manager")]
    [SerializeField] GameStateManager gameStateManager;

    [Header("Camera")]
    [SerializeField] GameObject menuCamera;
    [SerializeField] Vector3 menuCameraPanSpeed = new Vector3(0, 1f, 0);

    [Header("Player Information")]
    [SerializeField] Slider healthBar;
    [SerializeField] GameObject dashCooldownBar;
    [SerializeField] Slider bossHealthbar;
    PlayerControl player;
    PlayerHealth playerHealth;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreManager scoreManager;

    float pauseCooldown = 0;
    Canvas lastCanvas;
    MusicPlayer musicPlayer;

    void Start() {
        settingsUI.GetComponent<SettingsManager>().StartSettings();
        scoreManager = FindObjectOfType<ScoreManager>();
        musicPlayer = FindObjectOfType<MusicPlayer>();
        LoadMainMenu();
    }

    void Update(){
        // While the menu is up, pan the menu camera around the arena
        if(menuCamera.activeSelf){
            menuCamera.transform.Rotate(menuCameraPanSpeed * Time.deltaTime);
        }
        else if(gameUI.enabled){
            // Prevent the player from entering and exiting the pause menu too quickly
            if(Input.GetKey(KeyCode.Tab) && pauseCooldown <=0 && !settingsUI.enabled){
                TogglePauseMenu();
                pauseCooldown = 0.3f;
            }
            pauseCooldown -= Time.unscaledDeltaTime;

            // Update player info
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

            // Get boss info
            if(bossHealthbar.gameObject.activeSelf){
                bossHealthbar.value = FindObjectOfType<BossHealth>().GetHealth();
            }
        }
    }

    public void StartGame(){
        // Disable menu and game over ui
        menuUI.enabled = false;
        gameOverUI.enabled = false;
        menuCamera.SetActive(false);

        // GameManager Setup
        gameStateManager.StartGame();

        // Set up game UI
        player = FindObjectOfType<PlayerControl>();
        dashCooldownBar.GetComponent<Slider>().maxValue = player.getDashDelay();
        playerHealth = player.GetComponent<PlayerHealth>();
        healthBar.maxValue = playerHealth.GetHealth();
        healthBar.value = playerHealth.GetHealth();

        ToggleCursorOff();
        gameUI.enabled = true;

        musicPlayer.SetGameMusic();
    }

    // Currently unused, as the game is run on Unity Play and does not need one
    public void QuitGame(){
        Application.Quit();
    }

    public void LoadGameOver(){
        // Disable the game UI, enable game over UI
        gameUI.enabled = false;
        ToggleBossHealthbarOff();
        ToggleCursorOn();
        gameOverUI.enabled = true;
        menuCamera.SetActive(true);
        
        // Score text
        finalScoreText.text = "SCORE: " + scoreManager.GetScore();
    }

    public void LoadMainMenu(){
        if(FindObjectOfType<PlayerControl>())
            Destroy(FindObjectOfType<PlayerControl>().gameObject);

        menuCamera.SetActive(true);
        ToggleCursorOn();
        Time.timeScale = 1f;
        menuUI.enabled = true;
        gameUI.enabled = false;
        gameOverUI.enabled = false;
        pauseUI.enabled = false;
        settingsUI.enabled = false;
        gameStateManager.GetComponent<WaveManager>().ClearAll();
        FindObjectOfType<StageEffects>().MainMenuEffect();
        ToggleBossHealthbarOff();

        musicPlayer.SetMenuMusic();
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

    public void ToggleSettingsMenu(){
        if(!settingsUI.enabled){
            if(pauseUI.enabled){
                pauseUI.enabled = false;
                lastCanvas = pauseUI;
            }
            else if(menuUI.enabled){
                menuUI.enabled = false;
                lastCanvas = menuUI;
            }
            settingsUI.GetComponent<SettingsManager>().LoadSettingsValues();
            settingsUI.enabled = true;
        }
        else{
            settingsUI.enabled = false;
        }
    }

    public void Return(){
        lastCanvas.enabled = true;
    }

    void ToggleCursorOn(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void ToggleCursorOff(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ToggleBossHealthbarOn(int health){
        bossHealthbar.gameObject.SetActive(true);
        bossHealthbar.maxValue = health;
        bossHealthbar.value = health;
    }

    public void ToggleBossHealthbarOff(){
        bossHealthbar.gameObject.SetActive(false);
    }
}
