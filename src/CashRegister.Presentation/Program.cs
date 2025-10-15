using CashRegister.Domain.Interfaces;
using CashRegister.Infrastructure.Persistence;
using CashRegister.Infrastructure.Services;
using CashRegister.Presentation.Services;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
  // Domain / Infrastructure
  .AddSingleton<IProductRepository, InMemoryProductRepository>()
  .AddSingleton<IReceiptRepository, InMemoryReceiptRepository>()
  .AddSingleton<IBarcodeScannerService, FakeBarcodeScannerService>()
  // Presentation
  .AddSingleton<CashierService>()
  .AddSingleton<AdminService>()
  .AddSingleton<ConsoleService>()
  .AddSingleton<InputService>()
  .BuildServiceProvider();

var console = serviceProvider.GetRequiredService<ConsoleService>();

console.Clear();
console.PrintHeader("Кассовый аппарат");

while (true)
{
  console.WriteLine("\nДоступные действия:");
  console.WriteLine("1 - Авторизоваться, как кассир");
  console.WriteLine("2 - Авторизоваться, как администратор");
  console.WriteLine("0 - Выход");
  console.PrintSeparator();

  var input = serviceProvider.GetRequiredService<InputService>();
  var choice = input.GetChoice("Введите номер действия: ", new List<string> { "0", "1", "2", "3" });

  switch (choice)
  {
    case "1":
      var cashier = serviceProvider.GetRequiredService<CashierService>();
      cashier.Run();
      break;
    case "2":
      var admin = serviceProvider.GetRequiredService<AdminService>();
      admin.Run();
      break;
    case "0":
      console.PrintHeader("Выход из программы");
      return;
  }

  if (choice != "0") // Не ждем клавишу при выходе
  {
    console.WaitForAnyKey();
    console.Clear();
  }
}

