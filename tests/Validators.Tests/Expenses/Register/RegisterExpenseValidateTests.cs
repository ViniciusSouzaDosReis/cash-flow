using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using CommonTestUtilities.Requests;

namespace Validators.Tests.Expenses.Register;

public class RegisterExpenseValidateTests
{
    [Fact]
    public void Success()
    {
        //Arrange
        var validate = new RegisterExpenseValidate();

        var request = RequestRegisterExpenseJsonBuilder.Builder();
        //Act
        var result = validate.Validate(request);

        //Assert
        Assert.True(result.IsValid);
    }
}
