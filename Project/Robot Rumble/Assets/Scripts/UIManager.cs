using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Player Health")]
    [SerializeField] Slider healthBar;
    [SerializeField] PlayerHealth playerHealth;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreManager scoreManager;

    void Awake() {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void Start() {
        healthBar.maxValue = playerHealth.GetHealth();
    }

    void Update(){
        healthBar.value = playerHealth.GetHealth();
        scoreText.text = scoreManager.GetScore().ToString();
    }
}
