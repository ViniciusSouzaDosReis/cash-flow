using CashFlow.Communication.Requests;

namespace CashFlow.Application.UseCases.Expenses.Update;
internal interface IUpdateExpenseUseCase
{
    Task Execute(RequestExpenseJson request, long id);
}
