using Domain.ActivationRules;
using Domain.Battles.Rules;

namespace Domain.MagicCards.Cards;

public sealed record HealingCard : MagicCardBase
{
    public HealingCard(Power power) : base("Card of healing")
    {
        Power = power;
    }

    public Power Power { get; }

    public static HealingCard Create(Power power)
    {
        CheckRule(new ZeroPowerWiollKillYouRule(power));

        return new HealingCard(power);
    }
}
