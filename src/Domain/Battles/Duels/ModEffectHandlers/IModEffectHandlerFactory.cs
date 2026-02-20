using System.Diagnostics.CodeAnalysis;
using Domain.Battles.Duels.Effects;

namespace Domain.Battles.Duels.ModEffectHandlers;

internal interface IModEffectHandlerFactory
{
    bool TrySelectBy(EffectBase effect, [NotNullWhen(true)] out IModEffectHandler? handler);
}


