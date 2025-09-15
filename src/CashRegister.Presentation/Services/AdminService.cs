using CashRegister.Application.Services;
using CashRegister.Domain.Entities;
using CashRegister.Domain.Interfaces;

namespace CashRegister.Presentation.Services;

public class AdminService
{
  private readonly IProductRepository _productRepo;
  private readonly IReceiptRepository _receiptRepo;
  private readonly IReportService _reportService;
  private readonly IExportService _exportService;
  private readonly IBarcodeScannerService _scanner;

  public AdminService(
    IProductRepository productRepo,
    IReceiptRepository receiptRepo,
    IReportService reportService,
    IExportService exportService,
    IBarcodeScannerService scanner
  )
  {
    _productRepo = productRepo;
    _receiptRepo = receiptRepo;
    _reportService = reportService;
    _exportService = exportService;
    _scanner = scanner;
  }

  public void Run()
  {
    Console.WriteLine("Logged in as admin.");

    while (true)
    {
      Console.Write("Command (scan/report/export/exit): ");
      var cmd = Console.ReadLine()?.Trim().ToLower();

      switch (cmd)
      {
        case "scan":
          Console.Write("Scan barcode: ");
          var barcode = _scanner.Scan();
          var product = _productRepo.GetByBarcode(barcode);
          Console.WriteLine(
            product != null
              ? $"Found product: {product.Name} - {product.Price:C}"
              : "Product not found."
          );
          break;

        case "report":
          var report = _reportService.GenerateSalesReport(_receiptRepo.GetAll());
          Console.WriteLine("=== Sales Report ===");
          Console.WriteLine(report);
          break;

        case "export":
          _exportService.ExportReceipts(_receiptRepo.GetAll());
          Console.WriteLine("Export completed.");
          break;

        case "exit":
          return;

        default:
          Console.WriteLine("Unknown command.");
          break;
      }
    }
  }
}
