using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;

public class ScoreDataManager : MonoBehaviour
{
    private string PlayerName;
    public static ScoreDataManager Instance;

    public class HighScoreBoard
    {
        public string [] NameBoard = new string [5];
        public int [] ScoreBoard = new int [5];
    }

    public HighScoreBoard activeBoard = new HighScoreBoard();

    [System.Serializable]
    class SaveData
    {
        public string [] NameBoard = new string [5];
        public int [] ScoreBoard = new int [5];
    }

    public void SetPlayerName(string enteredName)
    {
        PlayerName = enteredName;
    }

    public void SaveHighestScore(int recentScore)
    {
        SaveData data = new SaveData();
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            LoadHighScores();
            EnterScore(recentScore);
            System.Array.Copy(activeBoard.ScoreBoard, data.ScoreBoard, 5);
            System.Array.Copy(activeBoard.NameBoard, data.NameBoard, 5);
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }
        
        else
        {
            data.NameBoard [0] = PlayerName;
            data.ScoreBoard [0] = recentScore;
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }
    }

    public void LoadHighScores()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            for (int i = 0; i < activeBoard.NameBoard.Length; i++)
            {
                activeBoard.NameBoard [i] = data.NameBoard[i];
                activeBoard.ScoreBoard [i] = data.ScoreBoard[i];
            }
        }
    }

    public void EnterScore(int scoreEntry)
    {
        int comparingValue = scoreEntry;
        string shiftingString = PlayerName;
        for (int i = 0; i < activeBoard.ScoreBoard.Length; i++)
        {
            if (comparingValue > activeBoard.ScoreBoard[i])
            {
                int tempInt = activeBoard.ScoreBoard[i];
                activeBoard.ScoreBoard[i] = comparingValue;
                comparingValue = tempInt;
                
                string tempString = activeBoard.NameBoard[i];
                activeBoard.NameBoard[i] = shiftingString;
                shiftingString = tempString;
                
            }
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighScores();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

}
