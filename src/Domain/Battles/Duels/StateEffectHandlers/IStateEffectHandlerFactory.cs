using System.Diagnostics.CodeAnalysis;
using Domain.Battles.Duels.Effects;

namespace Domain.Battles.Duels.StateEffectHandlers;

public interface IStateEffectHandlerFactory
{
    bool TrySelectBy(EffectBase effect, [NotNullWhen(true)] out IStateEffectHandler? handler);
}
