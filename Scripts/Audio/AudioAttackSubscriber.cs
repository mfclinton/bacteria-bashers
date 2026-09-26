using UnityEngine;
using FMODUnity;

public class AudioAttackSubscriber : AAttackSubscriber
{
    [SerializeField] private StudioEventEmitter attackPreparingEmitter;
    [SerializeField] private StudioEventEmitter attackFiredEmitter;
    
    protected override void OnAttackPreparing()
    {
        attackPreparingEmitter?.Play();
    }

    protected override void OnAttackFired()
    {
        attackFiredEmitter?.Play();
    }
}
