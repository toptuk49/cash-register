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
  .AddSingleton<IReportService, FakeReportService>()
  .AddSingleton<IExportService, FakeExportService>()
  // Presentation
  .AddSingleton<CashierService>()
  .AddSingleton<AdminService>()
  .BuildServiceProvider();

Console.WriteLine("=== CashRegister System ===");
Console.Write("Login as (cashier/admin): ");
var role = Console.ReadLine()?.Trim().ToLower();

if (role == "cashier")
{
  var cashier = serviceProvider.GetRequiredService<CashierService>();
  cashier.Run();
}
else if (role == "admin")
{
  var admin = serviceProvider.GetRequiredService<AdminService>();
  admin.Run();
}
else
{
  Console.WriteLine("Unknown role. Exiting...");
}
