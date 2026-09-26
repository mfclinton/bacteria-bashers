using System;
using UnityEngine;

public class UIHelper_GameStateMachine : MonoBehaviour
{
    // References
    private GameStateMachine gameStateMachine;

    private void Awake()
    {
        // Initialize
        gameStateMachine = FindAnyObjectByType<GameStateMachine>();
    }
    
    #region Button Events
    
    public void TransitionToMainMenu()
    {
        gameStateMachine.Transition(GameState.MainMenu);
    }
    
    public void TransitionToPlaying()
    {
        gameStateMachine.Transition(GameState.Playing);
    }
    
    public void TransitionToPaused()
    {
        gameStateMachine.Transition(GameState.Paused);
    }
    
    public void TransitionToGameOver()
    {
        gameStateMachine.Transition(GameState.GameOver);
    }
    
    public void TogglePause()
    {
        GameState targetState = gameStateMachine.CurrentState == GameState.Paused ? GameState.Playing : GameState.Paused;
        gameStateMachine.Transition(targetState);
    }
    
    #endregion
}
