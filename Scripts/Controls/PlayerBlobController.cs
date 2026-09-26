using UnityEngine;

[RequireComponent(typeof(PlayerController), typeof(BlobManager))]
public class PlayerBlobController : MonoBehaviour
{
    // References
    private PlayerController playerController;
    private BlobManager blobManager;
    
    #region Unity Callbacks
    
    private void Awake()
    {
        // Get References
        playerController = GetComponent<PlayerController>();
        blobManager = GetComponent<BlobManager>();
    }
    
    private void Update()
    {
        blobManager.MoveControlZone(playerController.PointerWorldPosition);
    }
    
    #endregion
}
