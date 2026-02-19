using System;

namespace Domain.Battles.Duels;

internal sealed record DamageMod : ModBase
{
    public readonly static DamageMod Default = new DamageMod();
}
