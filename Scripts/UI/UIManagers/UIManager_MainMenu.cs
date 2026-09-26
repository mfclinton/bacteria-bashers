using System;
using UnityEngine;

public class UIManager_MainMenu : AStateMachineSubscriber
{
    [Header("References")]
    [SerializeField] private Canvas mainMenuCanvas;
    [SerializeField] private Animator mainMenuAnimator;
    
    protected override void OnStateTransition(GameState previousState, GameState newState)
    {
        bool isEnabled = newState == GameState.MainMenu;
        mainMenuAnimator.SetBool("isOpen", isEnabled);
    }
}
