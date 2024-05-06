using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.Register;
internal interface IRegisterExpenseUseCase
{
    ResponseRegisteredExpenseJson Execute(RequestRegisterExpenseJson request);
}
