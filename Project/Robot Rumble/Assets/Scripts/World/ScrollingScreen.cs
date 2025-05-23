using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScrollingScreen : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 0.5f; // Speed that the text of the screen scrolls at
    TextMeshPro textField;
    WaveManager waveManager;
    string text;
    float timer;

    void Awake(){
        textField = GetComponent<TextMeshPro>();
        waveManager = FindObjectOfType<WaveManager>();
    }

    // Scroll the text by moving the first character to the end of the string
    void FixedUpdate(){
        if(timer >= scrollSpeed){
            timer = 0;
            text = textField.text;
            string result = text.Substring(1) + text[0] + "";
            textField.text = result;
        }
        timer += Time.deltaTime; 
    }

    public void ShowWaveText(){
        int waveNumber = waveManager.GetWaveNumber();
        textField.text = " WAVE "+waveNumber+ " | WAVE "+waveNumber+ " | WAVE "+waveNumber+ " | WAVE "+waveNumber+ " | WAVE "+waveNumber+" |";
    }

    public void ShowEnemyCountText(){
        int enemyCount = waveManager.GetEnemyCount();
        textField.text = " ENEMIES "+enemyCount+ " | ENEMIES "+enemyCount+ " | ENEMIES "+enemyCount+ " | ENEMIES "+enemyCount+ " |";
    }

    // Replace the enemy count number in the text string
    public void UpdateEnemyCountText(string oldCount, string newCount){
        string temp = textField.text;
        string modified = temp.Replace(oldCount, newCount);

        // Fixing a somewhat specific bug, where the enemy count does not replace correctly when a value is wrapping around
        // Will not work for values greater than 99
        if(oldCount.Length > 1 && modified[0] == oldCount[oldCount.Length-1] && modified[modified.Length-1] == oldCount[0]){
            // Get a substring that will remove the buggered value, then add the correct value
            modified = modified.Substring(modified.IndexOf(' '), modified.LastIndexOf(' '));
            modified += newCount;
        }
        textField.text = modified;
    }

    public void ShowMenuText(){
        textField.text = " ROBOT RUMBLE | ROBOT RUMBLE |";
    }

    // Used during boss spawn
    public void ShowWarningText(){
        textField.text = " WARNING | WARNING | WARNING | WARNING |";
    }
}
