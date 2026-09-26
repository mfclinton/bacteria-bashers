using UnityEngine;

public class PauseController : AStateMachineSubscriber
{
    // References
    private PlayerController playerController;

    #region Unity Callbacks

    protected override void Awake()
    {
        base.Awake();
        
        // Get References
        playerController = GetComponent<PlayerController>();
    }
    
    protected override void OnEnable()
    {
        base.OnEnable();
        
        // Events
        playerController.OnPausePressed += OnPausePressed;
    }
    
    protected override void OnDisable()
    {
        base.OnDisable();
        
        // Events
        playerController.OnPausePressed -= OnPausePressed;
    }

    #endregion

    #region Event Callbacks

    private void OnPausePressed()
    {
        GameState targetState = gameStateMachine.CurrentState == GameState.Paused ? GameState.Playing : GameState.Paused;
        gameStateMachine.Transition(targetState);
    }
    
    protected override void OnStateTransition(GameState previousState, GameState newState)
    {
        bool pausing = newState == GameState.Paused;
        bool unpausing = previousState == GameState.Paused;
        if (pausing)
            Time.timeScale = 0;
        else if (unpausing)
            Time.timeScale = 1;
    }

    #endregion
}
