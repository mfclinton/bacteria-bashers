using UnityEngine;

public abstract class AAttackSubscriber : MonoBehaviour
{
    // References
    private IAttackSubscribable attackSubscribable;
    
    #region Unity Callbacks
    
    protected virtual void Awake()
    {
        // References
        attackSubscribable = GetComponent<IAttackSubscribable>();
    }
    
    protected virtual void OnEnable()
    {
        // Events
        attackSubscribable.OnAttackFired += OnAttackFired;
    }
    
    protected virtual void OnDisable()
    {
        // Events
        attackSubscribable.OnAttackFired -= OnAttackFired;
    }
    
    #endregion
    
    #region Event Handlers
    
    protected abstract void OnAttackPreparing();
    protected abstract void OnAttackFired();
    
    #endregion
}
