using UnityEngine;

public class UIManager_PauseMenu : AStateMachineSubscriber
{
    [Header("References")]
    [SerializeField] private Canvas pauseMenuCanvas;
    
    protected override void OnStateTransition(GameState previousState, GameState newState)
    {
        bool pausing = newState == GameState.Paused;
        bool unpausing = previousState == GameState.Paused;
        
        if (pausing)
            pauseMenuCanvas.gameObject.SetActive(true);
        else if (unpausing)
            pauseMenuCanvas.gameObject.SetActive(false);
    }
}
