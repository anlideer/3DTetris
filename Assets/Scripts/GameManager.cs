using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static UnityEvent ScoreChanged;
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

    private void Awake()
    {
        if (ScoreChanged == null)
            ScoreChanged = new UnityEvent();
    }

    private void Start()
    {
        Score = 0;
    }
}
