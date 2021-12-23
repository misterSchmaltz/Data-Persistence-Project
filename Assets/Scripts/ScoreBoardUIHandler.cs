using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreBoardUIHandler : MonoBehaviour
{
    [SerializeField]
    private Transform entryContainer;
    [SerializeField]
    private Transform entryTemplate;

    void Awake()
    {
        ScoreDataManager.Instance.LoadHighScores();
        entryTemplate.gameObject.SetActive(false);
        for (int i = 0; i < 5; i++)
        {
            Transform newEntry = Instantiate(entryTemplate, entryContainer);
            //newEntry.RectTransform.anchoredPosition(new Vector2(0, -100 * i));
            RectTransform entryRectTransform = newEntry.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -100 * i);
            newEntry.gameObject.SetActive(true);

            int rank = i + 1;
            newEntry.Find("Rank Text").GetComponent<Text>().text = rank.ToString();
            newEntry.Find("Player Name Text").GetComponent<Text>().text = ScoreDataManager.Instance.activeBoard.NameBoard[i];
            newEntry.Find("Score Text").GetComponent<Text>().text = ScoreDataManager.Instance.activeBoard.ScoreBoard[i].ToString();
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
