using CashRegister.Domain.Entities;
using CashRegister.Infrastructure.Persistence;

namespace CashRegister.UnitTests.Infrastructure;

public class InMemoryUserRepositoryTests
{
    [Fact]
    public void GetByUsername_ExistingUser_ReturnsUser()
    {
        var repository = new InMemoryUserRepository();

        var user = repository.GetByUsername("кассир");

        Assert.NotNull(user);
        Assert.Equal("кассир", user.Username);
        Assert.Equal(Role.Cashier, user.Role);
    }

    [Fact]
    public void GetByUsername_ExistingAdmin_ReturnsUser()
    {
        var repository = new InMemoryUserRepository();

        var user = repository.GetByUsername("администратор");

        Assert.NotNull(user);
        Assert.Equal("администратор", user.Username);
        Assert.Equal(Role.Admin, user.Role);
    }

    [Fact]
    public void GetByUsername_CaseInsensitive_ReturnsUser()
    {
        var repository = new InMemoryUserRepository();

        var user = repository.GetByUsername("КАССИР");

        Assert.NotNull(user);
        Assert.Equal("кассир", user.Username);
    }

    [Fact]
    public void GetByUsername_NonExistentUser_ReturnsNull()
    {
        var repository = new InMemoryUserRepository();

        var user = repository.GetByUsername("несуществующий");

        Assert.Null(user);
    }
}
