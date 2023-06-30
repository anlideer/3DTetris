using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SystemUI : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public GameObject EndPanel;
    public TextMeshProUGUI FinalScoreText;
    public TextMeshProUGUI HighestScoreText;
    public GameObject HighestInfo;

    private void Start()
    {
        GameManager.ScoreChanged.AddListener(UpdateScoreText);
    }

    public void ShowEndMenu(int score, int highestScore, bool isNewHighest)
    {
        EndPanel.SetActive(true);
        FinalScoreText.text = score.ToString();
        HighestScoreText.text = highestScore.ToString();
        HighestInfo.SetActive(isNewHighest);
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("OpenScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void UpdateScoreText()
    {
        ScoreText.text = GameManager.Score.ToString();
    }
}
