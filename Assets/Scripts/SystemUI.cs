using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SystemUI : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;

    private void Start()
    {
        GameManager.ScoreChanged.AddListener(UpdateScoreText);
    }

    private void UpdateScoreText()
    {
        ScoreText.text = GameManager.Score.ToString();
    }
}
