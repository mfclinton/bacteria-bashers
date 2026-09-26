using UnityEngine;

public abstract class AStateMachineSubscriber : MonoBehaviour
{
    // References
    protected GameStateMachine gameStateMachine;

    #region Unity Callbacks

    protected virtual void Awake()
    {
        // References
        gameStateMachine = FindAnyObjectByType<GameStateMachine>();
    }

    protected virtual void OnEnable()
    {
        // Events
        gameStateMachine.OnStateTransition += OnStateTransition;
    }
    
    protected virtual void OnDisable()
    {
        // Events
        gameStateMachine.OnStateTransition -= OnStateTransition;
    }
    
    #endregion
    
    #region Event Handlers
    
    protected abstract void OnStateTransition(GameState previousState, GameState newState);
    
    #endregion
}
