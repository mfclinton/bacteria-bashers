using UnityEngine;

public abstract class ABlobBehaviour : MonoBehaviour
{
    public abstract void Execute(BlobManagerState blobManagerState, Blob_Entity blob);
}
