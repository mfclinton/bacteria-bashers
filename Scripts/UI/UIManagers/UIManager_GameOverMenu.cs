using UnityEngine;

public class UIManager_GameOverMenu : AStateMachineSubscriber
{
    [Header("References")]
    [SerializeField] private Canvas gameMenuCanvas;
    [SerializeField] private Animator gameMenuAnimator;
    
    protected override void OnStateTransition(GameState previousState, GameState newState)
    {
        bool isEnabled = newState == GameState.GameOver;
        gameMenuCanvas.gameObject.SetActive(isEnabled);
    }
}
