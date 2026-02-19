using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Domain.Battles.Spheres;
using Domain.MagicCards;
using Domain.Warriors;

namespace Domain.Battles.Duels;

internal sealed class Test(IDuelHitCheck duelHitCheck,
    IStateEffectHandlerFactory stateHffectHandlerFactory,
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

            IDictionary<Type, ModBase> mods = new Dictionary<Type, ModBase>();

            foreach (var effect in attacker.Effects)
            {
                if (stateHffectHandlerFactory.TrySelectBy(effect, out var stateHandler))
                {
                    stateHandler.Apply(attacker, opponent, effect);
                }

                if (modEffectHandlerFactory.TrySelectBy(effect, out var modHandler))
                {
                    modHandler.Apply(mods, attacker, opponent, effect);
                }
            }

            if (duelHitCheck.Attempt(attacker))
            {
                opponent.Hit(attacker.Damage());
            }
        }
    }
}

public interface IModEffectHandler
{
    Type EffectType { get; }

    void Apply(IDictionary<Type, ModBase> mods, DuelWarriorState self, DuelWarriorState opponent, EffectBase effect);
}

public interface IModEffectHandler<in TModEffect> where TModEffect : EffectBase
{   
    void Apply(IDictionary<Type, ModBase> mods, DuelWarriorState self, DuelWarriorState opponent, EffectBase effect);
}

public abstract class ModEffectHandlerBase<TModEffect> : IModEffectHandler<TModEffect>, IModEffectHandler
    where TModEffect : EffectBase
{
    public Type EffectType => typeof(TModEffect);

    public void Apply(IDictionary<Type, ModBase> mods, DuelWarriorState self, DuelWarriorState opponent, EffectBase effect) => Apply(mods, self, opponent, (TModEffect)effect);

    public abstract void Apply(IDictionary<Type, ModBase> mods, DuelWarriorState self, DuelWarriorState opponent, TModEffect effect);
}

public sealed class DamageEffectHandler : ModEffectHandlerBase<DamageEffect>
{
    public override void Apply(IDictionary<Type, ModBase> mods, DuelWarriorState self, DuelWarriorState opponent, DamageEffect effect)
    {
        mods.Add(typeof(DamageMod), new DamageMod());
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

    public void Apply(DuelWarriorState self, DuelWarriorState opponent, EffectBase effect)
    {
        Apply(self, opponent, (TEffect)effect);
    }

    public abstract void Apply(DuelWarriorState self, DuelWarriorState opponent, TEffect effect);
}



public record EffectBase;
 
