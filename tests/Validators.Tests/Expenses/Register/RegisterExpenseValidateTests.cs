using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;

namespace Validators.Tests.Expenses.Register;

public class RegisterExpenseValidateTests
{
    [Fact]
    public void Success()
    {
        //Arrange
        var validate = new RegisterExpenseValidate();

        var request = new RequestRegisterExpenseJson()
        {
            Amount = 100,
            Date = DateTime.Now.AddDays(-1),
            Description = "Description",
            PaymentType = CashFlow.Communication.Enums.PaymentType.Cash,
            Title = "Title",
        };
        //Act
        var result = validate.Validate(request);

        //Assert
        Assert.True(result.IsValid);
    }
}
