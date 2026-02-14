using System.Collections.Immutable;
using Domain.Battles.Events;
using Domain.Battles.Rules;
using Domain.Shared;

namespace Domain.Battles;

public sealed class BattleResult : EntityBase, IAgregationRoot
{
    public BattleResultId Id { get; }
    private readonly ImmutableArray<IBattleEvent> _battleEvents;

    private BattleResult(BattleResultId id, ImmutableArray<IBattleEvent> battleEvents)
    {
        Id = id;
        _battleEvents = battleEvents;
    }

    public ImmutableArray<IBattleEvent> BattleEvents => _battleEvents;

    public static BattleResult Create(ImmutableArray<IBattleEvent> battleEvents)
    {
        CheckRule(new BattleEventsCannotBeEmptyRule(battleEvents));

        return new BattleResult(new BattleResultId(Guid.NewGuid()), battleEvents);
    }
}

