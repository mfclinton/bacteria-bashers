using EasyTransition;
using UnityEngine;

public class UIHelper_TransitionManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private TransitionSettings transitionSettings;
    
    public void TransitionToGameScene()
    {
        string sceneName = SceneNames.GetSceneName(SceneNames.SceneEnum.Game);
        TransitionManager.Instance().Transition(sceneName, transitionSettings, 0f);
    }
}
