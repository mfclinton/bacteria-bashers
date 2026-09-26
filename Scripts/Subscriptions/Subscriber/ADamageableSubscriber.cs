using UnityEngine;

public abstract class ADamageableSubscriber : MonoBehaviour
{
    // References
    protected IDamageableSubscribable damageableSubscribable;

    #region Unity Callbacks

    protected virtual void Awake()
    {
        // References
        damageableSubscribable = GetComponent<IDamageableSubscribable>();
    }

    protected virtual void OnEnable()
    {
        // Events
        damageableSubscribable.OnHealthModified += OnHealthChanged;
    }
    
    protected virtual void OnDisable()
    {
        // Events
        damageableSubscribable.OnHealthModified -= OnHealthChanged;
    }
    
    #endregion
    
    #region Event Handlers

    protected abstract void OnHealthChanged(IDamageableSubscribable subject, float delta);

    #endregion
}
