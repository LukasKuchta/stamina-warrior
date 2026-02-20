using System.Diagnostics.CodeAnalysis;
using Domain.MagicCards;

namespace Domain.Battles.Duels.MaggicCardHandlers;

internal interface IMagicCardHandlerFactoryV2
{
    bool TrySelectBy(MagicCardBase card, [NotNullWhen(true)] out IMagicCardHandlerV2? handler);
}
