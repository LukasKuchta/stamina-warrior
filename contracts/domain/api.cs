[assembly: System.Runtime.Versioning.TargetFramework(".NETCoreApp,Version=v10.0", FrameworkDisplayName=".NET 10.0")]
namespace Domain.Battles
{
    public sealed class BattleContext : Domain.Shared.ValueObjectBase, System.IEquatable<Domain.Battles.BattleContext>
    {
        public BattleContext(Domain.Warriors.Warrior Attacker, Domain.Warriors.Warrior Opponent, int RoundsCount) { }
        public Domain.Warriors.Warrior Attacker { get; init; }
        public Domain.Warriors.Warrior Opponent { get; init; }
        public int RoundsCount { get; init; }
    }
    public sealed class BattleResult : System.IEquatable<Domain.Battles.BattleResult>
    {
        public System.ReadOnlySpan<Domain.Battles.Events.IBattleEvent> BattleEvents { get; }
    }
    public interface IBattleEventVisitor
    {
        void Visit(Domain.Battles.Events.AttackLanded e);
        void Visit(Domain.Battles.Events.BattleFinished e);
        void Visit(Domain.Battles.Events.BattleFinishedTied e);
        void Visit(Domain.Battles.Events.BattleStarted e);
        void Visit(Domain.Battles.Events.CardDrawn e);
        void Visit(Domain.Battles.Events.DoubleKnockoutOccurred e);
        void Visit(Domain.Battles.Events.RoundStarted e);
        void Visit(Domain.Battles.Events.WarriorDied e);
    }
    public interface IBattleStrategy
    {
        System.Type SphereType { get; }
        Domain.Battles.BattleResult StartBattle(Domain.Battles.BattleContext battleContext);
    }
    public interface IBattleStrategyFactory
    {
        Domain.Battles.IBattleStrategy SelectBy(Domain.Battles.Spheres.SphereBase sphere);
    }
    public interface IBattleStrategy<T>
        where T : Domain.Battles.Spheres.SphereBase { }
    public interface IFightDecisionSource
    {
        int PickBaseDamage(int maxDamage);
        int PickCardIndex(int maxCardIndex);
        bool TryActivate(Domain.MagicCards.Chance activationChance);
    }
}
namespace Domain.Battles.Events
{
    public sealed class AttackLanded : Domain.Battles.Events.IBattleEvent, System.IEquatable<Domain.Battles.Events.AttackLanded>
    {
        public string AttackerName { get; }
        public int Damage { get; }
        public string OponentName { get; }
        public void Accept(Domain.Battles.IBattleEventVisitor visitor) { }
    }
    public sealed class BattleFinished : Domain.Battles.Events.IBattleEvent, System.IEquatable<Domain.Battles.Events.BattleFinished>
    {
        public Domain.Battles.Events.WarrirorStat Looser { get; }
        public Domain.Battles.Events.WarrirorStat Winner { get; }
        public void Accept(Domain.Battles.IBattleEventVisitor visitor) { }
    }
    public sealed class BattleFinishedTied : Domain.Battles.Events.IBattleEvent, System.IEquatable<Domain.Battles.Events.BattleFinishedTied>
    {
        public string Warrior1Name { get; }
        public string Warrior2Name { get; }
        public void Accept(Domain.Battles.IBattleEventVisitor visitor) { }
    }
    public sealed class BattleStarted : Domain.Battles.Events.IBattleEvent, System.IEquatable<Domain.Battles.Events.BattleStarted>
    {
        public Domain.Battles.Events.WarrirorStat Attacker { get; }
        public Domain.Battles.Events.WarrirorStat Oponent { get; }
        public void Accept(Domain.Battles.IBattleEventVisitor visitor) { }
    }
    public sealed class CardDrawn : Domain.Battles.Events.IBattleEvent, System.IEquatable<Domain.Battles.Events.CardDrawn>
    {
        public string CardHolder { get; }
        public string CardName { get; }
        public void Accept(Domain.Battles.IBattleEventVisitor visitor) { }
    }
    public sealed class DoubleKnockoutOccurred : Domain.Battles.Events.IBattleEvent, System.IEquatable<Domain.Battles.Events.DoubleKnockoutOccurred>
    {
        public Domain.Battles.Events.WarrirorStat Attacker { get; }
        public Domain.Battles.Events.WarrirorStat Oponent { get; }
        public void Accept(Domain.Battles.IBattleEventVisitor visitor) { }
    }
    public interface IBattleEvent
    {
        void Accept(Domain.Battles.IBattleEventVisitor visitor);
    }
    public sealed class RoundStarted : Domain.Battles.Events.IBattleEvent, System.IEquatable<Domain.Battles.Events.RoundStarted>
    {
        public int Round { get; }
        public void Accept(Domain.Battles.IBattleEventVisitor visitor) { }
    }
    public sealed class WarriorDied : Domain.Battles.Events.IBattleEvent, System.IEquatable<Domain.Battles.Events.WarriorDied>
    {
        public Domain.Battles.Events.WarrirorStat Dead { get; }
        public Domain.Battles.Events.WarrirorStat Survivor { get; }
        public void Accept(Domain.Battles.IBattleEventVisitor visitor) { }
    }
    public sealed class WarrirorStat : System.IEquatable<Domain.Battles.Events.WarrirorStat>
    {
        public int Health { get; }
        public int MaxDamage { get; }
        public string Name { get; }
        public override string ToString() { }
    }
}
namespace Domain.Battles.Spheres
{
    public sealed class BetweenworldSphere : Domain.Battles.Spheres.SphereBase, System.IEquatable<Domain.Battles.Spheres.BetweenworldSphere> { }
    public sealed class BlueSkysphere : Domain.Battles.Spheres.SphereBase, System.IEquatable<Domain.Battles.Spheres.BlueSkysphere> { }
    public sealed class DeepvaultSphere : Domain.Battles.Spheres.SphereBase, System.IEquatable<Domain.Battles.Spheres.DeepvaultSphere> { }
    public abstract class SphereBase : Domain.Shared.ValueObjectBase, System.IEquatable<Domain.Battles.Spheres.SphereBase>
    {
        public static readonly Domain.Battles.Spheres.SphereBase Betweenworld;
        public static readonly Domain.Battles.Spheres.SphereBase BlueSky;
        public static readonly Domain.Battles.Spheres.SphereBase Deepvault;
        protected SphereBase(string name, int difficulty, float hitRatio) { }
        public int Difficulty { get; }
        public float HitRatio { get; }
        public string Name { get; }
        public static System.Collections.Generic.IReadOnlyCollection<Domain.Battles.Spheres.SphereBase> All { get; }
    }
}
namespace Domain.Battles.Strategies
{
    public abstract class BattleStrategyBase<TSphereType> : Domain.Battles.IBattleStrategy, Domain.Battles.IBattleStrategy<TSphereType>
        where TSphereType : Domain.Battles.Spheres.SphereBase
    {
        protected BattleStrategyBase() { }
        public System.Type SphereType { get; }
        public abstract Domain.Battles.BattleResult StartBattle(Domain.Battles.BattleContext battleContext);
    }
    public sealed class BlueSkyBattleStrategy : Domain.Battles.Strategies.BattleStrategyBase<Domain.Battles.Spheres.BlueSkysphere>
    {
        public BlueSkyBattleStrategy(Domain.MagicCards.IMagicCardStrategyFactory magicCardStrategy, Domain.Battles.IFightDecisionSource decisionSource) { }
        public override Domain.Battles.BattleResult StartBattle(Domain.Battles.BattleContext battleContext) { }
    }
}
namespace Domain
{
    public static class DependencyInjection
    {
        public static Microsoft.Extensions.DependencyInjection.IServiceCollection AddDomainServices(this Microsoft.Extensions.DependencyInjection.IServiceCollection services) { }
    }
}
namespace Domain.MagicCards.Cards
{
    public sealed class CoursedCard : Domain.MagicCards.MagicCardBase, System.IEquatable<Domain.MagicCards.Cards.CoursedCard>
    {
        public CoursedCard(Domain.MagicCards.Chance chance, Domain.MagicCards.Power power) { }
        public Domain.MagicCards.Power Power { get; }
    }
    public sealed class FightingCard : Domain.MagicCards.MagicCardBase, System.IEquatable<Domain.MagicCards.Cards.FightingCard>
    {
        public FightingCard(Domain.MagicCards.Chance chance, Domain.MagicCards.Power power) { }
        public Domain.MagicCards.Power Power { get; }
    }
    public sealed class HealingCard : Domain.MagicCards.MagicCardBase, System.IEquatable<Domain.MagicCards.Cards.HealingCard>
    {
        public HealingCard(Domain.MagicCards.Chance activationChance, Domain.MagicCards.Power power) { }
        public Domain.MagicCards.Power Power { get; }
    }
    public sealed class PoisonCard : Domain.MagicCards.MagicCardBase, System.IEquatable<Domain.MagicCards.Cards.PoisonCard>
    {
        public PoisonCard(Domain.MagicCards.Chance activationChance) { }
        public Domain.MagicCards.Power Power { get; }
    }
    public sealed class StealingCard : Domain.MagicCards.MagicCardBase, System.IEquatable<Domain.MagicCards.Cards.StealingCard> { }
    public sealed class ThornDamageCard : Domain.MagicCards.MagicCardBase, System.IEquatable<Domain.MagicCards.Cards.ThornDamageCard>
    {
        public ThornDamageCard(Domain.MagicCards.Chance activationChance, Domain.MagicCards.Power power) { }
        public Domain.MagicCards.Power Power { get; }
    }
}
namespace Domain.MagicCards
{
    public sealed class Chance : Domain.Shared.ValueObjectBase, System.IEquatable<Domain.MagicCards.Chance>
    {
        public static readonly Domain.MagicCards.Chance Always;
        public static readonly Domain.MagicCards.Chance CoinFlip;
        public static readonly Domain.MagicCards.Chance Never;
        public float Value { get; }
        public bool IsAlways() { }
        public bool IsNone() { }
        public static Domain.MagicCards.Chance FromPercentage(float value) { }
    }
    public sealed class DrawResult : System.IEquatable<Domain.MagicCards.DrawResult>
    {
        public Domain.MagicCards.MagicCardBase? Card { get; init; }
        public static Domain.MagicCards.DrawResult None { get; }
        public static Domain.MagicCards.DrawResult Create(Domain.MagicCards.MagicCardBase card) { }
    }
    public interface IMagicCardStrategy
    {
        System.Type CardType { get; }
        void ApplyMagic(Domain.Warriors.Warrior cardHolder, Domain.Warriors.Warrior oponent, Domain.MagicCards.MagicCardBase card);
    }
    public interface IMagicCardStrategyFactory
    {
        Domain.MagicCards.IMagicCardStrategy SelectBy(Domain.MagicCards.MagicCardBase card);
    }
    public interface IMagicCardStrategy<in TCard>
        where in TCard : Domain.MagicCards.MagicCardBase
    {
        void ApplyMagic(Domain.Warriors.Warrior cardHolder, Domain.Warriors.Warrior oponent, TCard card);
    }
    public abstract class MagicCardBase : Domain.Shared.ValueObjectBase, System.IEquatable<Domain.MagicCards.MagicCardBase>
    {
        protected MagicCardBase(string name, Domain.MagicCards.Chance activationChance) { }
        public Domain.MagicCards.Chance ActivationChance { get; }
        public string Name { get; }
    }
    public abstract class MagicCardStrategyBase<TCard> : Domain.MagicCards.IMagicCardStrategy, Domain.MagicCards.IMagicCardStrategy<TCard>
        where TCard : Domain.MagicCards.MagicCardBase
    {
        protected MagicCardStrategyBase() { }
        public System.Type CardType { get; }
        public void ApplyMagic(Domain.Warriors.Warrior cardHolder, Domain.Warriors.Warrior oponent, Domain.MagicCards.MagicCardBase card) { }
        public abstract void ApplyMagic(Domain.Warriors.Warrior cardHolder, Domain.Warriors.Warrior oponent, TCard card);
    }
    public sealed class MagicCardStrategyFactory : Domain.MagicCards.IMagicCardStrategyFactory
    {
        public MagicCardStrategyFactory(System.Collections.Generic.IEnumerable<Domain.MagicCards.IMagicCardStrategy> strategies) { }
        public Domain.MagicCards.IMagicCardStrategy SelectBy(Domain.MagicCards.MagicCardBase card) { }
    }
    public sealed class Power : Domain.Shared.ValueObjectBase, System.IEquatable<Domain.MagicCards.Power>
    {
        public static readonly Domain.MagicCards.Power Zero;
        public bool HasPower { get; }
        public bool NoPower { get; }
        public float Value { get; }
        public static Domain.MagicCards.Power FromValue(float value) { }
    }
}
namespace Domain.MagicCards.Strategies
{
    public sealed class FightingCardStrategy : Domain.MagicCards.MagicCardStrategyBase<Domain.MagicCards.Cards.FightingCard>
    {
        public FightingCardStrategy() { }
        public override void ApplyMagic(Domain.Warriors.Warrior cardHolder, Domain.Warriors.Warrior oponent, Domain.MagicCards.Cards.FightingCard card) { }
    }
    public sealed class HealingCardStrategy : Domain.MagicCards.MagicCardStrategyBase<Domain.MagicCards.Cards.HealingCard>
    {
        public HealingCardStrategy() { }
        public override void ApplyMagic(Domain.Warriors.Warrior cardHolder, Domain.Warriors.Warrior oponent, Domain.MagicCards.Cards.HealingCard card) { }
    }
    public sealed class StealingCardStrategy : Domain.MagicCards.MagicCardStrategyBase<Domain.MagicCards.Cards.StealingCard>
    {
        public StealingCardStrategy(Domain.RandomSources.IRandomSource randomSource) { }
        public override void ApplyMagic(Domain.Warriors.Warrior cardHolder, Domain.Warriors.Warrior oponent, Domain.MagicCards.Cards.StealingCard card) { }
    }
    public sealed class ThornDamageStrategy : Domain.MagicCards.MagicCardStrategyBase<Domain.MagicCards.Cards.ThornDamageCard>
    {
        public ThornDamageStrategy() { }
        public override void ApplyMagic(Domain.Warriors.Warrior cardHolder, Domain.Warriors.Warrior oponent, Domain.MagicCards.Cards.ThornDamageCard card) { }
    }
}
namespace Domain.RandomSources
{
    public interface IRandomSource
    {
        int NextIntInclusive(int maxInclusive);
        bool Succeeds(Domain.MagicCards.Chance chance);
    }
}
namespace Domain.Shared
{
    public sealed class BusinessRuleValidationException : System.Exception
    {
        public BusinessRuleValidationException(Domain.Shared.IBusinessRule brokenRule) { }
        public Domain.Shared.IBusinessRule BrokenRule { get; }
        public string Details { get; }
        public override string ToString() { }
    }
    public class DomainEventBase : System.IEquatable<Domain.Shared.DomainEventBase>
    {
        public DomainEventBase() { }
    }
    public abstract class EntityBase
    {
        protected EntityBase() { }
        public System.Collections.Generic.IReadOnlyList<Domain.Shared.DomainEventBase> DomainEvents { get; }
        public void ClearDomainEvents() { }
        protected static void CheckRule(Domain.Shared.IBusinessRule rule) { }
    }
    public interface IAgregationRoot { }
    public interface IBusinessRule
    {
        string Message { get; }
        bool IsBroken();
    }
    public abstract class ValueObjectBase : System.IEquatable<Domain.Shared.ValueObjectBase>
    {
        protected ValueObjectBase() { }
        protected static void CheckRule(Domain.Shared.IBusinessRule rule) { }
    }
}
namespace Domain.Warriors.Events
{
    public sealed class CardStolen : Domain.Shared.DomainEventBase, System.IEquatable<Domain.Warriors.Events.CardStolen>
    {
        public CardStolen(string CardName) { }
        public string CardName { get; init; }
    }
}
namespace Domain.Warriors
{
    public sealed class Level : Domain.Shared.ValueObjectBase, System.IEquatable<Domain.Warriors.Level>
    {
        public int Value { get; }
        public static Domain.Warriors.Level FromNumber(int value) { }
    }
    public sealed class Warrior : Domain.Shared.EntityBase, Domain.Shared.IAgregationRoot
    {
        public Domain.Battles.Spheres.SphereBase CurrentSphere { get; }
        public int Health { get; }
        public Domain.Warriors.WarriorId Id { get; }
        public bool IsDeckOfCardsEmpty { get; }
        public Domain.Warriors.Level Level { get; }
        public int MaxDamage { get; }
        public string Name { get; }
        public float Strength { get; }
        public static Domain.Warriors.Warrior Create(string name, Domain.Battles.Spheres.SphereBase currentSphere, Domain.Warriors.Level level, System.Collections.Generic.IEnumerable<Domain.MagicCards.MagicCardBase> cards) { }
        public static Domain.Warriors.Warrior Create(Domain.Warriors.WarriorId id, string name, Domain.Battles.Spheres.SphereBase currentSphere, Domain.Warriors.Level level, System.Collections.Generic.IEnumerable<Domain.MagicCards.MagicCardBase> cards) { }
    }
    public sealed class WarriorId : System.IEquatable<Domain.Warriors.WarriorId>
    {
        public System.Guid Value { get; }
        public static Domain.Warriors.WarriorId New() { }
    }
}