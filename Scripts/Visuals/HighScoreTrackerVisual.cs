using TMPro;
using UnityEngine;

[RequireComponent(typeof(HighScoreTracker))]
public class HighScoreTrackerVisual : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI highScoreText;
    
    // References
    private HighScoreTracker highScoreTracker;
    
    #region Unity Callbacks
    
    private void Awake()
    {
        // References
        highScoreTracker = GetComponent<HighScoreTracker>();
        
        // Initialize
        InitializeHighScoreText();
    }
    
    private void OnEnable()
    {
        // Events
        highScoreTracker.OnHighScoreChanged += OnHighScoreChanged;
    }
    
    private void OnDisable()
    {
        // Events
        highScoreTracker.OnHighScoreChanged -= OnHighScoreChanged;
    }
    
    #endregion
    
    #region Event Handlers
    
    private void OnHighScoreChanged(int highScore)
    {
        SetHighScoreText(highScore);
    }
    
    #endregion
    
    #region Helpers
    
    private void InitializeHighScoreText()
    {
        string highScoreKey = PlayerPrefKey.HighScoreInt.Name;
        int currentHighScore = PlayerPrefs.GetInt(highScoreKey, 0);
        SetHighScoreText(currentHighScore);
    }
    
    private void SetHighScoreText(int highScore)
    {
        highScoreText.text = highScore.ToString();
    }
    
    #endregion
}
