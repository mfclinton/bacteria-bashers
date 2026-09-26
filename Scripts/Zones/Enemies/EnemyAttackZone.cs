using UnityEngine;

public class EnemyAttackZone : TrackedComponentZone<Blob_Entity>
{
    protected override void Awake()
    {
        base.Awake();
        layerName = LayerName.Layer.Blob;
    }
}
