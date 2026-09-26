using FMODUnity;
using UnityEngine;

public class AudioDamageSubscriber : ADamageableSubscriber
{
    [SerializeField] private StudioEventEmitter damageEmitter;
    [SerializeField] private StudioEventEmitter deathEmitter;
    
    protected override void OnHealthChanged(IDamageableSubscribable subject, float delta)
    {
        if (delta < 0)
            damageEmitter?.Play();
        
        // TODO: Temp Death
        if (subject.CurrentHealth <= 0)
            deathEmitter?.Play();
    }
}
