using Domain.Battles.Spheres;
using Domain.Warriors;

namespace Domain.Battles.Duels;

internal sealed class Test(IHitCheck duelHitCheck,
    IMagicCardHandlerFactoryV2 magicCardHandlerFactoryV2,
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
            var self = participants[attack.AttackerId];
            var opponent = participants[attack.OpponentId];

            Modifiers mods = new Modifiers();

            // apply magic cards
            foreach (var slot in attack.Slots)
            {
                if (magicCardHandlerFactoryV2.TrySelectBy(slot.Card, out var cardHandler))
                {
                    cardHandler.Apply(self, opponent, slot.Card);
                }
            }

            // apply effects 
            foreach (var effect in self.Effects)
            {
                if (stateEffectHandlerFactory.TrySelectBy(effect, out var stateHandler))
                {
                    stateHandler.ApplyEffect(self, opponent, effect);
                }

                if (modEffectHandlerFactory.TrySelectBy(effect, out var modHandler))
                {
                    modHandler.Amplify(mods, self, opponent, effect);
                }
            }

            Accuracy accuracy = mods.Get(AccuracyMod.None).Apply(self.BaseAccuracy);
            _ = mods.Get(EvasionMod.None).Apply(self.BaseEvasion);

            if (duelHitCheck.Attempt(accuracy, opponent))
            {
                Damage damage = mods.Get(DamageMod.None).Apply(self.BaseDamage);

                opponent.Hit(damage);
            }
        }
    }
}
