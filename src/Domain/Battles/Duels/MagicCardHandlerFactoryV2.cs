using System.Diagnostics.CodeAnalysis;
using Domain.MagicCards;

namespace Domain.Battles.Duels;

internal class MagicCardHandlerFactoryV2 : IMagicCardHandlerFactoryV2
{
    private readonly IDictionary<Type, IMagicCardHandlerV2> _map;
    internal MagicCardHandlerFactoryV2(IEnumerable<IMagicCardHandlerV2> handlers)
    {
        _map = handlers.ToDictionary(s => s.CardType);
    }

    public bool TrySelectBy(MagicCardBase card, [NotNullWhen(true)] out IMagicCardHandlerV2? handler)
    {
        return _map.TryGetValue(card.GetType(), out handler);
    }
}
