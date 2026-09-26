

public class EnemyPushZone : TrackedComponentZone<Enemy_Entity>
{
    protected override void Awake()
    {
        base.Awake();
        layerName = LayerName.Layer.Enemy;
    }
    
    
}