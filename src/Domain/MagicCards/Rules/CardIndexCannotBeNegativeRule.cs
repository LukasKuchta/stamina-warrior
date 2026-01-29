
using Domain.Shared;

namespace Domain.MagicCards.Rules;
internal sealed class CardIndexCannotBeNegativeRule(int cardIndex) : IBusinessRule
{
    public string Message => "Index of lucky card cannto be negative!";

    public bool IsBroken()
    {
        return cardIndex < 0;
    }
}
