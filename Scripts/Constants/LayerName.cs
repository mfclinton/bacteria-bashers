using System;
using UnityEngine;

public static class LayerName
{
    // Layer Name Enums
    public enum Layer
    {
        Blob,
        Enemy,
    }

    #region Helpers

    public static string GetLayerName(Layer layer)
    {
        return layer.ToString();
    }

    public static int GetLayerIndex(Layer layer)
    {
        string layerName = GetLayerName(layer);
        
        int layerIndex = LayerMask.NameToLayer(layerName);
        if (layerIndex == -1)
            Debug.LogError($"Layer '{layerName}' not found. Please ensure it's defined in the Layer settings.");

        return layerIndex;
    }

    #endregion
}