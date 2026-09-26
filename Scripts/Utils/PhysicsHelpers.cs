using UnityEngine;

public static class PhysicsHelpers
{
    #region Movement

    public static void MoveTowards_Smooth(Rigidbody2D rb, Vector2 goal, float minSpeed, float maxSpeed, float maxDistance)
    {
        Vector2 desiredVelocity = CalculateDesiredVelocity(rb.position, goal, minSpeed, maxSpeed, maxDistance);
        rb.linearVelocity = desiredVelocity;
    }

    public static void MoveTowards_WithAcceleration(Rigidbody2D rb, Vector2 goal, float minSpeed, float maxSpeed, float maxDistance, float accelerationRate)
    {
        Vector2 desiredVelocity = CalculateDesiredVelocity(rb.position, goal, minSpeed, maxSpeed, maxDistance);

        Vector2 acceleration = (desiredVelocity - rb.linearVelocity) * (accelerationRate * Time.deltaTime);
        rb.linearVelocity += acceleration;
    }

    private static Vector2 CalculateDesiredVelocity(Vector2 currentPosition, Vector2 goal, float minSpeed, float maxSpeed, float maxDistance)
    {
        // At Goal
        float distance = Vector2.Distance(currentPosition, goal);
        if (distance < Mathf.Epsilon)
            return Vector2.zero;

        // Calculate Speed
        float speedFactor = Mathf.Clamp01(distance / maxDistance);
        float speed = Mathf.Lerp(minSpeed, maxSpeed, speedFactor);

        // Calculate Direction
        Vector2 direction = (goal - currentPosition).normalized;
        return direction * speed;
    }

    #endregion
    
    #region Rotation
    
    public static void RotateTowards_Smooth(Rigidbody2D rb, Vector2 target, float rotationSpeed, float angleOffset)
    {
        // Calculate Target Angle
        Vector2 direction = (target - rb.position).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + angleOffset;
        
        // Smooth Rotation
        float smoothAngle = Mathf.MoveTowardsAngle(rb.rotation, targetAngle, rotationSpeed * Time.deltaTime);
        rb.MoveRotation(smoothAngle);
    }

    #endregion
}