using Domain.Battles.Spheres;
using Domain.Warriors;

namespace Domain.Battles.Duels;

internal sealed class Test(IDuelHitCheck duelHitCheck,
    IStateEffectHandlerFactory stateEffectHandlerFactory,
    IModEffectHandlerFactory modEffectHandlerFactory)
{
    public void Orchestrate()
    {
        var conan = Warrior.Create(WarriorId.New(), "Conan", SphereBase.BlueSky, Level.FromValue(1), []);
        var brutus = Warrior.Create(WarriorId.New(), "Brutus", SphereBase.BlueSky, Level.FromValue(1), []);

        var duel = Duel.Create(MaxRound.FromValue(10));

        var participants0 = RoundParticipantsSnapshot.Create([conan.Id, brutus.Id]);
        var deadline0 = Deadline.FromStart(DateTimeOffset.Now, TimeSpan.FromSeconds(60));
        var round0 = DuelRound.CreateFirst(duel.Id, participants0, duel.MaxRound, deadline0);

        round0.MarkAsReady(conan.Id);
        round0.MarkAsReady(brutus.Id);

        round0.CloseByReadiness();

        var participants1 = RoundParticipantsSnapshot.Create([conan.Id, brutus.Id]);

        var deadline1 = Deadline.FromStart(DateTimeOffset.Now, TimeSpan.FromSeconds(60));
        var nextRound = round0.OpenNextRound(participants1, deadline1);
        _ = nextRound;
    }

    public void Evaluate(IEnumerable<AttackExchange> attacks, Dictionary<WarriorId, DuelWarriorState> participants)
    {
        foreach (var attack in attacks)
        {
            var attacker = participants[attack.AttackerId];
            var opponent = participants[attack.OpponentId];

            Modifiers mods = new Modifiers();

            // apply magic cards
            // <----
            foreach (var effect in attacker.Effects)
            {
                if (stateEffectHandlerFactory.TrySelectBy(effect, out var stateHandler))
                {
                    stateHandler.Handle(attacker, opponent, effect);
                }

                if (modEffectHandlerFactory.TrySelectBy(effect, out var modHandler))
                {
                    modHandler.Handle(mods, attacker, opponent, effect);
                }
            }

            _ = mods.Get(DamageMod.Default);


            if (duelHitCheck.Attempt(attacker))
            {
                opponent.Hit(attacker.Damage());
            }
        }
    }
}

internal sealed class DamageEffectHandler : ModEffectHandlerBase<DamageEffect>
{
    public override void Apply(Modifiers mods, DuelWarriorState self, DuelWarriorState opponent, DamageEffect effect)
    {
        // compute damage mod based on the effect and add it to mods
        mods.Add(new DamageMod());
    }
}

public sealed class HealthEffectHandler : StateEffectHandlerBase<HealthEffect>
{
    public override void Apply(DuelWarriorState self, DuelWarriorState opponent, HealthEffect effect)
    {
        self.Heal(effect.Value);
        //effect.IsConsumed = true;
    }
}

public abstract class StateEffectHandlerBase<TEffect> : IEffectHandler<TEffect>, IStateEffectHandler
    where TEffect : EffectBase
{
    public Type EffectType => typeof(TEffect);

    public void Handle(DuelWarriorState self, DuelWarriorState opponent, EffectBase effect)
    {
        Apply(self, opponent, (TEffect)effect);
    }

    public abstract void Apply(DuelWarriorState self, DuelWarriorState opponent, TEffect effect);
}



public record EffectBase;

