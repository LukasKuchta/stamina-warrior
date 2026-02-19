using System.Diagnostics.CodeAnalysis;

namespace Domain.Battles.Duels;

public interface IStateEffectHandlerFactory
{
    bool TrySelectBy(EffectBase effect, [NotNullWhen(true)] out IStateEffectHandler? handler);
}
