using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySettings : MonoBehaviour
{
    // enemies will look for this script on start. Store in variable on start
    [Header("Enemy Modifiers")]
    [SerializeField][Range(0.5f, 3)] float enemySpeed = 1; // Multiply base move speed and projectile speed
    [SerializeField][Range(1, 5)] int enemySpawnAmount = 2; // Affects how many enemies can spawn each round. Multiplied by the wave number after each round

    [Header("Player Modifiers")]
    [SerializeField][Range(1, 15)] int playerMaxHealth = 5; // Set max health of player
    [SerializeField][Range(0, 15)] int playerHealing = 1; // Amount healed between waves

    [Header("Game Modifiers")]
    [SerializeField][Range(0.5f, 1f)] float gameSpeed = 1;

    float enemySpeedMult;

    float playerMaxHealthMult;
    float playerHealingMult;

    float gameSpeedMult;


    // Enemies
    public float GetEnemySpeed()
    {
        return enemySpeed;
    }

    public void SetEnemySpeed(float value)
    {
        enemySpeed = value;

        if (enemySpeed > 1)
        {
            if (enemySpeed > 2)
            {
                enemySpeedMult = 0.4f;
            }
            else
            {
                enemySpeedMult = 0.2f;
            }
        }
        else if (enemySpeed < 1)
        {
            enemySpeedMult = -0.1f;
        }
        else
        {
            enemySpeedMult = 0;
        }
    }

    public int GetEnemySpawnAmount()
    {
        return enemySpawnAmount;
    }

    public void SetEnemySpawnAmount(int value)
    {
        enemySpawnAmount = value;
    }

    // Player
    public int GetPlayerMaxHealth()
    {
        return playerMaxHealth;
    }

    public void SetPlayerMaxHealth(int value)
    {
        playerMaxHealth = value;
        if (playerMaxHealth <= 3)
        {
            playerMaxHealthMult = 0.2f;
        }
        else if (playerMaxHealth >= 7)
        {
            playerMaxHealthMult = -0.2f;
        }
        else
        {
            playerMaxHealthMult = 0;
        }
    }

    public int GetPlayerHealing()
    {
        return playerHealing;
    }

    public void SetPlayerHealing(int value)
    {
        playerHealing = value;
        if (playerHealing == 0)
        {
            playerHealingMult = 0.2f;
        }
        else if (playerHealing > 1)
        {
            playerHealingMult = -0.1f;
        }
        else
        {
            playerHealingMult = 0;
        }
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
    }

    public float GetTotalMult()
    {
        return 1 + enemySpeedMult + playerMaxHealthMult + playerHealingMult + gameSpeedMult;
    }
}
