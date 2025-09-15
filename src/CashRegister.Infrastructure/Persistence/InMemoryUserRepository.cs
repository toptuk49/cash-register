using CashRegister.Domain.Entities;
using CashRegister.Domain.Interfaces;

namespace CashRegister.Infrastructure.Persistence;

public class InMemoryUserRepository : IUserRepository
{
  private readonly List<User> _users = new()
  {
    new User("кассир", Role.Cashier),
    new User("администратор", Role.Admin),
  };

  public User? GetByUsername(string username) =>
    _users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
}
