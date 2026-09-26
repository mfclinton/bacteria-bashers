using TMPro;
using UnityEngine;

[RequireComponent(typeof(ProgressManager))]
public class ProgressManagerVisual : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    
    // References
    private ProgressManager progressManager;
    
    #region Unity Callbacks
    
    private void Awake()
    {
        // References
        progressManager = GetComponent<ProgressManager>();
        
        // Initialize
        SetScoreText(0);
    }
    
    private void OnEnable()
    {
        // Events
        progressManager.OnScoreChanged += OnScoreChanged;
    }
    
    private void OnDisable()
    {
        // Events
        progressManager.OnScoreChanged -= OnScoreChanged;
    }
    
    #endregion
    
    #region Event Handlers
    
    private void OnScoreChanged(int score, int delta)
    {
        SetScoreText(score);
    }
    
    #endregion

    #region Helpers

    private void SetScoreText(int score)
    {
        scoreText.text = score.ToString();
    }

    #endregion
}
