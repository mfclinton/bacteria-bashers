using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    [Header("Behaviours")]
    [SerializeField] private AEnemyBehaviour moveEnemyBehaviour;
    [SerializeField] private AEnemyBehaviour attackEnemyBehaviour;

    public void Execute(BlobManagerState blobManagerState, Enemy_Entity enemy)
    {
        moveEnemyBehaviour?.Execute(blobManagerState, enemy);
        attackEnemyBehaviour?.Execute(blobManagerState, enemy);
    }
}
