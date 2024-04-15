using CashFlow.Communication.Requests;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseValidate : AbstractValidator<RequestRegisterExpenseJson>
{
    public RegisterExpenseValidate()
    {
        RuleFor(expense => expense.Title)
            .NotEmpty()
            .WithMessage("The title is required.");

        RuleFor(expense => expense.Amount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("The Amount must be greater than zero.");

        RuleFor(expense => expense.Date)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Expenses cannot be for the futere.");

        RuleFor(expense => expense.PaymentType)
            .IsInEnum()
            .WithMessage("Payment Type is not valid.");
    }
}
