using CashRegister.Domain.Entities;

namespace CashRegister.Application.Services;

public interface IAuthenticationService
{
  User? Login(string username);
}
