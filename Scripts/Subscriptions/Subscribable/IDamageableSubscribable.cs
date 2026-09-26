public delegate void HealthModifiedEventHandler(IDamageableSubscribable subject, float delta);

public interface IDamageableSubscribable
{
    event HealthModifiedEventHandler OnHealthModified;
    
    // Properties
    public float CurrentHealth { get; }
    public float MaxHealth { get; }
    
    // Methods
    public void ModifyHealth(float delta);
}