namespace Domain.MagicCards;

public sealed record DrawResult
{
    public static DrawResult None => new DrawResult();

    public MagicCardBase? Card { get; init; }

    private DrawResult()
    {
    }

    private DrawResult(MagicCardBase card)
    {
        Card = card;
    }

    public static DrawResult Create(MagicCardBase card)
    {        
        return new DrawResult(card);
    }
}
