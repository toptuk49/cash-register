using CashRegister.Domain.Interfaces;

namespace CashRegister.Presentation.Services;

public class AdminService
{
  private readonly IProductRepository _productRepo;
  private readonly IReceiptRepository _receiptRepo;
  private readonly IBarcodeScannerService _scanner;
  private readonly ConsoleService _console;
  private readonly InputService _input;

  public AdminService(
      IProductRepository productRepo,
      IReceiptRepository receiptRepo,
      IBarcodeScannerService scanner,
      ConsoleService consoleService,
      InputService inputService
  )
  {
    _productRepo = productRepo;
    _receiptRepo = receiptRepo;
    _scanner = scanner;
    _console = consoleService;
    _input = inputService;
  }

  public void Run()
  {
    _console.Clear();
    _console.PrintHeader("=== РЕЖИМ АДМИНИСТРАТОРА ===");

    while (true)
    {
      _console.WriteLine("\nДоступные действия:");
      _console.WriteLine("1 - Сканировать товар");
      _console.WriteLine("2 - Просмотреть отчет");
      _console.WriteLine("3 - Экспортировать данные");
      _console.WriteLine("0 - Выход");
      _console.PrintSeparator();

      var choice = _input.GetChoice("Введите номер действия: ", new List<string> { "0", "1", "2", "3" });

      switch (choice)
      {
        case "1":
          ScanProduct();
          break;
        case "2":
          GenerateReport();
          break;
        case "3":
          ExportData();
          break;
        case "0":
          _console.PrintHeader("Выход из режима администратора");
          return;
      }

      if (choice != "0") // Не ждем клавишу при выходе
      {
        _console.WaitForAnyKey();
        _console.Clear();
      }
    }
  }

  private void ScanProduct()
  {
    _console.PrintHeader("Сканирование штрих-кода");

    _console.Write("Введите штрих-код товара: ");
    var barcode = _scanner.Scan();

    var product = _productRepo.GetByBarcode(barcode.Value);

    if (product != null)
    {
      _console.WriteLine($"\n✅ Найден товар:");
      _console.WriteLine($"   Штрих-код: {product.Barcode}");
      _console.WriteLine($"   Название: {product.Name}");
      _console.WriteLine($"   Цена: {product.Price:C}");
    }
    else
    {
      _console.WriteLine($"\n❌ Товар со штрих-кодом '{barcode}' не найден.");
    }
  }

  private void GenerateReport()
  {
    _console.PrintHeader("Формирование отчета");

    var allProducts = _productRepo.GetAll();
    var allReceipts = _receiptRepo.GetAll();

    _console.WriteLine($"📊 Общая статистика:");
    _console.WriteLine($"   Количество товаров: {allProducts.Count()}");
    _console.WriteLine($"   Количество чеков: {allReceipts.Count()}");
    _console.WriteLine($"   Общая выручка: {allReceipts.Sum(r => r.TotalAmount):C}");

    _console.WriteLine("\n📦 Список товаров:");
    foreach (var product in allProducts)
    {
      _console.WriteLine($"   {product.Barcode} - {product.Name} - {product.Price:C}");
    }
  }

  private void ExportData()
  {
    _console.PrintHeader("Экспорт данных в учетную систему");

    var confirm = _input.GetConfirmation("Вы уверены, что хотите экспортировать данные?");

    if (confirm)
    {
      // TODO: добавить логику экспорта
      _console.WriteLine("✅ Данные успешно экспортированы в учетную систему.");
    }
    else
    {
      _console.WriteLine("❌ Экспорт отменен.");
    }
  }
}
