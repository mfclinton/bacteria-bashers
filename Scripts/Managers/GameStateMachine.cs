using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameStateMachine : MonoBehaviour
{
    // Events
    public delegate void GameStateTransitionHandler(GameState previousState, GameState newState);
    public event GameStateTransitionHandler OnStateTransition;

    // Internals
    private Dictionary<GameState, List<GameState>> validTransitions;
    
    // State
    public GameState CurrentState { get; private set; }

    #region Unity Callbacks

    private void Awake()
    {
        // Initialize
        CurrentState = GameState.MainMenu;
        InitializeValidTransitions();
    }

    private void Start()
    {
        // Initial Event
        OnStateTransition?.Invoke(GameState.MainMenu, GameState.MainMenu);
    }

    #endregion

    public void Transition(GameState newState)
    {
        if (validTransitions[CurrentState].Contains(newState))
        {
            // Update State
            var previousState = CurrentState;
            CurrentState = newState;

            // Event
            OnStateTransition?.Invoke(previousState, newState);
        }
    }
    
    private void InitializeValidTransitions()
    {
        validTransitions = new Dictionary<GameState, List<GameState>>
        {
            { GameState.MainMenu, new List<GameState> { GameState.Playing } },
            { GameState.Playing, new List<GameState> { GameState.Paused, GameState.GameOver } },
            { GameState.Paused, new List<GameState> { GameState.Playing } },
            { GameState.GameOver, new List<GameState> { GameState.MainMenu } }
        };
    }
}