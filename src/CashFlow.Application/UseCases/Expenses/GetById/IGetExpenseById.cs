using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.GetById;
internal interface IGetExpenseById
{
    Task<ResponseExpensesJson> Execute(long id);
}
