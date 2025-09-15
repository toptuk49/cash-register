using CashRegister.Application.Services;
using CashRegister.Domain.Entities;
using CashRegister.Domain.Interfaces;

namespace CashRegister.Presentation.Services;

public class AdminService
{
  private readonly IProductRepository _productRepo;
  private readonly IReceiptRepository _receiptRepo;
  private readonly IBarcodeScannerService _scanner;

  public AdminService(
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
    Console.WriteLine("Администратор ---");

    while (true)
    {
      Console.Write("Введите действие (сканировать / отчет / экспортировать / выход): ");
      var cmd = Console.ReadLine()?.Trim().ToLower();

      switch (cmd)
      {
        case "сканировать":
          Console.Write("Введите идентификатор штрихкода: ");
          var barcode = _scanner.Scan();
          var product = _productRepo.GetByBarcode(barcode);
          Console.WriteLine(
            product != null
              ? $"Найден продукт: {product.Name} - {product.Price:C}"
              : "Продукт не найден."
          );
          break;

        case "отчет":
          Console.WriteLine("Отчет сформирован.");
          break;

        case "экспортировать":
          Console.WriteLine("Данные экспортированы в учетную систему.");
          break;

        case "выход":
          return;

        default:
          Console.WriteLine("Неизвестное действие.");
          break;
      }
    }
  }
}
