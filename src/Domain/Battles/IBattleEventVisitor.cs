using Domain.Battles.Events;

namespace Domain.Battles;

public interface IBattleEventVisitor
{
    void Visit(RoundStarted e);
    void Visit(DoubleKnockoutOccurred e);
    void Visit(WarriorDied e);
    void Visit(BattleFinished e);
    void Visit(BattleFinishedTied e);
    void Visit(AttackLanded e);
    void Visit(CardDrawn e);
    void Visit(BattleStarted e);
    void Visit(RoundStatsCaptured e);
}
