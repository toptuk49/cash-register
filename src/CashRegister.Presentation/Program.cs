using CashRegister.Presentation.Models;
using CashRegister.Presentation.Services;

Console.WriteLine("=== Модуль кассы ===");
Console.Write("Войти как (кассир/администратор): ");
var role = Console.ReadLine()?.Trim().ToLower();

if (role == "кассир")
{
  var cashier = new CashierService();
  cashier.Run();
}
else if (role == "администратор")
{
  var admin = new AdminService();
  admin.Run();
}
else
{
  Console.WriteLine("Неизвестная роль. Выход из программы...");
}
