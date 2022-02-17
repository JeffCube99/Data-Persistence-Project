using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// This class only saves the top 5 high scores
public class HighScoreManager : MonoBehaviour
{
    public int highScore;
    public string highScorePlayerName = "N/A";


    public List<ScoreInfo> scores;
    private static string SAVE_FILE_PATH;

    private void Awake()
    {
        // Define the location of the save file.
        SAVE_FILE_PATH = Path.Combine(Application.persistentDataPath, "savefile.json");
        LoadHighScores();
    }

    // We use LevelData to store the number of levels the player has unlocked.
    // `[System.Serializable]` is needed for any class used with JSON Serializer API
    [System.Serializable]
    class HighScoreData
    {
        public List<ScoreInfo> scores;

        public HighScoreData (List<ScoreInfo> scores)
        {
            this.scores = scores;
        }
    }

    [System.Serializable]
    public class ScoreInfo
    {
        public int score;
        public string playerName;

        public ScoreInfo (int score, string playerName)
        {
            this.score = score;
            this.playerName = playerName;
        }
    }

    public void addScore(int score, string playerName)
    {
        ScoreInfo newRecord = new ScoreInfo(score, playerName);
        if (scores.Count > 0)
        {
            for (int i = 0; i < scores.Count; i++)
            {
                if (scores[i].score < newRecord.score)
                {
                    scores.Insert(i, newRecord);
                    break;
                }
            }
        }
        else
        {
            scores.Add(newRecord);
        }

        if (scores.Count > 5)
        {
            scores.RemoveRange(5, scores.Count - 5);
        }
        SaveHighScores();
    }

    public void SaveHighScores()
    {
        Debug.Log("Saving high scores");
        foreach (ScoreInfo s in scores)
        {
            Debug.Log($"SAVING Player: {s.playerName}, Score: {s.score}");
        }
        // Store information in a new LevelData class
        HighScoreData data = new HighScoreData(scores);

        // Convert LevelData class into a json string
        string jsonData = JsonUtility.ToJson(data);

        // Write the string to the file at SAVE_FILE_PATH
        File.WriteAllText(SAVE_FILE_PATH, jsonData);
    }

    public void LoadHighScores()
    {
        // We only load information if the save file exists
        if (File.Exists(SAVE_FILE_PATH))
        {
            // Load the json string from the savefile
            string jsonData = File.ReadAllText(SAVE_FILE_PATH);

            // Convert the json string back into a LevelData class
            HighScoreData data = JsonUtility.FromJson<HighScoreData>(jsonData);

            // Retrieve the levelsUnlocked from the loaded LevelData class
            scores = data.scores;
            Debug.Log("Loading high scores");
            foreach (ScoreInfo s in scores)
            {
                Debug.Log($"LOADING Player: {s.playerName}, Score: {s.score}");
            }
            if (scores.Count > 0)
            {
                highScore = data.scores[0].score;
                highScorePlayerName = data.scores[0].playerName;
            }
            else
            {
                highScore = 0;
                highScorePlayerName = "N/A";
            }
        }
        else
        {
            highScore = 0;
            highScorePlayerName = "N/A";
        }
    }

    public void ClearHighScores()
    {
        if (File.Exists(SAVE_FILE_PATH))
        {
            FileInfo highScoreFile = new FileInfo(SAVE_FILE_PATH);
            highScoreFile.Delete();
        }
    }
}
