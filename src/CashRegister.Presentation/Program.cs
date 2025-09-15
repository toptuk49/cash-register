using CashRegister.Application.Services;
using CashRegister.Domain.Interfaces;
using CashRegister.Infrastructure.Persistence;
using CashRegister.Infrastructure.Services;
using CashRegister.Presentation.Services;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
  // Domain / Infrastructure
  .AddSingleton<IProductRepository, InMemoryProductRepository>()
  .AddSingleton<IReceiptRepository, InMemoryReceiptRepository>()
  .AddSingleton<IUserRepository, InMemoryUserRepository>()
  .AddSingleton<IBarcodeScannerService, FakeBarcodeScannerService>()
  .AddSingleton<IAuthenticationService, SimpleAuthenticationService>()
  // Presentation
  .AddSingleton<CashierService>()
  .AddSingleton<AdminService>()
  .BuildServiceProvider();

Console.WriteLine("=== Модуль кассы ===");
Console.Write("Авторизоваться как (кассир/администратор): ");
var role = Console.ReadLine()?.Trim().ToLower();

if (role == "кассир")
{
  var cashier = serviceProvider.GetRequiredService<CashierService>();
  cashier.Run();
}
else if (role == "администратор")
{
  var admin = serviceProvider.GetRequiredService<AdminService>();
  admin.Run();
}
else
{
  Console.WriteLine("Неизвестная роль. Модуль прекращает работу в целях безопасности.");
}
