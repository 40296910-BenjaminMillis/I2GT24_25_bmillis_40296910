using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocalLeaderboard : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> names;
    [SerializeField] List<TextMeshProUGUI> scores;
    [SerializeField] TextMeshProUGUI playerName;

    private const string PlayerPrefsBaseKey = "localleaderboard";

    ScoreManager scoreManager;

    void Start() {
        scoreManager = FindObjectOfType<ScoreManager>();
        entries = new List<ScoreEntry>();
        LoadScores();
    }

    public struct ScoreEntry {
        public string name;
        public int score;

        public ScoreEntry(string name, int score) {
            this.name = name;
            this.score = score;
        }
    }

    private List<ScoreEntry> entries;

    private void SortScores() {
        entries.Sort((a, b) => b.score.CompareTo(a.score));
    }

    // gets leaderboard entries from playerprefs and adds them to the list
    public void LoadScores() {
        if(entries != null){
            entries.Clear();
        }

        for (int i = 0; i < names.Count; ++i) {
            ScoreEntry entry;
            entry.name = PlayerPrefs.GetString(PlayerPrefsBaseKey + "[" + i + "].name", "");
            entry.score = PlayerPrefs.GetInt(PlayerPrefsBaseKey + "[" + i + "].score", 0);

            Debug.Log(entry.name);
            entries.Add(entry);
        }

        SortScores();
        for (int i = 0; i < names.Count; ++i) {
            names[i].text = entries[i].name;
            scores[i].text = entries[i].score.ToString();
        }
    }

    private void SaveScores() {
        for (int i = 0; i < names.Count; ++i) {
            var entry = entries[i];
            PlayerPrefs.SetString(PlayerPrefsBaseKey + "[" + i + "].name", entry.name);
            PlayerPrefs.SetInt(PlayerPrefsBaseKey + "[" + i + "].score", entry.score);
        }
    }

    public void Record() {
        entries.Add(new ScoreEntry(playerName.text, scoreManager.GetScore()));
        SortScores();
        entries.RemoveAt(entries.Count - 1);
        SaveScores();
        ShowScores();
    }

    public void ShowScores(){
        for (int i = 0; i < names.Count; ++i) {
            names[i].text = entries[i].name;
            scores[i].text = entries[i].score.ToString();
        }
    }

    public int GetLowestScore() {
        return entries[names.Count-1].score;
    }
}
