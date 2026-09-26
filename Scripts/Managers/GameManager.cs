using System;
using UnityEngine;

public class GameManager : AStateMachineSubscriber
{
    [Header("Configuration")]
    [SerializeField] private int initialBlobCount = 8;
    
    // References
    private PlayerBlobController playerBlobController;
    private BlobManager blobManager;

    #region Unity Callbacks

    protected override void Awake()
    {
        base.Awake();
        
        // References
        playerBlobController = FindObjectOfType<PlayerBlobController>();
        blobManager = FindObjectOfType<BlobManager>();
    }

    private void Start()
    {
        // Initialize
        InitializeGame();
    }

    #endregion

    #region Event Callbacks

    protected override void OnStateTransition(GameState previousState, GameState newState)
    {
        // Enable Blob Controller
        bool blobControllerEnabled = newState == GameState.Playing;
        playerBlobController.enabled = blobControllerEnabled;
    }
    
    private void OnBlobCountChange(int newCount)
    {
        bool isGameOver = newCount == 0;
        if (isGameOver)
            gameStateMachine.Transition(GameState.GameOver);
    }

    #endregion
    
    private void InitializeGame()
    {
        // Spawn Blobs
        for (int i = 0; i < initialBlobCount; i++)
            blobManager.CreateBlob();
        
        // Events
        blobManager.BlobManagerState.OnBlobCountChange += OnBlobCountChange;
    }
}
