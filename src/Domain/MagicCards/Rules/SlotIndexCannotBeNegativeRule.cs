
using Domain.Shared;

namespace Domain.MagicCards.Rules;

public sealed class SlotIndexCannotBeNegativeRule(int cardIndex) : IBusinessRule
{
    public string Message => "Index of lucky card cannto be negative!";

    public bool IsBroken()
    {
        return cardIndex < 0;
    }
}
