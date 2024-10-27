using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int score;

    public void UpdateScore(int value){
        score += value;
    }

    public int GetScore(){
        return score;
    }

    public void ResetScore(){
        score = 0;
    }
}
