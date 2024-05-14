﻿using CashFlow.Communication.Requests;

namespace CashFlow.Application.UseCases.Expenses.Update;
public interface IUpdateExpenseUseCase
{
    Task Execute(RequestExpenseJson request, long id);
}
