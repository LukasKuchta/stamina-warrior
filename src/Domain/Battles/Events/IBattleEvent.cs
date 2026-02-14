namespace Domain.Battles.Events;

public interface IBattleEvent
{
    void Accept(IBattleEventVisitor visitor);
    int Order { get; }
    void SetOrder(int order);
}
