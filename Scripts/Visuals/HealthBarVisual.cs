using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HealthBarVisual : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float maxHealthBarTTransitionTime = 1f;
    
    // References
    private SpriteRenderer spriteRenderer;

    // State
    private Vector3 localOffset;

    // Variables
    private MaterialPropertyBlock materialPropertyBlock;
    private Coroutine setHealthBarTCoroutine;
    
    private void Awake()
    {
        // References
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Initialize
        materialPropertyBlock = new MaterialPropertyBlock();
        localOffset = transform.localPosition;
        
        // Set Health to Full
        spriteRenderer.GetPropertyBlock(materialPropertyBlock);
        materialPropertyBlock.SetFloat(HealthBarShaderConstants.T, 1f);
        spriteRenderer.SetPropertyBlock(materialPropertyBlock);
    }
    
    private void Update()
    {
        SetPositionAndUpright();
    }

    public void SetHealthBarT(float t)
    {
        TriggerSetHealthBarTCoroutine(t);
    }

    #region Helpers

    private void SetPositionAndUpright()
    {
        // Position
        Vector3 parentPosition = transform.parent.position;
        Vector3 scaledOffset = Vector3.Scale(localOffset, transform.parent.lossyScale);
        transform.position = parentPosition + scaledOffset;
        
        // Rotation
        transform.rotation = Quaternion.identity;
    }
    
    private void TriggerSetHealthBarTCoroutine(float targetT)
    {
        if (setHealthBarTCoroutine != null)
            StopCoroutine(setHealthBarTCoroutine);
        
        setHealthBarTCoroutine = StartCoroutine(SetHealthBarTCoroutine(targetT));
    }
    
    private IEnumerator SetHealthBarTCoroutine(float targetT)
    {
        spriteRenderer.GetPropertyBlock(materialPropertyBlock);
        
        float startT = materialPropertyBlock.GetFloat(HealthBarShaderConstants.T);
        float duration = Mathf.Abs(targetT - startT) * maxHealthBarTTransitionTime;
        
        float timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float progress = timeElapsed / duration;
            
            float t = Mathf.Lerp(startT, targetT, progress);
            materialPropertyBlock.SetFloat(HealthBarShaderConstants.T, t);
            
            spriteRenderer.SetPropertyBlock(materialPropertyBlock);
            yield return null;
        }
    }

    #endregion
}