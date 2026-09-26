public delegate void AttackEventHandler();

public interface IAttackSubscribable
{
    // Events
    public event AttackEventHandler OnAttackPreparing;
    public event AttackEventHandler OnAttackFired;
}
