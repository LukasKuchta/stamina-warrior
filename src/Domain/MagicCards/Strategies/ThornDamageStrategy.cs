using Domain.MagicCards.Cards;
using Domain.Warriors;

namespace Domain.MagicCards.Strategies;

public sealed class ThornDamageStrategy : MagicCardStrategyBase<ThornCard>
{
    public override void ApplyMagic(Warrior cardHolder, Warrior oponent, ThornCard card)
    {
        int damage = (int)(cardHolder.MaxDamage * card.Power.Value);
        cardHolder.SelfHit(damage);
    }
}
