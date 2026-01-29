using Domain.MagicCards.Cards;
using Domain.Warriors;

namespace Domain.MagicCards.Strategies;

internal sealed class CoursedCardStrategy : MagicCardStrategyBase<CoursedCard>
{
    public override void ApplyMagic(Warrior cardHolder, Warrior oponent, CoursedCard card)
    {
        oponent.CourseTarget(card.Power, oponent);
    }
}
