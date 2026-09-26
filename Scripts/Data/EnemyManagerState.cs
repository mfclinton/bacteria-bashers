using UnityEngine;

public class EnemyManagerState : MonoBehaviour
{
    // Events
    public delegate void EnemyCountChange(int newCount);
    public event EnemyCountChange OnEnemyCountChange;
    
    // State
    public int EnemyCount { get; private set; }
    
    #region Enemy Count Helpers
    
    public void AddEnemy()
    {
        EnemyCount++;
        OnEnemyCountChange?.Invoke(EnemyCount);
    }
    
    public void RemoveEnemy()
    {
        EnemyCount--;
        OnEnemyCountChange?.Invoke(EnemyCount);
    }
    
    #endregion
}
