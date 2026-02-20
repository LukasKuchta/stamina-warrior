using System.Collections.Immutable;
using Domain.Battles.Duels.Rules;
using Domain.Shared;
using Domain.Warriors;

namespace Domain.Battles.Duels.Rounds;

public sealed record RoundParticipantsSnapshot : ValueObjectBase
{
    public ImmutableHashSet<WarriorId> Snapshot { get; }

    private RoundParticipantsSnapshot(ImmutableHashSet<WarriorId> participants)
    {
        Snapshot = participants;
    }

    public bool CompareMembers(IEnumerable<WarriorId> members)
    {
        ArgumentNullException.ThrowIfNull(members);

        return Snapshot.SetEquals(members);
    }

    public static RoundParticipantsSnapshot Create(IReadOnlyCollection<WarriorId> participants)
    {
        ArgumentNullException.ThrowIfNull(participants);        
        
        var uniqParticipants = participants.ToImmutableHashSet();        
        CheckRule(new MustHaveAtLeastTwoParticipantsRule(uniqParticipants.Count));

        return new RoundParticipantsSnapshot(uniqParticipants);
    }
}
