using UnityEngine;

[RequireComponent(typeof(EnemyBrain), typeof(Rigidbody2D))]
public class Enemy_Entity : AEntity
{
    // References
    private EnemyBrain brain;
    public Rigidbody2D Rb { get; private set; }
    
    // State
    private BlobManagerState blobManagerState;
    private EnemyManagerState enemyManagerState;
    
    #region Unity Callbacks
    
    private void Update()
    {
        Execute();
    }
    
    // Setup
    private void Awake()
    {
        // Base
        base.Awake();
        
        // Get References
        brain = GetComponent<EnemyBrain>();
        Rb = GetComponent<Rigidbody2D>();
    }
    
    #endregion

    #region Event Callbacks

    protected override void HandleDeath()
    {
        // Update Enemy Count
        enemyManagerState.RemoveEnemy();
        
        // Destroy
        Destroy(gameObject, 0.1f); // TODO
    }

    #endregion
    
    #region Helpers

    public void Initialize(BlobManagerState blobManagerState, EnemyManagerState enemyManagerState)
    {
        // Initialize
        this.blobManagerState = blobManagerState;
        this.enemyManagerState = enemyManagerState;
        
        // Update Enemy Count
        enemyManagerState.AddEnemy();
    }
    
    private void Execute()
    {
        if (blobManagerState != null)
            brain.Execute(blobManagerState, this);
    }

    #endregion
}
