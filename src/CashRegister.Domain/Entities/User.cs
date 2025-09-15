namespace CashRegister.Domain.Entities;

public class User
{
  public string Username { get; }
  public Role Role { get; }

  public User(string username, Role role)
  {
    Username = username;
    Role = role;
  }
}
