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
    Console.WriteLine("Logged in as cashier.");
    var receipt = new Receipt();

    while (true)
    {
      Console.Write("Scan barcode (or 'pay' / 'return' / 'exit'): ");
      var input = _scanner.Scan();

      if (input == "exit")
        break;
      if (input == "pay")
      {
        receipt.Print();
        _receiptRepo.Add(receipt);
        receipt = new Receipt(); // new sale
        continue;
      }
      if (input == "return")
      {
        Console.WriteLine("Processing return... (stub)");
        continue;
      }

      var product = _productRepo.GetByBarcode(input);
      if (product == null)
        Console.WriteLine("Product not found.");
      else
      {
        receipt.AddProduct(product);
        Console.WriteLine($"Added {product.Name} - {product.Price:C}");
      }
    }
  }
}
