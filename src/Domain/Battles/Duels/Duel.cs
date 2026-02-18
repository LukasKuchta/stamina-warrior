using System;
using System.Collections.Generic;
using System.Text;
using Domain.Shared;

namespace Domain.Battles.Duels;

public sealed class Duel : EntityBase, IAgregationRoot
{
    public DuelId Id { get; } 

    public int MaxRounds { get; }

    public Duel(DuelId id, int maxRounds)
    {
        Id = id;
        MaxRounds = maxRounds;        
    }

    public static Duel Create(int maxRounds)
    {
        return new Duel(DuelId.NewId(), maxRounds);
    }
}
