using UnityEngine;

public class EnableSampledGameObject : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjectsPool;
    
    private void Awake()
    {
        // Disable
        foreach (var go in gameObjectsPool)
            go.SetActive(false);
        
        // Enable
        int randomIndex = Random.Range(0, gameObjectsPool.Length);
        gameObjectsPool[randomIndex].SetActive(true);
    }
}
