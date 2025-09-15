using CashRegister.Domain.Entities;

namespace CashRegister.Domain.Interfaces;

public interface IUserRepository
{
  User? GetByUsername(string username);
}
