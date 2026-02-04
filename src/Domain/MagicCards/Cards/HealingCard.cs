using Domain.Battles.Rules;

namespace Domain.MagicCards.Cards;

public sealed record HealingCard : MagicCardBase
{
    public HealingCard(Chance activationChance, Power power) : base("Card of healing", activationChance)
    {
        Power = power;
    }

    public Power Power { get; }

    public static HealingCard Create(Chance activationChance, Power power)
    {
        CheckRule(new ZeroPowerWiollKillYouRule(power));

        return new HealingCard(activationChance, power);
    }
}
