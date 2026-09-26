using UnityEngine;
using UnityEngine.Serialization;

public class EnemyMoveBehaviour : AEnemyBehaviour
{
    [Header("Smooth Move Settings")]
    [SerializeField] private float maxSpeedDistanceToTarget = 5f;
    [SerializeField] private Vector2 speedRange = new Vector2(0.5f, 1.5f);
    
    [Header("Stopping Settings")]
    [SerializeField] private float stoppingSpeed = 1f;
    [SerializeField] private float stoppingDistance = 1f;

    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float angleOffset = 0f;

    [Header("Push Zone")]
    [SerializeField] private EnemyPushZone enemyPushZone;
    [SerializeField] private float pushForce = 15f;
    
    public void Configure(float difficulty)
    {
        // Speed
        float r = 0.25f;
        Vector2 lowerSpeedRange = speedRange * (1f - r);
        Vector2 maxSpeedRange = speedRange * (1f + r) * (1f + difficulty);
        speedRange = new Vector2(Random.Range(lowerSpeedRange.x, maxSpeedRange.x), Random.Range(lowerSpeedRange.y, maxSpeedRange.y));
        
        // Rotation Speed
        r = 0.25f;
        float lowerRotationSpeed = rotationSpeed * (1f - r);
        float maxRotationSpeed = rotationSpeed * (1f + r) * (1f + difficulty);
        rotationSpeed = Random.Range(lowerRotationSpeed, maxRotationSpeed);
    }
    
    public override void Execute(BlobManagerState blobManagerState, Enemy_Entity enemy)
    {
        MoveAndRotate(blobManagerState, enemy);
        
        if (enemyPushZone.TrackedComponents.Count > 0)
        {
            enemyPushZone.TrackedComponents.ForEach(collider2D =>
            {
                Vector2 direction = (enemy.Rb.position - (Vector2)collider2D.transform.position).normalized;
                enemy.Rb.linearVelocity += direction * (pushForce * Time.deltaTime);
            });
        }
    }
    
    private void MoveAndRotate(BlobManagerState blobManagerState, Enemy_Entity enemy)
    {
        // Variables
        Rigidbody2D rb = enemy.Rb;
        
        Vector2 target = blobManagerState.Centroid;
        target = PlayableAreaManager.Instance.ClampPositionWithinPlayableArea(target);
        
        // Rotate
        PhysicsHelpers.RotateTowards_Smooth(enemy.Rb, target, rotationSpeed, angleOffset);

        float distance = Vector2.Distance(rb.position, target);
        if (distance > stoppingDistance)
            PhysicsHelpers.MoveTowards_Smooth(enemy.Rb, target, speedRange.x, speedRange.y, maxSpeedDistanceToTarget);
        else
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, stoppingSpeed * Time.deltaTime);
    }
}
