using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [Header("Playable Area Settings")]
    [Tooltip("Assign the BoxCollider2D defining the playable area.")]
    [SerializeField] private BoxCollider2D playableAreaCollider;

    [Header("Aspect Ratio Settings")]
    [Tooltip("Set the desired aspect ratio (width:height).")]
    [SerializeField] private Vector2 fixedAspectRatio = new Vector2(9, 19);

    [Header("Camera Settings")]
    [Tooltip("Desired vertical size of the camera view.")]
    [SerializeField] private float desiredVerticalSize = 10f;

    private Camera cam;
    private float targetAspect;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        cam.orthographic = true;

        if (playableAreaCollider == null)
        {
            Debug.LogError("Playable Area Collider is not assigned in the CameraController.");
        }

        // Calculate the target aspect ratio
        targetAspect = fixedAspectRatio.x / fixedAspectRatio.y;
    }

    private void Start()
    {
        AdjustCamera();
    }

    private void AdjustCamera()
    {
        if (playableAreaCollider == null) return;

        // Set the camera's orthographic size based on the desired vertical size
        cam.orthographicSize = desiredVerticalSize;

        // Calculate the current window's aspect ratio
        float windowAspect = (float)Screen.width / Screen.height;

        // Calculate the scaling factor to maintain the fixed aspect ratio
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            // Window is too tall, adjust orthographic size based on width
            cam.orthographicSize = desiredVerticalSize / scaleHeight;
        }
        else
        {
            // Window is wide enough, keep the desired orthographic size
            cam.orthographicSize = desiredVerticalSize;
        }

        // Center the camera on the playable area's center
        Vector3 colliderCenter = playableAreaCollider.bounds.center;
        cam.transform.position = new Vector3(colliderCenter.x, colliderCenter.y, cam.transform.position.z);

        
        float scaleWidth = 1.0f / scaleHeight;
        if (scaleHeight < 1.0f)
        {
            cam.rect = new Rect(0, (1.0f - scaleHeight) / 2.0f, 1.0f, scaleHeight);
        }
        else
        {
            cam.rect = new Rect((1.0f - scaleWidth) / 2.0f, 0, scaleWidth, 1.0f);
        }
    }

    private void Update()
    {
        // Handle dynamic window resizing by re-adjusting the camera
        // This ensures the aspect ratio is maintained in real-time
        AdjustCamera();
    }
}
