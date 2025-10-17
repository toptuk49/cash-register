using CashRegister.Domain.Interfaces;
using CashRegister.Domain.ValueObjects;

namespace CashRegister.Infrastructure.Services;

public class Ean13BarcodeScannerService : IBarcodeScannerService
{
  public Barcode Scan()
  {
    while (true)
    {
      Console.Write("Введите EAN-13 штрих-код (13 цифр): ");
      var input = Console.ReadLine()?.Trim();

      if (Barcode.TryCreate(input, out var barcode))
      {
        return barcode!;
      }

      Console.WriteLine("Неправильный EAN-13 штрих-код. Должен состоять из 13 цифр с правильной контрольной суммой.");
      Console.WriteLine("Например: 4601234567890");
    }
  }

  public bool TryScan(out Barcode? barcode)
  {
    Console.Write("Введите EAN-13 штрих-код: ");
    var input = Console.ReadLine()?.Trim();
    return Barcode.TryCreate(input, out barcode);
  }
}
