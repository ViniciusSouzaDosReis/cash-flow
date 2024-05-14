using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.GetById;
public interface IGetExpenseById
{
    Task<ResponseExpensesJson> Execute(long id);
}
