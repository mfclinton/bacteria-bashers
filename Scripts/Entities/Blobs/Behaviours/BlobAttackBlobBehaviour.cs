using System;
using UnityEngine;

public class BlobAttackBlobBehaviour : ABlobBehaviour, IAttackSubscribable
{
    [Header("Attack Settings")]
    [SerializeField] private Vector2 attackSpeedRange = new Vector2(0.5f, 1.5f);
    [SerializeField] private float attackAcceleration = 5f;
    [SerializeField] private float attackCooldown = 0.5f;
    
    [Header("Positioning Settings")]
    [SerializeField] private Vector2 positionSpeedRange = new Vector2(0.5f, 1.5f);
    [SerializeField] private float positionForAttackDistance = 1f; // TODO: Fix this
    
    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 1000f;
    [SerializeField] private float angleOffset = 0f;
    
    // Events
    public event AttackEventHandler OnAttackPreparing;
    public event AttackEventHandler OnAttackFired;
    
    // State
    private float lastAttackTime = 0f;
    public bool AttackCooldownReady => Time.time - lastAttackTime > attackCooldown;
    public bool InPositionForAttack { get; private set; }

    private void OnCollisionEnter2D(Collision2D other)
    {
        bool isEnemy = LayerName.GetLayerIndex(LayerName.Layer.Enemy) == other.gameObject.layer;
        if (isEnemy && AttackCooldownReady)
        {
            // Damage
            IDamageableSubscribable damageable = other.gameObject.GetComponent<IDamageableSubscribable>();
            damageable?.ModifyHealth(-1f); // TODO
            
            // Update State
            InPositionForAttack = false;
            lastAttackTime = Time.time;
            
            // Events
            OnAttackFired?.Invoke();
        }
    }

    public override void Execute(BlobManagerState blobManagerState, Blob_Entity blob)
    {
        Collider2D target = blobManagerState.BlobAttackZone.GetClosestTarget(blob.transform.position);
        if (target == null)
            return;
        
        RotateTowards(blob, target);
        if (InPositionForAttack && AttackCooldownReady)
        {
            Attack(blob, target);
        }
        else
        {
            PositionForAttack(blobManagerState, blob, target);
        }
    }

    #region Helpers

    private void Attack(Blob_Entity blob, Collider2D target)
    {
        PhysicsHelpers.MoveTowards_WithAcceleration(blob.Rb, target.transform.position, attackSpeedRange.x, attackSpeedRange.y, positionForAttackDistance, attackAcceleration);
    }
    
    private void PositionForAttack(BlobManagerState blobManagerState, Blob_Entity blob, Collider2D target)
    {
        // Update InPositionForAttack
        // TODO
        if (Vector2.Distance(blob.transform.position, target.transform.position) > positionForAttackDistance * 0.75f)
            InPositionForAttack = true;
        
        // Get the position to move to
        Vector2 currentPosition = blob.transform.position;
        Vector2 positionForAttack = blobManagerState.BlobAttackZone.GetAttackingPosition(currentPosition, target, positionForAttackDistance, blob.GetHashCode());
        
        // Move to the position
        PhysicsHelpers.MoveTowards_Smooth(blob.Rb, positionForAttack, positionSpeedRange.x, positionSpeedRange.y, positionForAttackDistance);
    }
    
    private void RotateTowards(Blob_Entity blob, Collider2D target)
    {
        PhysicsHelpers.RotateTowards_Smooth(blob.Rb, target.transform.position, rotationSpeed, angleOffset);
    }

    #endregion
}
