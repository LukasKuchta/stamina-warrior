using Domain.Battles.Spheres;
using Domain.MagicCards;
using Domain.Shared;
using Domain.Warriors.Events;
using Domain.Warriors.Rules;

namespace Domain.Warriors;

public sealed class Warrior : EntityBase, IAgregationRoot
{
    private Warrior(WarriorId id, string name, SphereBase currentSphere, Level level, DeckOfCards deckOfCards)
    {
        Id = id;
        Name = name;
        CurrentSphere = currentSphere;
        Level = level;
        Health = 100 * Level.Value;
        DeckOfCards = deckOfCards;
        Course = Power.Zero;
        Strength = Health * CurrentSphere.HitRatio;
    }

    public WarriorId Id { get; }

    public float Strength { get; }

    private DeckOfCards DeckOfCards { get; }

    public string Name { get; }

    public SphereBase CurrentSphere { get; }

    public Level Level { get; }

    public int Health { get; private set; }

    public Power Course { get; private set; }

    public int MaxDamage => (int)(Strength * (BoostedDamage == null ? 1 : BoostedDamage.Value));

    private Power? BoostedDamage { get; set; }

    public bool IsDeckOfCardsEmpty => DeckOfCards.NotEmpty;

    internal int DeckMaxIndexInclusive => DeckOfCards.MaxIndexOfCard;

    public static Warrior Create(
        WarriorId id,
        string name,
        SphereBase currentSphere,
        Level level,
        IEnumerable<MagicCardBase> cards)
    {
        CheckRule(new WarrirNameLengthRule(name));

        return new Warrior(id, name, currentSphere, level, DeckOfCards.FromList(cards));
    }

    internal void StealCard(int cardIndex, Warrior oponent)
    {
        DrawResult drawResult = oponent.TryToDrawCard(cardIndex);

        if (drawResult.Card is { } card)
        {
            DeckOfCards.Add(card);

            AddDomainEvent(new CardStolen(card.Name));
        }
    }

    internal DrawResult TryToDrawCard(int cardIndex)
    {
        return DeckOfCards.DrawACard(cardIndex);
    }

    internal void CourseTarget(Power power, Warrior target)
    {
        target.Course = power;
    }

    internal void Heal(Power healPower)
    {
        if (Health <= 0)
        {
            int ressurectedFrom = Health;
            Health = 0;
            AddDomainEvent(new Resurrected(ressurectedFrom, Health));
        }

        int health = Health;
        Health = (int)((Health == 0 ? 1 : Health) * healPower.Value);
        AddDomainEvent(new WarriorHealed(health, Health));
    }

    internal void BoostDamage(Power power)
    {
        AddDomainEvent(new DamageBoosted(MaxDamage, power.Value));

        BoostedDamage = power;
    }

    internal void Hit(int damage, Warrior oponent)
    {
        oponent.Health -= damage;
        BoostedDamage = null;
    }

    internal void SelfHit(int damage)
    {
        Health -= damage;
        BoostedDamage = null;
    }

    internal void CourseBites()
    {
        // dont reset the curse after applying
        Health -= (int)(Health * Course.Value);
    }
}
