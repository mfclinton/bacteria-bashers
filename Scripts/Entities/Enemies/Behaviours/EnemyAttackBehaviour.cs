using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAttackBehaviour : AEnemyBehaviour, IAttackSubscribable
{
    [Header("References")]
    [SerializeField] private EnemyAttackZone enemyAttackZone;
    
    [Header("Attack Settings")]
    [SerializeField] private float timeToAttack = 1.25f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private Vector2 randomAttackCooldownRange = new Vector2(3f, 8f);
    
    // Events
    public event AttackEventHandler OnAttackPreparing;
    public event AttackEventHandler OnAttackFired;
    
    // State
    private float lastAttackTime;
    private float randomAttackCooldown;
    public bool AttackCooldownReady => Time.time - lastAttackTime > attackCooldown;
    
    // Internal
    private Coroutine attackCoroutine;

    private void Awake()
    {
        // Initialize
        lastAttackTime = Time.time;
        randomAttackCooldown = Random.Range(randomAttackCooldownRange.x, randomAttackCooldownRange.y);
    }
    
    public void Configure(float multiplier)
    {
        // Attack Cooldown
        float r = 0.25f;
        float lowerAttackCooldown = attackCooldown * (1f - r);
        float maxAttackCooldown = attackCooldown * (1f + r) * (1f - multiplier);
        attackCooldown = Random.Range(lowerAttackCooldown, maxAttackCooldown);
        
        // Random Attack Cooldown
        r = 0.25f;
        float lowerRandomAttackCooldown = randomAttackCooldownRange.x * (1f - r);
        float maxRandomAttackCooldown = randomAttackCooldownRange.x * (1f + r) * (1f - multiplier);
        randomAttackCooldownRange = new Vector2(Random.Range(lowerRandomAttackCooldown, maxRandomAttackCooldown), randomAttackCooldownRange.y);
    }

    public override void Execute(BlobManagerState blobManagerState, Enemy_Entity enemy)
    {
        if (!AttackCooldownReady || attackCoroutine != null)
            return;
        
        bool targetInAttackZone = enemyAttackZone.TrackedComponents.Count > 0;
        bool canRandomAttack = Time.time - lastAttackTime > randomAttackCooldown;
        if (targetInAttackZone || canRandomAttack)
            TriggerAttackCoroutine();
    }

    #region Helpers

    private void TriggerAttackCoroutine()
    {
        if (attackCoroutine != null)
            StopCoroutine(attackCoroutine);
        
        attackCoroutine = StartCoroutine(AttackCoroutine());
    }
    
    private IEnumerator AttackCoroutine()
    {
        OnAttackPreparing?.Invoke();
        yield return new WaitForSeconds(timeToAttack);
        Attack();
        
        attackCoroutine = null;
    }
    
    public void Attack()
    {
        foreach (Blob_Entity target in enemyAttackZone.TrackedComponents)
        {
            // TODO
            target.ModifyHealth(-1f);
        }
        
        OnAttackFired?.Invoke();
        lastAttackTime = Time.time;
        randomAttackCooldown = Random.Range(randomAttackCooldownRange.x, randomAttackCooldownRange.y);
    }

    #endregion

}
