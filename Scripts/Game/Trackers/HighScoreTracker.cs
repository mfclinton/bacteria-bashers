using UnityEngine;

public class HighScoreTracker : AStateMachineSubscriber
{
    // Events
    public delegate void HighScoreEventHandler(int highScore);
    public event HighScoreEventHandler OnHighScoreChanged;

    // References
    ProgressManager progressManager;
    
    protected override void Awake()
    {
        base.Awake();
        
        // References
        progressManager = FindAnyObjectByType<ProgressManager>();
    }

    protected override void OnStateTransition(GameState previousState, GameState newState)
    {
        if (newState == GameState.GameOver)
            TryUpdateHighScore(progressManager.Score);
    }
    
    private void TryUpdateHighScore(int newScore)
    {
        string highScoreKey = PlayerPrefKey.HighScoreInt.Name;
        int currentHighScore = PlayerPrefs.GetInt(highScoreKey, 0);
        if (newScore > currentHighScore)
        {
            PlayerPrefs.SetInt(highScoreKey, newScore);
            OnHighScoreChanged?.Invoke(newScore);
        }
    }
}
