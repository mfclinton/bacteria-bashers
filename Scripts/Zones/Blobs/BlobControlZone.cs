using System;
using UnityEngine;

public class BlobControlZone : MonoBehaviour
{
    [Header("Radius Size Settings")]
    [SerializeField] private Vector2 radiusRange = new Vector2(.5f, 1.25f);

    [SerializeField] private float accumulatedTravelDistanceMultiplier = 1f;
    [SerializeField] private float accumulatedTravelTimeDecay = 3f;
    [SerializeField] private float maxAccumulatedTravel = 5f;

    [Header("Floating Position Settings")]
    [SerializeField] private float noiseScale = 1f;
    
    // State
    private float accumulatedTravel;
    public float AccumulatedTravelPercentage => Mathf.InverseLerp(0f, maxAccumulatedTravel, accumulatedTravel);

    #region Unity Callbacks
    
    private void Update()
    {
        ApplyAccumulatedTravelDecay();
        UpdateRadius();
    }

    #endregion

    #region Position Functions

    public Vector2 GetFloatingPosition(float objectHash)
    {
        objectHash %= 10000;
        
        float t = Time.time * noiseScale + objectHash;
        
        float theta = Mathf.PerlinNoise(objectHash, t) * Mathf.PI * 2f;
        
        float radius = Mathf.Sqrt(Mathf.PerlinNoise(t, objectHash)) * GetRadius();
    
        float offsetX = radius * Mathf.Cos(t + theta);
        float offsetY = radius * Mathf.Sin(t + theta);

        return (Vector2)transform.position + new Vector2(offsetX, offsetY);
    }

    #endregion
    
    #region Update Functions
    
    public void Move(Vector2 target)
    {
        float distance = Vector2.Distance(transform.position, target);
        if (distance < Mathf.Epsilon)
            return;
        
        // Accumulate Travel
        float travelDelta = distance * accumulatedTravelDistanceMultiplier;
        accumulatedTravel = Mathf.Clamp(accumulatedTravel + travelDelta, 0f, maxAccumulatedTravel);
        
        // Set Position
        transform.position = target;
    }
    
    private void ApplyAccumulatedTravelDecay()
    {
        float decayAmount = accumulatedTravelTimeDecay * Time.deltaTime;
        accumulatedTravel = Mathf.Clamp(accumulatedTravel - decayAmount, 0f, maxAccumulatedTravel);
    }

    private void UpdateRadius()
    {
        float radiusT = 1f - AccumulatedTravelPercentage;
        float r = Mathf.Lerp(radiusRange.x, radiusRange.y, radiusT);
        SetRadius(r);
    }

    #endregion
    
    #region Radius Helpers

    protected void SetRadius(float radius)
    {
        transform.localScale = Vector3.one * (2 * radius);
    }
    
    protected float GetRadius()
    {
        return transform.localScale.x / 2f;
    }

    #endregion
}
