namespace Domain.MagicCards;

public interface IMagicCardStrategyFactory
{
    IMagicCardStrategy SelectBy(MagicCardBase card);
}
