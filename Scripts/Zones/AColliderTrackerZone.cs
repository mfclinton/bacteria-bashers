using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class TrackedComponentZone<T> : MonoBehaviour
{
    // Settings
    protected LayerName.Layer layerName;
    
    // State
    public List<T> TrackedComponents { get; private set; }

    protected virtual void Awake()
    {
        TrackedComponents = new List<T>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (LayerName.GetLayerIndex(layerName) == other.gameObject.layer)
        {
            T component = other.GetComponent<T>();
            if (component != null)
                TrackedComponents.Add(component);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (LayerName.GetLayerIndex(layerName) == other.gameObject.layer)
        {
            T component = other.GetComponent<T>();
            if (component != null)
                TrackedComponents.Remove(component);
        }
    }
}