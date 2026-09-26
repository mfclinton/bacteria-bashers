using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BlobBrain), typeof(Rigidbody2D))]
public class Blob_Entity : AEntity
{
    // References
    private BlobBrain brain;
    public Rigidbody2D Rb { get; private set; }
    
    // State
    private BlobManagerState blobManagerState;

    #region Unity Callbacks

    private void Awake()
    {
        // Base
        base.Awake();
        
        // Get References
        brain = GetComponent<BlobBrain>();
        Rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        Execute();
        ClampPositionWithinPlayableArea();
    }

    #endregion

    #region Event Callbacks

    protected override void HandleDeath()
    {
        // Update Blob Count
        blobManagerState.RemoveBlob();
        
        // Destroy
        Destroy(gameObject, 0.1f); // TODO
    }

    #endregion
    
    #region Helpers
    
    public void Initialize(BlobManagerState blobManagerState)
    {
        // Initialize
        this.blobManagerState = blobManagerState;
        
        // Update Blob Count
        blobManagerState.AddBlob();
    }
    
    private void Execute()
    {
        if (blobManagerState != null)
            brain.Execute(blobManagerState, this);
    }

    private void ClampPositionWithinPlayableArea()
    {
        // Get Playable Area
        PlayableAreaManager pam = PlayableAreaManager.Instance;
        if (pam == null)
            return;

        // Check Bounds
        Vector3 pos = transform.position;
        Bounds bounds = pam.PlayableAreaCollider.bounds;
        if (bounds.Contains(pos))
            return;

        // Clamp Position
        pos.x = Mathf.Clamp(pos.x, bounds.min.x, bounds.max.x);
        pos.y = Mathf.Clamp(pos.y, bounds.min.y, bounds.max.y);

        // Set Position
        transform.position = pos;
    }

    #endregion
}
