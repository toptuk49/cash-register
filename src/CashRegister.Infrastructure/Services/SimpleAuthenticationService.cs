using CashRegister.Application.Services;
using CashRegister.Domain.Entities;
using CashRegister.Domain.Interfaces;
using CashRegister.Infrastructure.Persistence;

namespace CashRegister.Infrastructure.Services;

public class SimpleAuthenticationService : IAuthenticationService
{
  private readonly IUserRepository _userRepo;

  public SimpleAuthenticationService(IUserRepository userRepo)
  {
    _userRepo = userRepo;
  }

  public User? Login(string username) => _userRepo.GetByUsername(username);
}
