using System.Diagnostics.CodeAnalysis;
using Domain.Shared;
using Domain.Warriors;

namespace Domain.Battles.Duels;

public sealed class DuelRound : EntityBase, IAgregationRoot
{
    private DuelRound(DuelRoundId id, DuelId duelId, RoundNumber roundNumber, HashSet<WarriorId> participants)
    {
        Id = id;
        DuelId = duelId;
        RoundNumber = roundNumber;
        State = DuelRoundState.Open;

        _participants = participants;

        _readOnlyReadyWarriors = _readyWarriors.AsReadOnly();
        _readOnlyParticipants = _participants.AsReadOnly();
    }

    private readonly HashSet<WarriorId>  _participants;
    private readonly HashSet<WarriorId> _readyWarriors = [];

    private readonly IReadOnlySet<WarriorId> _readOnlyReadyWarriors;
    private readonly IReadOnlySet<WarriorId> _readOnlyParticipants;

    public IReadOnlySet<WarriorId> ReadyWarriors => _readOnlyReadyWarriors;
    public IReadOnlySet<WarriorId> Participants => _readOnlyParticipants;

    internal RoundNumber RoundNumber { get; }

    public DuelRoundId Id { get; }
    public DuelId DuelId { get; }

    public DuelRoundState State { get; private set; }

    public bool IsClosed => State == DuelRoundState.Closed;

    public void MarkAsReady(WarriorId warriorId)
    {
        CheckRule(new AllowOnlyModificationWhenRoundIsOpenRule(State));
        CheckRule(new MustBelongToParticipantsRule(Participants, warriorId));

        _readyWarriors.Add(warriorId);
    }

    public void Close()
    {
        CheckRule(new AllowOnlyTransitFromOpenToCLoseRule(State));

        State = DuelRoundState.Closed;
    }

    public bool AllReady => _readyWarriors.Count == _participants.Count;
    public bool IsParticipant(WarriorId id) => _participants.Contains(id);
    public bool IsReady(WarriorId id) => _readyWarriors.Contains(id);

    public bool TryOpenNewRound(WarriorId[] participants, [NotNullWhen(true)] out DuelRound? nextDuelRound)
    {
        if (IsClosed)
        {
            nextDuelRound = Create(DuelId, RoundNumber.Next(), participants);
            return true;
        }

        nextDuelRound = null;
        return false;
    }

    public static DuelRound Create(DuelId duelId, RoundNumber roundNumber, WarriorId[] participants)
    {
        var uniq = participants.ToHashSet();

        CheckRule(new MustHaveAtLeastTwoParticipantsRule(uniq.Count));

        return new DuelRound(DuelRoundId.NewId(), duelId, roundNumber, uniq);
    }
}
