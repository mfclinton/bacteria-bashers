using UnityEngine;

public class DamageBlobSpawner : ADamageableSubscriber
{
    [Header("Settings")]
    [SerializeField] private float healthPerStoredBlob = 7f;
    [SerializeField] private AnimationCurve spawnRateHealthCurve = AnimationCurve.EaseInOut(0, .5f, 1, 0);
    
    // Events
    public delegate void BlobExtracted();
    public event BlobExtracted OnBlobExtracted;
    
    // State
    private int maxNumBlobsStored => Mathf.FloorToInt(damageableSubscribable.MaxHealth / healthPerStoredBlob);
    private int numBlobsExtracted;
    
    // References
    private BlobManager blobManager;

    protected override void Awake()
    {
        base.Awake();
        
        // References
        blobManager = FindAnyObjectByType<BlobManager>();
    }

    protected override void OnHealthChanged(IDamageableSubscribable subject, float delta)
    {
        float healthT = subject.CurrentHealth / subject.MaxHealth;
        float thresh = spawnRateHealthCurve.Evaluate(healthT);
        
        float rng = Random.value;
        if (rng < thresh)
            TrySpawnBlob();
    }
    
    private void TrySpawnBlob()
    {
        print(maxNumBlobsStored);
        if (numBlobsExtracted >= maxNumBlobsStored)
            return;
        
        print("Spawning Blob");
        blobManager.CreateBlob();
        numBlobsExtracted++;
        
        OnBlobExtracted?.Invoke();
    }
}
