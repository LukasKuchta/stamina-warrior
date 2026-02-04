namespace Domain.MagicCards;

public sealed class MagicCardStrategyFactory : IMagicCardStrategyFactory
{
    private readonly Dictionary<Type, IMagicCardStrategy> _map;

    public MagicCardStrategyFactory(IEnumerable<IMagicCardStrategy> strategies) => _map = strategies.ToDictionary(s => s.CardType);

    public IMagicCardStrategy SelectBy(MagicCardBase card)
    {
        return _map[card.GetType()];
    }
}
