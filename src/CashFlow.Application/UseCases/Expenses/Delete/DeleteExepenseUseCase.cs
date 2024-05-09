
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.Delete;

public class DeleteExepenseUseCase : IDeleteExepenseUseCase
{
    private readonly IExpensesWriteOnlyRepositories _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteExepenseUseCase(IExpensesWriteOnlyRepositories repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(long id)
    {
        var response = await _repository.Delete(id);

        if(response is false)
        {
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUNT);
        }

        await _unitOfWork.Commit();
    }
}
