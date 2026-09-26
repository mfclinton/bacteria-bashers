using FMODUnity;
using UnityEngine;

[RequireComponent(typeof(BlobControlZone))]
public class AudioBlobControlZoneSubscriber : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter movementEmitter;
    
    // References
    private BlobControlZone blobControlZone;
    
    #region Unity Callbacks
    
    private void Awake()
    {
        // Get References
        blobControlZone = GetComponent<BlobControlZone>();
    }
    
    private void Update()
    {
        movementEmitter.SetParameter(MovementFMODKeyConstants.Volume, blobControlZone.AccumulatedTravelPercentage);
    }
    
    #endregion
}
