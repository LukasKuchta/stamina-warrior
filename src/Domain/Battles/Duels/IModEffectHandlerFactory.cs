using System.Diagnostics.CodeAnalysis;

namespace Domain.Battles.Duels;

internal interface IModEffectHandlerFactory
{
    bool TrySelectBy(EffectBase effect, [NotNullWhen(true)] out IModEffectHandler? handler);
}


