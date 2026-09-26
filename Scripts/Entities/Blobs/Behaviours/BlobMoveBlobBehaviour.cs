using UnityEngine;
using UnityEngine.Serialization;

public class BlobMoveBlobBehaviour : ABlobBehaviour
{
    [Header("Smooth Move Settings")]
    [SerializeField] private float maxSpeedDistanceToTarget = 20f;
    [SerializeField] private Vector2 speedRange = new Vector2(0.5f, 1.5f);
    
    [Header("Acceleration Settings")]
    [SerializeField] private Vector2 accelerationRange = new Vector2(0.5f, 2f);
    
    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 500f;
    [SerializeField] private float angleOffset = 0f;
    
    public override void Execute(BlobManagerState blobManagerState, Blob_Entity blob)
    {
        Move(blobManagerState, blob);
        Rotate(blob);
    }

    #region Helpers

    private void Move(BlobManagerState blobManagerState, Blob_Entity blob)
    {
        Vector2 target = blobManagerState.BlobControlZone.GetFloatingPosition(blob.GetHashCode());
        
        // Move
        float accelerationT = 1f - blobManagerState.BlobControlZone.AccumulatedTravelPercentage;
        float accelerationRate = Mathf.Lerp(accelerationRange.x, accelerationRange.y, accelerationT);
        PhysicsHelpers.MoveTowards_WithAcceleration(blob.Rb, target, speedRange.x, speedRange.y, maxSpeedDistanceToTarget, accelerationRate);
    }
    
    private void Rotate(Blob_Entity blob)
    {
        // Rotate
        PhysicsHelpers.RotateTowards_Smooth(blob.Rb, blob.Rb.position + blob.Rb.linearVelocity, rotationSpeed, angleOffset);
    }

    #endregion
}
    