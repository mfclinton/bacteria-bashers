using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomizeEnemy : MonoBehaviour
{
    [Header("Blob Settings")]
    [SerializeField] private int peakBlobCount = 50;
    [SerializeField] private AnimationCurve blobDifficultyCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    
    [Header("Score Settings")]
    [SerializeField] private int peakScore = 1000;
    [SerializeField] private AnimationCurve scoreDifficultyCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    
    [Header("Difficulty Curve Settings")]
    [SerializeField] private AnimationCurve difficultyCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    
    [Header("Visual Settings")]
    [SerializeField] private Vector2 scaleRange = new Vector2(0.5f, 1.5f);
    [SerializeField] private Gradient colorGradient;
    
    private void Start()
    {
        // Difficulties
        float difficulty = CalculateDifficulty();
        
        // AEntity
        var entity = GetComponent<AEntity>();
        entity.Configure(difficulty);
        
        // Enemy Move Behaviour
        var moveBehaviour = GetComponent<EnemyMoveBehaviour>();
        moveBehaviour.Configure(difficulty);
        
        // Enemy Attack Behaviour
        var attackBehaviour = GetComponent<EnemyAttackBehaviour>();
        attackBehaviour.Configure(difficulty);
        
        // Scale 
        float scale = Random.Range(scaleRange.x, scaleRange.y);
        transform.localScale = new Vector3(scale, scale, 1);
        
        // Color
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = colorGradient.Evaluate(difficulty);
    }
    
    private float CalculateDifficulty()
    {
        // BlobManager
        var blobManager = FindAnyObjectByType<BlobManager>();
        float blobT = Mathf.Clamp01(blobManager.BlobManagerState.BlobCount / peakBlobCount);
        float blobDifficulty = blobDifficultyCurve.Evaluate(blobT);
        
        // ProgressManager
        var progressManager = FindAnyObjectByType<ProgressManager>();
        float scoreT = Mathf.Clamp01(progressManager.Score / peakScore);
        float scoreDifficulty = scoreDifficultyCurve.Evaluate(scoreT);
        
        // Difficulty
        float difficultyT = (blobDifficulty + scoreDifficulty) / 2f;
        float difficulty = difficultyCurve.Evaluate(difficultyT);
        
        return difficulty;
    }
}
