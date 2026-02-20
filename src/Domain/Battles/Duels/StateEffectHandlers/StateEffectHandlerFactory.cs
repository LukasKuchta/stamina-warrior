using System.Diagnostics.CodeAnalysis;
using Domain.Battles.Duels.Effects;

namespace Domain.Battles.Duels.StateEffectHandlers;

public sealed class StateEffectHandlerFactory : IStateEffectHandlerFactory
{
    private readonly Dictionary<Type, IStateEffectHandler> _map;

    public StateEffectHandlerFactory(IEnumerable<IStateEffectHandler> strategies) => _map = strategies.ToDictionary(s => s.EffectType);

    public bool TrySelectBy(EffectBase effect, [NotNullWhen(true)] out IStateEffectHandler? handler)
    {
        return _map.TryGetValue(effect.GetType(), out handler);
    }
}
