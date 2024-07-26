using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager Instance;

    public static int Score = 0;


    public event ChangeScoreEvent OnScoreChanged;
    public delegate void ChangeScoreEvent();

    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else { Destroy(gameObject); }
    }
 

    public void AddScore(int amount)
    {
        Score += amount;
        OnScoreChanged?.Invoke();
    }

    public int GetScore()
    {
        return Score;
    }

    public void ResetScore()
    {
        Score = 0;
    }
}
