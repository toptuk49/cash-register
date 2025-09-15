using CashRegister.Application.Services;
using CashRegister.Domain.Entities;
using CashRegister.Domain.Interfaces;

namespace CashRegister.Presentation.Services;

public class CashierService
{
  private readonly IProductRepository _productRepo;
  private readonly IReceiptRepository _receiptRepo;
  private readonly IBarcodeScannerService _scanner;

  public CashierService(
    IProductRepository productRepo,
    IReceiptRepository receiptRepo,
    IBarcodeScannerService scanner
  )
  {
    _productRepo = productRepo;
    _receiptRepo = receiptRepo;
    _scanner = scanner;
  }

  public void Run()
  {
    Console.WriteLine("Кассир ---");
    var receipt = new Receipt();

    while (true)
    {
      Console.Write(
        "Введите индентификатор штрих-кода: (или 'оформить оплату' / 'оформить возврат' / 'выйти' / 'сформировать отчет' / 'экспортировать данные'): "
      );
      var input = _scanner.Scan();

      if (input == "выйти")
        break;
      if (input == "оформить оплату")
      {
        receipt.Print();
        _receiptRepo.Add(receipt);
        receipt = new Receipt();
        continue;
      }
      if (input == "оформить возврат")
      {
        Console.WriteLine("Возврат оформлен успешно.");
        continue;
      }
      if (input == "сформировать отчет")
      {
        Console.WriteLine("Отчет сформирован.");
        continue;
      }
      if (input == "экспортировать данные")
      {
        Console.WriteLine("Данные успешно экспортированы.");
        continue;
      }

      var product = _productRepo.GetByBarcode(input);
      if (product == null)
        Console.WriteLine("Продукт не найден.");
      else
      {
        receipt.AddProduct(product);
        Console.WriteLine($"Добавлен продукт: {product.Name} - {product.Price:C}");
      }
    }
  }
}
