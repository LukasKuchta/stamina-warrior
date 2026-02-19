using System.Diagnostics.CodeAnalysis;

namespace Domain.Battles.Duels;

public interface IModEffectHandlerFactory
{
    bool TrySelectBy(EffectBase effect, [NotNullWhen(true)] out IModEffectHandler? handler);
}
