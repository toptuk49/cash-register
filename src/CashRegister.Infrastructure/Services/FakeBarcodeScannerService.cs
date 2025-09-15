using CashRegister.Application.Services;
using CashRegister.Infrastructure.Persistence;

namespace CashRegister.Infrastructure.Services;

public class FakeBarcodeScannerService : IBarcodeScannerService
{
  public string Scan()
  {
    Console.Write("Enter barcode: ");
    return Console.ReadLine() ?? string.Empty;
  }
}
