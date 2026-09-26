using System.Collections.Generic;
using UnityEngine;

public class BlobManagerState
{
    // References
    public BlobControlZone BlobControlZone { get; private set; }
    public BlobAttackZone BlobAttackZone { get; private set; }
    
    // Events
    public delegate void BlobCountChange(int newCount);
    public event BlobCountChange OnBlobCountChange;
    
    // State
    public int BlobCount { get; private set; }
    public Vector2 Centroid { get; private set; }
    
    public BlobManagerState(BlobControlZone blobControlZone, BlobAttackZone blobAttackZone)
    {
        BlobControlZone = blobControlZone;
        BlobAttackZone = blobAttackZone;
    }

    #region Control Zone Helpers

    public void MoveControlZone(Vector2 target)
    {
        BlobControlZone.Move(target);
    }

    #endregion

    #region Blob Count Helpers

    public void AddBlob()
    {
        BlobCount++;
        OnBlobCountChange?.Invoke(BlobCount);
    }
    
    public void RemoveBlob()
    {
        BlobCount--;
        OnBlobCountChange?.Invoke(BlobCount);
    }

    #endregion
    
    public void SetCentroid(Vector2 centroid)
    {
        Centroid = centroid;
    }
}
