using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.GetAll;
internal interface IGetAllExpensesUseCase
{
    Task<ResponseExpensesJson> Execute();
}
