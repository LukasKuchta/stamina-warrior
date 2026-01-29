using System.ComponentModel;
using Domain.Battles.Rules;
using Domain.Shared;
using Domain.Warriors;

namespace Domain.Battles;

public sealed record BattleContext(
    Warrior Attacker,
    Warrior Opponent,
    int RoundsCount) : ValueObjectBase
{
    internal void CheckIfCompetitiorsAreWithinTheSameSphere()
    {
        ArgumentNullException.ThrowIfNull(Attacker);
        ArgumentNullException.ThrowIfNull(Opponent);

        CheckRule(new CheckIfCompetitiorsAreWithinTheSameSphereRule(this));
    }

    internal bool TryGetDeath(out (Warrior Dead, Warrior Survivor) outcome)
    {
        outcome = default;

        bool isAttackerDead = Attacker.Health <= 0;
        bool isOponentDead = Opponent.Health <= 0;

        if (!isAttackerDead && !isOponentDead)
        {
            return false;
        }

        if (isAttackerDead && isOponentDead)
        {
            throw new InvalidOperationException("Both warriors are dead (tie?)");
        }

        outcome = isAttackerDead
            ? (Attacker, Opponent)
            : (Opponent, Attacker);

        return true;
    }

    internal bool CheckDoubleKnockout()
    {
        return Attacker.Health <= 0 && Opponent.Health <= 0;
    }


}


