namespace Domain.Shared;

public interface IBusinessRule
{
    string Message { get; }

    bool IsBroken();
}
