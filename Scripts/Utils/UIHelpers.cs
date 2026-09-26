using UnityEngine;

public static class UIHelpers
{
    public static void SetCanvasGroupEnabled(CanvasGroup canvasGroup, bool isEnabled)
    {
        canvasGroup.alpha = isEnabled ? 1 : 0;
        canvasGroup.interactable = isEnabled;
        canvasGroup.blocksRaycasts = isEnabled;
    }
}