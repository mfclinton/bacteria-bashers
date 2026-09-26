using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyManager : AStateMachineSubscriber
{
    [Header("Settings")]
    [SerializeField] private Enemy_Entity enemyPrefab;
    
    [Header("Spawn Settings")]
    [SerializeField] private int maxEnemies = 10;
    [SerializeField] private Vector2Int numberOfEnemiesToSpawnRange = new Vector2Int(1, 3);
    [SerializeField] private float timeBetweenSpawnWaves = 12f;
    
    [Header("References")]
    [SerializeField] private Transform enemyParent;
    [SerializeField] private BoxCollider2D enemySpawnArea;
    
    // State
    public EnemyManagerState EnemyManagerState { get; private set; }
    private Coroutine spawnCoroutine;
    
    // References
    private BlobManager blobManager;

    #region Unity Callbacks

    protected override void Awake()
    {
        base.Awake();

        // Get References
        blobManager = FindAnyObjectByType<BlobManager>();
        EnemyManagerState = new EnemyManagerState();
    }

    #endregion

    #region Event Callbacks

    protected override void OnStateTransition(GameState previousState, GameState newState)
    {
        // State
        bool isGameStart = previousState == GameState.MainMenu && newState == GameState.Playing;
        bool isGameOver = newState == GameState.GameOver;
        
        // Control Spawn Coroutine
        if (isGameStart)
            StartSpawnCoroutine();
        else if (isGameOver)
            TryStopSpawnCoroutine();
        
    }

    #endregion

    #region Spawn Coroutine

    private void StartSpawnCoroutine()
    {
        TryStopSpawnCoroutine();
        spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }
    
    private void TryStopSpawnCoroutine()
    {
        if (spawnCoroutine != null)
            StopCoroutine(spawnCoroutine);
        
        spawnCoroutine = null;
    }
    
    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            // Spawn Enemy
            if (EnemyManagerState.EnemyCount < maxEnemies)
            {
                int capacity = maxEnemies - EnemyManagerState.EnemyCount;
                int maxToSpawn = Mathf.Min(numberOfEnemiesToSpawnRange.y, capacity);
                int numberToSpawn = Random.Range(numberOfEnemiesToSpawnRange.x, maxToSpawn + 1);
                for (int i = 0; i < numberToSpawn; i++)
                    CreateEnemy();
            }
            
            yield return new WaitForSeconds(timeBetweenSpawnWaves);
        }
    }

    #endregion

    #region Helpers

    private Enemy_Entity CreateEnemy()
    {
        PlayableAreaManager pam = PlayableAreaManager.Instance;
        
        // Calculate Position
        Vector2 randomPosition = RandomHelpers.RandomPointInBox2D(enemySpawnArea);
        
        // Calculate Rotation
        Vector2 direction = ((Vector2)pam.transform.position - randomPosition).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion randomRotation = Quaternion.Euler(0, 0, targetAngle);
        
        // Spawn
        Enemy_Entity enemy = Instantiate(enemyPrefab, randomPosition, randomRotation, enemyParent);
        enemy.Initialize(blobManager.BlobManagerState, EnemyManagerState);
        
        return enemy;
    }

    #endregion
}
