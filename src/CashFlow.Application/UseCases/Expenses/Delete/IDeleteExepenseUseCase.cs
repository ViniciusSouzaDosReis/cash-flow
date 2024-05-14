namespace CashFlow.Application.UseCases.Expenses.Delete;
public interface IDeleteExepenseUseCase
{
    Task Execute(long id);
}
