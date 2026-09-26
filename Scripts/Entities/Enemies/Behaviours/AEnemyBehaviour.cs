using UnityEngine;

public abstract class AEnemyBehaviour : MonoBehaviour
{
    public abstract void Execute(BlobManagerState blobManagerState, Enemy_Entity enemy);
}