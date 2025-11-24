using CashRegister.Domain.Entities;

namespace CashRegister.UnitTests.Domain.Entities;

public class RoleTests
{
    [Fact]
    public void Role_ContainsExpectedValues()
    {
        Assert.Equal(0, (int)Role.Cashier);
        Assert.Equal(1, (int)Role.Admin);
    }
}
