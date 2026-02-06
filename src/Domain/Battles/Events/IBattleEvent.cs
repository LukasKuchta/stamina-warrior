namespace Domain.Battles.Events;

public interface IBattleEvent
{
    void Accept(IBattleEventVisitor visitor);
}
