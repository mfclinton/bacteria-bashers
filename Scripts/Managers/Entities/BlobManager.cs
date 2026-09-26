using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlobManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Blob_Entity blobPrefab;
    
    [Header("References")]
    [SerializeField] private Transform blobParent;
    
    // State
    public BlobManagerState BlobManagerState { get; private set; }
    
    private void Awake()
    {
        // Initialize
        BlobControlZone blobControlZone = FindObjectOfType<BlobControlZone>();
        BlobAttackZone blobAttackZone = FindObjectOfType<BlobAttackZone>();
        BlobManagerState = new BlobManagerState(blobControlZone, blobAttackZone);
    }
    
    private void Update()
    {
        UpdateCentroid();
    }
    
    public void MoveControlZone(Vector2 target)
    {
        BlobManagerState.MoveControlZone(target);
    }
    
    public Blob_Entity CreateBlob()
    {
        // Randomize
        Vector2 randomPosition = BlobManagerState.BlobControlZone.GetFloatingPosition(Random.value);
        Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
        
        // Spawn
        Blob_Entity blob = Instantiate(blobPrefab, randomPosition, randomRotation, blobParent);
        blob.Initialize(BlobManagerState);
        
        return blob;
    }
    
    private void UpdateCentroid()
    {
        Vector2 sum = Vector2.zero;
        int count = 0;
        foreach (Transform child in blobParent)
        {
            sum += (Vector2)child.position;
            count++;
        }
        
        Vector2 centroid = sum / count;
        BlobManagerState.SetCentroid(centroid);
    }
}
