using UnityEngine;

public class DamageScoreTracker : ADamageableSubscriber
{
    [Header("Settings")]
    [SerializeField] private int scorePerDamage = 1;
    
    // References
    private ProgressManager progressManager;

    protected override void Awake()
    {
        base.Awake();
        
        // References
        progressManager = FindAnyObjectByType<ProgressManager>();
    }

    protected override void OnHealthChanged(IDamageableSubscribable subject, float delta)
    {
        if (delta >= 0)
            return;
        
        float damage = Mathf.Abs(delta);
        
        int score = Mathf.RoundToInt(damage * scorePerDamage);
        progressManager.AddScore(score);
    }
}
