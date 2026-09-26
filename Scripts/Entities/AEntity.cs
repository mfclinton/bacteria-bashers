using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Collider2D))]
public abstract class AEntity : MonoBehaviour, IDamageableSubscribable
{
    [SerializeField] private float maxHealth = 10f;

    // Events
    public event HealthModifiedEventHandler OnHealthModified;

    // State
    private float currentHealth;
    
    // Accessors
    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    protected void Awake()
    {
        currentHealth = maxHealth;
    }
    
    public void Configure(float difficulty)
    {
        // Max Health Range
        float r = 0.5f;
        float lowerMaxHealthBound = r * maxHealth;
        float upperMaxHealthBound = (r + difficulty) * maxHealth;
        
        // Set Health
        maxHealth = Random.Range(lowerMaxHealthBound, upperMaxHealthBound);
        currentHealth = maxHealth;
    }

    public void ModifyHealth(float delta)
    {
        currentHealth = Mathf.Clamp(currentHealth + delta, 0, maxHealth);
        OnHealthModified?.Invoke(this, delta);
        
        if (currentHealth <= 0)
            HandleDeath();
    }

    protected abstract void HandleDeath();
}