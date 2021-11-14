namespace Joker.Domain.BusinessRule;

public interface IBusinessRule
{
    bool IsBroken();
    string Message { get; }
}