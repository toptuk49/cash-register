using CashRegister.Domain.Entities;

namespace CashRegister.UnitTests.Domain.Entities;

public class UserTests
{
    [Fact]
    public void Constructor_ValidParameters_CreatesUser()
    {
        var username = "testuser";
        var role = Role.Admin;

        var user = new User(username, role);

        Assert.Equal(username, user.Username);
        Assert.Equal(role, user.Role);
    }
}
