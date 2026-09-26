using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayableAreaManager : MonoBehaviour
{
    // References
    public BoxCollider2D PlayableAreaCollider { get; private set; }
    
    // Singleton
    public static PlayableAreaManager Instance { get; private set; }

    private void Awake()
    {
        // Singleton
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        // References
        PlayableAreaCollider = GetComponent<BoxCollider2D>();
    }
    
    public Vector2 ClampPositionWithinPlayableArea(Vector2 position)
    {
        return new Vector2(
            Mathf.Clamp(position.x, PlayableAreaCollider.bounds.min.x, PlayableAreaCollider.bounds.max.x),
            Mathf.Clamp(position.y, PlayableAreaCollider.bounds.min.y, PlayableAreaCollider.bounds.max.y)
        );
    }

    private void OnDrawGizmosSelected()
    {
        if (PlayableAreaCollider != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(PlayableAreaCollider.bounds.center, PlayableAreaCollider.bounds.size);
        }
    }

}
