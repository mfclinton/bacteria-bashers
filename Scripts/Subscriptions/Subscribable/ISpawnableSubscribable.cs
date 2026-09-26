public delegate void SpawnEventHandler();
    
public interface ISpawnableSubscribable
{
    // Events
    public event SpawnEventHandler OnSpawned;
    public event SpawnEventHandler OnDespawned;
}
