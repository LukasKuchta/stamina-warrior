using Domain.BattlePlans;
using Domain.Battles.Duels.Attributes;
using Domain.Battles.Duels.Rounds;
using Domain.Battles.Duels.Rules;
using Domain.Shared;
using Domain.Warriors;

namespace Domain.Battles.Duels.AttackExchanges;

public sealed class AttackExchange : EntityBase, IAgregationRoot
{
    public AttackExchangeId Id { get; }
    public DuelId DuelId { get; }
    public DuelRoundId DuelRoundId { get; }
    public WarriorId AttackerId { get; }
    public WarriorId OpponentId { get; private set; }
    private readonly HashSet<GameAttributeBase> _attributes = [];
    private readonly List<Slot> _slots = [];
    public AttackExchangeState State { get; private set; }

    private readonly IReadOnlyCollection<GameAttributeBase> _readonlyAttributes;
    private readonly IReadOnlyCollection<Slot> _readonlySlots;

    public AttackExchange(
        AttackExchangeId attackId,
        DuelId duelId,
        DuelRoundId duelRoundId,
        WarriorId attackerId,
        WarriorId opponentId)
    {
        Id = attackId;
        DuelId = duelId;
        DuelRoundId = duelRoundId;

        AttackerId = attackerId;
        OpponentId = opponentId;

        State = AttackExchangeState.Draft;

        _readonlySlots = _slots.AsReadOnly();
        _readonlyAttributes = _attributes.AsReadOnly();
    }

    public IReadOnlyCollection<GameAttributeBase> Attributes => _readonlyAttributes;
    public IReadOnlyCollection<Slot>  Slots => _readonlySlots;

    public void AddMagicCard(Slot slot)
    {
        CheckRule(new AllowOnlyDraftModifiationRule(State));

        _slots.Add(slot);
    }

    public void RemoveMagicCard(Slot slot)
    {
        CheckRule(new AllowOnlyDraftModifiationRule(State));

        _slots.Remove(slot);
    }

    public void AddAttribute(GameAttributeBase attribute)
    {
        CheckRule(new AllowOnlyDraftModifiationRule(State));

        _attributes.Add(attribute);
    }

    public void RemoveAttribute(GameAttributeBase attribute)
    {
        CheckRule(new AllowOnlyDraftModifiationRule(State));

        _attributes.Remove(attribute);
    }

    public void ChangeOpponent(WarriorId newOpponentId)
    {
        CheckRule(new AllowOnlyDraftModifiationRule(State));
        CheckRule(new AttackerAndOpponentCannotBeTheSameRule(AttackerId, newOpponentId));

        OpponentId = newOpponentId;
    }

    public void Ready()
    {
        CheckRule(new AllowOnlyTransitionDraftToReadyRule(State));

        State = AttackExchangeState.Ready;
    }

    public void Resolved()
    {
        CheckRule(new AllowOnlyTransitionReadyToResolvedRule(State));

        State = AttackExchangeState.Resolved;
    }

    public void Timeouted()
    {
        CheckRule(new AllowOnlyTransitionDraftToTimeoutRule(State));

        State = AttackExchangeState.Timeout;
    }

    public static AttackExchange Create(
        DuelId duelId,
        DuelRoundId duelRoundId,
        WarriorId attackerId,
        WarriorId opponentId)
    {
        CheckRule(new AttackerAndOpponentCannotBeTheSameRule(attackerId, opponentId));

        return new AttackExchange(AttackExchangeId.NewId(), duelId, duelRoundId, attackerId, opponentId);
    }
}
