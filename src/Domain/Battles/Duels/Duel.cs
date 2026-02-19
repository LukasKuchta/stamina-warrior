using System;
using System.Collections.Generic;
using System.Text;
using Domain.Shared;

namespace Domain.Battles.Duels;

public sealed class Duel : EntityBase, IAgregationRoot
{
    public DuelId Id { get; }

    public MaxRound MaxRound { get; }    

    public Duel(DuelId id, MaxRound maxRound)
    {
        Id = id;
        MaxRound = maxRound;
    }

    public static Duel Create(MaxRound maxRound)
    {
        return new Duel(DuelId.NewId(), maxRound);
    }


}
