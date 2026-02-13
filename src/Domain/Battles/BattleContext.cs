using Domain.Battles.Rules;
using Domain.Shared;
using Domain.Warriors;

namespace Domain.Battles;

internal enum DeathState
{
    None,
    Single,
    Double
}

public sealed record BattleContext : ValueObjectBase
{
    private BattleContext(Warrior attacker, Warrior opponent, int roundsCount)
    {
        Attacker = attacker;
        Opponent = opponent;
        RoundsCount = roundsCount;
    }

    public Warrior Attacker { get; }
    public Warrior Opponent { get; }
    public int RoundsCount { get; }

    internal DeathState TryGetDeath(out (Warrior Dead, Warrior Survivor) outcome)
    {
        outcome = default;

        bool isAttackerDead = Attacker.Health <= 0;
        bool isOponentDead = Opponent.Health <= 0;

        if (!isAttackerDead && !isOponentDead)
        {
            return DeathState.None;
        }

        if (isAttackerDead && isOponentDead)
        {
            return DeathState.Double;
        }

        outcome = isAttackerDead
            ? (Attacker, Opponent)
            : (Opponent, Attacker);

        return DeathState.Single;
    }

    public static BattleContext Create(Warrior attacker, Warrior opponent, int roundsCount)
    {
        CheckRule(new RoundCannotBeLowerOrEqualZeroRule(roundsCount));
        CheckRule(new AttackerAndOpponentCannotBeTheSameRule(attacker, opponent));
        CheckRule(new AttackerAndOpponentSpehereCheckRule(attacker, opponent));

        return new BattleContext(attacker, opponent, roundsCount);
    }

}

public sealed class AttackerAndOpponentCannotBeTheSameRule : IBusinessRule
{
    private readonly Warrior _attacker;
    private readonly Warrior _opponent;

    internal AttackerAndOpponentCannotBeTheSameRule(Warrior attacker, Warrior opponent)
    {
        _attacker = attacker;
        _opponent = opponent;
    }

    public string Message => "Attacker and oponnet cannot be the same!";

    public bool IsBroken()
    {
        return _attacker.Equals(_opponent);
    }
}

public sealed class RoundCannotBeLowerOrEqualZeroRule : IBusinessRule
{
    private readonly int _roundsCount;
    internal RoundCannotBeLowerOrEqualZeroRule(int roundsCount)
    {
        _roundsCount = roundsCount;
    }

    public string Message => "Round count cannot be zeor or negative!";

    public bool IsBroken()
    {
        return _roundsCount <= 0;
    }
}
