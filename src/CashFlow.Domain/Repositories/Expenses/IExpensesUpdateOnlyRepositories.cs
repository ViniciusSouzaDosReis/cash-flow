using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;
public interface IExpensesUpdateOnlyRepositories
{
    void Update(Expense expense);
    Task<Expense?> GetById(long id);
}
