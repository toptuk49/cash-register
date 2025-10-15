using CashRegister.Domain.Interfaces;
using CashRegister.Infrastructure.Persistence;

namespace CashRegister.Infrastructure.Services;

public class FakeBarcodeScannerService : IBarcodeScannerService
{
  public string Scan()
  {
    Console.Write("Введите идентификатор штрих-кода: ");
    return Console.ReadLine() ?? string.Empty;
  }
}
