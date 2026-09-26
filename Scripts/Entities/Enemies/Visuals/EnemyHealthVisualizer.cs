using UnityEngine;

[RequireComponent(typeof(IDamageableSubscribable))]
public class EnemyHealthVisualizer : ADamageableSubscriber
{
    [Header("References")]
    [SerializeField] private HealthBarVisual healthBarVisual;
    
    #region Event Handlers

    protected override void OnHealthChanged(IDamageableSubscribable subject, float delta)
    {
        float t = subject.CurrentHealth / subject.MaxHealth;
        healthBarVisual.SetHealthBarT(t);
    }
    
    #endregion
}
