using UnityEngine;
using UnityEngine.Serialization;

public class BlobBrain : MonoBehaviour
{
    [Header("Behaviours")]
    [SerializeField] private ABlobBehaviour moveBlobBehaviour;
    [SerializeField] private ABlobBehaviour attackBlobBehaviour;
    
    // State
    public BlobMode CurrentMode;
    
    public void Execute(BlobManagerState blobManagerState, Blob_Entity blob)
    {
        UpdateMode(blobManagerState, blob);
        switch (CurrentMode)
        {
            case BlobMode.Moving:
                moveBlobBehaviour?.Execute(blobManagerState, blob);
                break;
            case BlobMode.Attacking:
                attackBlobBehaviour?.Execute(blobManagerState, blob);
                break;
        }
    }
    
    public void UpdateMode(BlobManagerState blobManagerState, Blob_Entity blob)
    {
        if (blobManagerState.BlobAttackZone.TrackedComponents.Count != 0)
        {
            CurrentMode = BlobMode.Attacking;
        }
        else
        {
            CurrentMode = BlobMode.Moving;
        }
    }
}
