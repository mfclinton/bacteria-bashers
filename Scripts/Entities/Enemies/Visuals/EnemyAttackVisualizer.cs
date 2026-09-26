using System.Collections;
using UnityEngine;

[RequireComponent(typeof(IAttackSubscribable))]
public class EnemyAttackVisualizer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer attackZone;

    [Header("Duration Settings")]
    [SerializeField] private float lerpDuration = 0.2f;
    [SerializeField] private float attackFiredVisualDuration = 0.2f;

    [Header("Color Settings")]
    [SerializeField] private Color idleColor = Color.yellow;
    [SerializeField] private Color attackPreparingColor = Color.magenta;
    [SerializeField] private Color attackFiredColor = Color.red;

    // Variables
    private Coroutine visualizeCoroutine;

    // References
    private IAttackSubscribable attackSubscribable;

    #region Unity Callbacks

    private void Awake()
    {
        // References
        attackSubscribable = GetComponent<IAttackSubscribable>();

        // Initialize
        attackZone.color = idleColor;
    }

    private void OnEnable()
    {
        // Subscribe
        attackSubscribable.OnAttackPreparing += OnAttackPreparing;
        attackSubscribable.OnAttackFired += OnAttackFired;
    }

    private void OnDisable()
    {
        // Unsubscribe
        attackSubscribable.OnAttackPreparing -= OnAttackPreparing;
        attackSubscribable.OnAttackFired -= OnAttackFired;
    }

    #endregion

    #region Event Handlers

    private void OnAttackPreparing()
    {
        VisualizePreparingAttack();
    }

    private void OnAttackFired()
    {
        VisualizeAttackFired();
    }

    #endregion

    #region Helpers

    private void VisualizePreparingAttack()
    {
        if (visualizeCoroutine != null)
            StopCoroutine(visualizeCoroutine);

        Color startColor = attackZone.color;
        visualizeCoroutine = StartCoroutine(LerpColor(startColor, attackPreparingColor, lerpDuration));
    }
    
    private void VisualizeAttackFired()
    {
        if (visualizeCoroutine != null)
            StopCoroutine(visualizeCoroutine);
        
        Color startColor = attackZone.color;
        visualizeCoroutine = StartCoroutine(AttackFiredVisualCoroutine(startColor, attackFiredColor, lerpDuration));
    }

    #endregion

    #region Coroutines
    
    private IEnumerator LerpColor(Color startColor, Color targetColor, float duration)
    {
        float timeElapsed = 0;
        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            attackZone.color = Color.Lerp(startColor, targetColor, timeElapsed / duration);
            yield return null;
        }
        
        attackZone.color = targetColor;
        visualizeCoroutine = null;
    }
    
    private IEnumerator AttackFiredVisualCoroutine(Color startColor, Color targetColor, float duration)
    {
        float lerpDuration = duration * 0.5f;
        
        // Lerp to AttackFired
        yield return LerpColor(startColor, targetColor, lerpDuration);
        
        // Wait in AttackFired
        float timeRemaining = attackFiredVisualDuration - duration;
        yield return new WaitForSeconds(timeRemaining);
        
        // Lerp to Idle
        yield return LerpColor(targetColor, idleColor, lerpDuration);
        visualizeCoroutine = null;
    }

    #endregion
}
