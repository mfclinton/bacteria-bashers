using System;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    // Events
    public delegate void ScoreEventHandler(int score, int delta);
    public event ScoreEventHandler OnScoreChanged;
    
    // State
    public int Score { get; private set; }
    
    public void AddScore(int score)
    {
        Score += score;
        OnScoreChanged?.Invoke(Score, score);
    }
}
