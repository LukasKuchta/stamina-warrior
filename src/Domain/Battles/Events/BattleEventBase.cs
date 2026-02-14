namespace Domain.Battles.Events;

public abstract record BattleEventBase : IBattleEvent
{
    public int Order { get; private set; }
    public void SetOrder(int order) => Order = order;
    public abstract void Accept(IBattleEventVisitor visitor);
}
