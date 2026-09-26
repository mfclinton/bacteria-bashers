using FMODUnity;
using UnityEngine;

public class AudioBlobExtractedSubscriber : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter blobExtractedEmitter;
    
    // References
    private DamageBlobSpawner damageBlobSpawner;
    
    #region Unity Callbacks
    
    private void Awake()
    {
        // References
        damageBlobSpawner = GetComponent<DamageBlobSpawner>();
    }
    
    private void OnEnable()
    {
        // Events
        damageBlobSpawner.OnBlobExtracted += OnBlobExtracted;
    }
    
    private void OnDisable()
    {
        // Events
        damageBlobSpawner.OnBlobExtracted -= OnBlobExtracted;
    }
    
    #endregion
    
    #region Event Handlers

    private void OnBlobExtracted()
    {
        blobExtractedEmitter?.Play();
    }
    
    #endregion
}
