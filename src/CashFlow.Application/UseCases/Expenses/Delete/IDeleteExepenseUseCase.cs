namespace CashFlow.Application.UseCases.Expenses.Delete;
internal interface IDeleteExepenseUseCase
{
    Task Execute(long id);
}
