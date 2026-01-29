using Domain.MagicCards.Cards;
using Domain.Warriors;

namespace Domain.MagicCards.Strategies;

public sealed class ThornDamageStrategy : MagicCardStrategyBase<ThornDamageCard>
{
    public override void ApplyMagic(Warrior cardHolder, Warrior oponent, ThornDamageCard card)
    {
        int damage = (int)(cardHolder.MaxDamage * card.Power.Value);
        cardHolder.Hit(damage, cardHolder);
    }
}
