using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Dan.Main;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> names;
    [SerializeField] List<TextMeshProUGUI> scores;

    [SerializeField] TextMeshProUGUI playerName;

    string publicKey = "632e3b40381162e0dce02359a5568bf0a0665a3a2ea89b7ccc8d0dbc9ed24135";
    ScoreManager scoreManager;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        GetLeaderboard();
    }

    // Get a list of the top scores and display them
    public void GetLeaderboard(){
        LeaderboardCreator.GetLeaderboard(publicKey, (msg => {
            int listLength;
            if(msg.Length < names.Count)
                listLength = msg.Length;
            else
                listLength = names.Count;

            for (int i = 0; i < listLength; i++){
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }
    
    // Add current score and name to leaderboard
    public void SetLeaderboardEntry(){
        LeaderboardCreator.UploadNewEntry(publicKey, playerName.text, scoreManager.GetScore(), (msg => {GetLeaderboard();}));
        GetLeaderboard();
    }
}
