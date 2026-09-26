using UnityEngine;

public class UIManager_GameMenu : AStateMachineSubscriber
{
    [Header("References")]
    [SerializeField] private Canvas gameMenuCanvas;
    
    protected override void OnStateTransition(GameState previousState, GameState newState)
    {
        bool isEnabled = newState != GameState.MainMenu;
        gameMenuCanvas.gameObject.SetActive(isEnabled);
    }
}
