using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if UNITY_EDITOR
    using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public Text bestScoreText;
    public InputField nameInputField;

    public void StartNew()
    {
        ScoreDataManager.Instance.SetPlayerName(nameInputField.text);
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

    void Start()
    {
        UpdateBestScore();
    }

    void UpdateBestScore()
    {
        bestScoreText.text = "Best Score: " + ScoreDataManager.Instance.activeBoard.NameBoard[0] + ": " + ScoreDataManager.Instance.activeBoard.ScoreBoard[0];
    }

    public void EnterHighScoreScene()
    {
        SceneManager.LoadScene(2);
    }
}
