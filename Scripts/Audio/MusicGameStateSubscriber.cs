using FMODUnity;
using UnityEngine;

public class MusicGameStateSubscriber : AStateMachineSubscriber
{
    [SerializeField] private StudioEventEmitter musicEmitter;
    
    protected override void OnStateTransition(GameState previousState, GameState newState)
    {
        // Set all to 0 on main menu
        if (newState == GameState.MainMenu)
        {
            musicEmitter.SetParameter(MusicFMODKeyConstants.T1, 0);
            musicEmitter.SetParameter(MusicFMODKeyConstants.T2, 0);
            musicEmitter.SetParameter(MusicFMODKeyConstants.T3, 0);
            musicEmitter.SetParameter(MusicFMODKeyConstants.GameOver, 0);
            RuntimeManager.StudioSystem.setParameterByName(MusicFMODKeyConstants.PauseGame, 0);
        }
        
        // Set T1 to 1 on game start
        if (newState == GameState.Playing)
        {
            musicEmitter.SetParameter(MusicFMODKeyConstants.T1, 1);
        }
        
        // Set PauseGame to 1 on pause
        if (newState == GameState.Paused)
        {
            RuntimeManager.StudioSystem.setParameterByName(MusicFMODKeyConstants.PauseGame, 1);
        }
        else if (previousState == GameState.Paused)
        {
            RuntimeManager.StudioSystem.setParameterByName(MusicFMODKeyConstants.PauseGame, 0);
        }
        
        // Set GameOver to 1 on game over
        if (newState == GameState.GameOver)
        {
            musicEmitter.SetParameter(MusicFMODKeyConstants.GameOver, 1);
        }
    }
}
