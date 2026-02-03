namespace Domain.Shared;

public abstract record ValueObjectBase
{
    protected static void CheckRule(IBusinessRule rule)
    {        
        if (rule.IsBroken())
        {
            throw new BusinessRuleValidationException(rule);
        }
    }
}
