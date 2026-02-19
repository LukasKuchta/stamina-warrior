using System.Diagnostics.CodeAnalysis;

namespace Domain.Battles.Duels;

internal sealed class ModEffectHandlerFactory : IModEffectHandlerFactory
{
    private readonly Dictionary<Type, IModEffectHandler> _map;

    public ModEffectHandlerFactory(IEnumerable<IModEffectHandler> strategies) => _map = strategies.ToDictionary(s => s.EffectType);

    public bool TrySelectBy(EffectBase effect, [NotNullWhen(true)] out IModEffectHandler? handler)
    {
        return _map.TryGetValue(effect.GetType(), out handler);
    }
}
