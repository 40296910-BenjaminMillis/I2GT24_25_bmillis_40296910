using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScrollingScreen : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 0.5f;
    TextMeshPro textField;
    WaveManager waveManager;
    string text;
    float timer;

    void Awake(){
        textField = GetComponent<TextMeshPro>();
        waveManager = FindObjectOfType<WaveManager>();
    }

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
        textField.text = " WAVE "+waveNumber+ " | WAVE "+waveNumber+ " | WAVE "+waveNumber+ " | WAVE "+waveNumber+ " | WAVE "+waveNumber+" |";
    }

    public void ShowEnemyCountText(){
        int enemyCount = waveManager.GetEnemyCount();
        textField.text = " ENEMIES "+enemyCount+ " | ENEMIES "+enemyCount+ " | ENEMIES "+enemyCount+ " | ENEMIES "+enemyCount+ " |";
    }

    public void UpdateEnemyCountText(string oldCount, string newCount){
        string temp = textField.text;
        string modified = temp.Replace(oldCount, newCount);
        Debug.Log(modified);
        textField.text = modified;
    }

    public void ShowMenuText(){
        textField.text = " ROBOT RUMBLE | ROBOT RUMBLE |";
    }
}
