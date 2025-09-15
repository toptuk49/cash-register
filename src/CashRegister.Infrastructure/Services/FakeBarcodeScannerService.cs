using CashRegister.Application.Services;

namespace CashRegister.Infrastructure.Services;

public class FakeBarcodeScannerService : IBarcodeScannerService
{
  public string Scan()
  {
    Console.Write("Enter barcode: ");
    return Console.ReadLine() ?? string.Empty;
  }
}
