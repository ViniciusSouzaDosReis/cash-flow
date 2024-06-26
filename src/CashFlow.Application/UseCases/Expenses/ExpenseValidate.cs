﻿using CashFlow.Communication.Requests;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses;

public class ExpenseValidate : AbstractValidator<RequestExpenseJson>
{
    public ExpenseValidate()
    {
        RuleFor(expense => expense.Title)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.TITLE_REQUIRED);

        RuleFor(expense => expense.Amount)
            .GreaterThan(0)
            .WithMessage(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO);

        RuleFor(expense => expense.Date)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage(ResourceErrorMessages.EXPENSES_CANNOT_BE_FOR_FUTURE);

        RuleFor(expense => expense.PaymentType)
            .IsInEnum()
            .WithMessage(ResourceErrorMessages.PAYMENT_TYPE_NOT_VALID);
    }
}
