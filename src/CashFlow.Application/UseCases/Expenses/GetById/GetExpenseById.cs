using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.GetById;

public class GetExpenseById : IGetExpenseById
{
    private readonly IExpensesReadOnlyRepositories _repository;
    private readonly IMapper _mapper;
    public GetExpenseById(IMapper mapper, IExpensesReadOnlyRepositories repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    public async Task<ResponseExpensesJson> Execute(long id)
    {
        var entity = await _repository.GetById(id);

        if(entity is null)
        {
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUNT);
        }

        return _mapper.Map<ResponseExpensesJson>(entity);
    }
}
