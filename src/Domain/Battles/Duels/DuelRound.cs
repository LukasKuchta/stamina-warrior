using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Domain.Shared;
using Domain.Warriors;

namespace Domain.Battles.Duels;

public enum DuelRoundCloseReason
{
    NotClosedYet, AllReady, Timeout
}
public sealed class DuelRound : EntityBase, IAgregationRoot
{
    private DuelRound(
        DuelRoundId id,
        DuelId duelId,
        RoundNumber roundNumber,
        RoundParticipantsSnapshot participants,
        MaxRound maxRound,
        Deadline deadline)
    {
        Id = id;
        DuelId = duelId;
        RoundNumber = roundNumber;

        Participants = participants;

        MaxRound = maxRound;
        Deadline = deadline;

        RoundStatus = RoundStatus.Open();

        _readOnlyReadyWarriors = _readyWarriors.AsReadOnly();
    }
    public DuelRoundCloseReason CloseReason => RoundStatus.Reason;
    public DuelRoundState RoundState => RoundStatus.State;
    public RoundParticipantsSnapshot Participants { get; }
    private readonly HashSet<WarriorId> _readyWarriors = [];
    private readonly IReadOnlySet<WarriorId> _readOnlyReadyWarriors;
    public IReadOnlySet<WarriorId> ReadyWarriors => _readOnlyReadyWarriors;
    public RoundNumber RoundNumber { get; }
    public Deadline Deadline { get; }
    public DuelRoundId Id { get; }
    public DuelId DuelId { get; }
    private MaxRound MaxRound { get; }  
    private RoundStatus RoundStatus { get; set; }
    public bool IsClosed => RoundState == DuelRoundState.Closed;

    public void MarkAsReady(WarriorId warriorId)
    {
        CheckRule(new AllowOnlyModificationWhenRoundIsOpenRule(RoundState));
        CheckRule(new MustBelongToParticipantsRule(Participants, warriorId));

        _readyWarriors.Add(warriorId);
    }

    public void CloseByReadiness()
    {
        CheckRule(new AllowOnlyTransitFromOpenToCloseRule(RoundState));
        CheckRule(new CheckAllReadyRule(Participants, _readyWarriors));
        
        RoundStatus = RoundStatus.Close(DuelRoundCloseReason.AllReady);
    }

    public bool TryCloseDueToTimeout(DateTimeOffset now)
    {
        if (RoundState != DuelRoundState.Open)
        {
            return false;
        }

        if (!Deadline.IsExpired(now))
        {
            return false;
        }
        
        RoundStatus = RoundStatus.Close(DuelRoundCloseReason.Timeout);

        return true;
    }

    // add idempotency check later
    public DuelRound OpenNextRound(RoundParticipantsSnapshot participants, Deadline deadline)
    {
        CheckRule(new MustBeClosedRule(RoundState));

        var next = RoundNumber.Next();
        CheckRule(new CheckRoundNumberOutOfRangeRule(next, MaxRound));

        return Create(DuelId, next, participants, MaxRound, deadline);
    }

    public static DuelRound CreateFirst(DuelId duelId, RoundParticipantsSnapshot participants, MaxRound maxRound, Deadline deadline)
    {
        return Create(duelId, RoundNumber.Zero, participants, maxRound, deadline);
    }

    private static DuelRound Create(
        DuelId duelId,
        RoundNumber roundNumber,
        RoundParticipantsSnapshot participants,
        MaxRound maxRound,
        Deadline deadline)
    {
        return new DuelRound(DuelRoundId.NewId(), duelId, roundNumber, participants, maxRound, deadline);
    }
}
