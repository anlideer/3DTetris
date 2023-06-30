using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static UnityEvent ScoreChanged;
    public static UnityEvent GameEnded;
    public static bool IsGameEnd
    {
        get
        {
            return isGameEnd;
        }
        set
        {
            isGameEnd = value;
            if (value)
            {
                Time.timeScale = 0f;
                // deal with game end
                Debug.Log("game end");
                GameEnded.Invoke();
            }
        }
    }

    public static int Score 
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            ScoreChanged?.Invoke();
        }
    }

    private static int score;
    private static bool isGameEnd;

    private void Awake()
    {
        if (ScoreChanged == null)
            ScoreChanged = new UnityEvent();
        if (GameEnded == null) 
            GameEnded = new UnityEvent();
    }

    private void Start()
    {
        Score = 0;
        GameEnded?.AddListener(OnGameEnd);
    }

    private void OnGameEnd()
    {
        bool isHighest = false;
        int highest = PlayerPrefs.GetInt("Score", 0);
        if (Score > highest)
        {
            isHighest = true;
            highest = Score;
            PlayerPrefs.SetInt("Score", Score);
        }
        GetComponent<SystemUI>().ShowEndMenu(Score, highest, isHighest);
    }
}
