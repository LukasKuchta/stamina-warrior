using System;
using System.Collections.Generic;
using System.Text;
using Domain.Shared;
using Domain.Warriors;

namespace Domain.Battles.Duels;


public sealed record DuelId(Guid Value)
{
    public static DuelId NewId() => new(Guid.NewGuid());
}   

public sealed class Duel : EntityBase
{
    public DuelId Id { get; }

    public Duel(DuelId id)
    {
        Id = id;
    }
}


public class Attack 
{
    public DuelId DuelId { get; set; }
}
