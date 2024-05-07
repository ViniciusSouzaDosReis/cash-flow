using CashFlow.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _dbContext;
    public UnitOfWork(DbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void Commit()
    {
        _dbContext.SaveChanges();
    }
}
