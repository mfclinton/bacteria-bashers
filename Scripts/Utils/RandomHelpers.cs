using UnityEngine;

public static class RandomHelpers
{
    public static Quaternion SmoothRandomRotation(float objectHash)
    {
        Vector2 random = new Vector2(Mathf.PerlinNoise(Time.time, objectHash), Mathf.PerlinNoise(objectHash, Time.time));
        return Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(0, 0, random.x * 360), random.y);
    }
    
    public static Vector2 RandomPointInBox2D(BoxCollider2D box)
    {
        Vector2 boxSize = box.size;
        Vector2 boxCenter = box.offset;

        // Local
        Vector2 randomPointLocal = new Vector2(
            Random.Range(-boxSize.x / 2, boxSize.x / 2),
            Random.Range(-boxSize.y / 2, boxSize.y / 2)
        );

        // World
        Vector2 randomPointWorld = box.transform.TransformPoint(boxCenter + randomPointLocal);
        return randomPointWorld;
    }
}
