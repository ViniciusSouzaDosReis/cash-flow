using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;
public interface IExpensesWriteOnlyRepositories
{
    Task Add(Expense expense);
    Task<bool> Delete(long id);
}
