using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySettings : MonoBehaviour
{
    // enemies will look for this script on start. Store in variable on start
    [Header("Enemy Speed")]
    [SerializeField][Range(0.5f, 3)] float enemySpeed = 1; // Multiply base move speed and projectile speed
    [SerializeField][Range(0.5f, 3)] float enemySpeedEasy = 0.5f;
    [SerializeField][Range(0.5f, 3)] float enemySpeedNormal = 1f;
    [SerializeField][Range(0.5f, 3)] float enemySpeedHard = 2f;

    [Header("Enemy Spawn Amount")]
    [SerializeField][Range(1, 5)] int enemySpawnAmount = 3; // Affects how many enemies can spawn each round. Multiplied by the wave number after each round
    [SerializeField][Range(1, 5)] float enemySpawnAmountEasy = 1;
    [SerializeField][Range(1, 5)] float enemySpawnAmountNormal = 3;
    [SerializeField][Range(1, 5)] float enemySpawnAmountHard = 5;

    [Header("Player Health")]
    [SerializeField][Range(1, 15)] int playerHealth = 5; // Set max health of player
    [SerializeField][Range(1, 15)] float playerHealthEasy = 10;
    [SerializeField][Range(1, 15)] float playerHealthNormal = 5;
    [SerializeField][Range(1, 15)] float playerHealthHard = 3;

    [Header("Player Healing")]
    [SerializeField][Range(0, 15)] int playerHealing = 1; // Amount healed between waves
    [SerializeField][Range(0, 15)] float playerHealingEasy = 5;
    [SerializeField][Range(0, 15)] float playerHealingNormal = 1;
    [SerializeField][Range(0, 15)] float playerHealingHard = 0;

    [Header("Game Modifiers")]
    [SerializeField][Range(0.5f, 1f)] float gameSpeed = 1; // Adjusts game speed. Currently unused as enemy speed basically serves the same purpose

    [Header("Sliders and Text")]
    [SerializeField] Slider enemySpeedSlider;
    [SerializeField] TextMeshProUGUI enemySpeedText;
    [SerializeField] Slider enemySpawnSlider;
    [SerializeField] TextMeshProUGUI enemySpawnText;
    [SerializeField] Slider playerHealthSlider;
    [SerializeField] TextMeshProUGUI playerHealthText;
    [SerializeField] Slider playerHealingSlider;
    [SerializeField] TextMeshProUGUI playerHealingText;
    [SerializeField] TextMeshProUGUI scoreMultText;

    float enemySpeedMult;
    float enemySpawnMult;

    float playerHealthMult;
    float playerHealingMult;

    float gameSpeedMult;

    void Start()
    {
        NormalDifficulty();
    }

    public void SetSliderValues()
    {
        enemySpeedSlider.value = enemySpeed;
        enemySpawnSlider.value = enemySpawnAmount;
        playerHealthSlider.value = playerHealth;
        playerHealingSlider.value = playerHealing;
        enemySpeedText.text = enemySpeed.ToString();
        enemySpawnText.text = enemySpawnAmount.ToString();
        playerHealthText.text = playerHealth.ToString();
        playerHealingText.text = playerHealing.ToString();
        scoreMultText.text = GetTotalMult().ToString();
    }


    public void EasyDifficulty()
    {
        SetEnemySpeed(enemySpeedEasy);
        SetEnemySpawnAmount(enemySpawnAmountEasy);
        SetPlayerMaxHealth(playerHealthEasy);
        SetPlayerHealing(playerHealingEasy);
        SetSliderValues();
    }

    public void NormalDifficulty()
    {
        SetEnemySpeed(enemySpeedNormal);
        SetEnemySpawnAmount(enemySpawnAmountNormal);
        SetPlayerMaxHealth(playerHealthNormal);
        SetPlayerHealing(playerHealingNormal);
        SetSliderValues();
    }

    public void HardDifficulty()
    {
        SetEnemySpeed(enemySpeedHard);
        SetEnemySpawnAmount(enemySpawnAmountHard);
        SetPlayerMaxHealth(playerHealthHard);
        SetPlayerHealing(playerHealingHard);
        SetSliderValues();
    }

    // Enemies
    public float GetEnemySpeed()
    {
        return enemySpeed;
    }

    public void SetEnemySpeed(float value)
    {
        value = Mathf.Round(value / 0.1f) * 0.1f;
        enemySpeed = value;
        enemySpeedText.text = enemySpeed.ToString();
        enemySpeedMult = (value - enemySpeedNormal) / 4;
        scoreMultText.text = GetTotalMult().ToString();
    }

    public int GetEnemySpawnAmount()
    {
        return enemySpawnAmount;
    }

    public void SetEnemySpawnAmount(float value)
    {
        enemySpawnAmount = (int)value;
        enemySpawnText.text = enemySpawnAmount.ToString();
        enemySpawnMult = (value - enemySpawnAmountNormal) / 8;
        scoreMultText.text = GetTotalMult().ToString();
    }

    // Player
    public int GetPlayerMaxHealth()
    {
        return playerHealth;
    }

    public void SetPlayerMaxHealth(float value)
    {
        playerHealth = (int)value;
        playerHealthText.text = playerHealth.ToString();

        if(value > playerHealthNormal)
            playerHealthMult = -(value - playerHealthNormal) / 10;
        else
            playerHealthMult = -(value - playerHealthNormal) / 4;


        scoreMultText.text = GetTotalMult().ToString();
    }

    public int GetPlayerHealing()
    {
        return playerHealing;
    }

    public void SetPlayerHealing(float value)
    {
        playerHealing = (int)value;
        playerHealingText.text = playerHealing.ToString();

        if(value > playerHealingNormal)
            playerHealingMult = -(value - playerHealingNormal) / 12;
        else
            playerHealingMult = -(value - playerHealingNormal) / 2;

        scoreMultText.text = GetTotalMult().ToString();
    }

    // Game
    public float GetGameSpeed()
    {
        return gameSpeed;
    }

    public void SetGameSpeed(float value)
    {
        gameSpeed = value;
        Time.timeScale = gameSpeed;
        if (gameSpeed > 1)
        {
            if (gameSpeed >= 1.4f)
            {
                gameSpeedMult = 0.3f;
            }
            else if (gameSpeed >= 1.2f)
            {
                gameSpeedMult = 0.2f;
            }
            gameSpeedMult = 0.1f;
        }
        else if (gameSpeed < 1)
        {
            if (gameSpeed <= 0.6f)
            {
                gameSpeedMult = -0.3f;
            }
            else if (gameSpeed <= 0.8f)
            {
                gameSpeedMult = -0.2f;
            }
            gameSpeedMult = -0.1f;
        }
        else
        {
            gameSpeedMult = 0;
        }
        scoreMultText.text = GetTotalMult().ToString();
    }

    public float GetTotalMult()
    {
        float totalMult = 1 + enemySpeedMult + enemySpawnMult + playerHealthMult + playerHealingMult + gameSpeedMult;
        if (totalMult < 0.5f)
            return 0.5f;
        else
            return totalMult;
    }
}
