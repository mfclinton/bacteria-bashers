using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BlobAttackZone : TrackedComponentZone<Collider2D>
{
    [Header("Floating Position Settings")]
    [SerializeField] private float noiseScale = 1f;
    
    [Header("Attacking Position Settings")]
    [SerializeField] private float rotationSpeed = 120f;
    
    #region Unity Callbacks
    
    private void Awake()
    {
        base.Awake();
        layerName = LayerName.Layer.Enemy;
    }
    
    #endregion

    #region Position Functions

    public Vector2 GetAttackingPosition(Vector2 currentPosition, Collider2D target, float positionForAttackDistance, float objectHash)
    {
        objectHash %= 10000;
        
        // Calculate Target Rotation
        Vector2 targetPosition = target.transform.position; // TODO: positionForAttackDistance needs to account for target size
        
        Vector2 currentPositionToTarget = currentPosition - targetPosition;
        Vector2 attackZonePositionToTarget = (Vector2)transform.position - targetPosition;
        
        Quaternion totalRotation = Quaternion.FromToRotation(currentPositionToTarget, attackZonePositionToTarget);
        Quaternion randomRotation = RandomHelpers.SmoothRandomRotation(objectHash);
        
        bool isInAttackZone = Vector2.Distance(currentPosition, transform.position) < GetRadius();
        Quaternion rot = isInAttackZone ? randomRotation : totalRotation;
        
        Quaternion targetRotation = Quaternion.Slerp(Quaternion.identity, rot, Time.deltaTime * rotationSpeed);

        // Calculate Attacking Position
        Vector2 offset = targetRotation * currentPositionToTarget * positionForAttackDistance;
        
        Vector2 attackingPosition = targetPosition + offset;
        return attackingPosition;
    }

    #endregion
    
    #region Helpers

    public Collider2D GetClosestTarget(Vector3 transformPosition)
    {
        Collider2D closestEnemy = null;
        float minDistance = float.MaxValue;

        foreach (Collider2D enemy in TrackedComponents)
        {
            float distance = Vector2.Distance(transformPosition, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    #endregion
    
    #region Radius Helpers
    
    protected float GetRadius()
    {
        return transform.lossyScale.x / 2f;
    }

    #endregion

}
