using CashRegister.Domain.Entities;
using CashRegister.Domain.Interfaces;

namespace CashRegister.Presentation.Services;

public class CashierService
{
  private readonly IProductRepository _productRepo;
  private readonly IReceiptRepository _receiptRepo;
  private readonly IBarcodeScannerService _scanner;
  private readonly ConsoleService _console;
  private readonly InputService _input;

  public CashierService(
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
    _console.PrintHeader("=== РЕЖИМ КАССИРА ===");

    var receipt = new Receipt();

    while (true)
    {
      DisplayCurrentReceipt(receipt);

      _console.WriteLine("\nДоступные действия:");
      _console.WriteLine("1 - Сканировать штрих-код товара");
      _console.WriteLine("2 - Оформить продажу и сформировать чек");
      _console.WriteLine("3 - Оформить возврат товара");
      _console.WriteLine("4 - Сформировать отчет о продажах/возвратах");
      _console.WriteLine("0 - Выход");
      _console.PrintSeparator();

      var choice = _input.GetChoice("Введите номер действия: ", new List<string> { "0", "1", "2", "3", "4" });

      switch (choice)
      {
        case "1":
          ScanProductAndAddToReceipt(receipt);
          break;
        case "2":
          CompleteSaleAndGenerateReceipt(receipt);
          receipt = new Receipt(); // Новый чек после продажи
          break;
        case "3":
          ProcessReturn();
          break;
        case "4":
          GenerateSalesReport();
          break;
        case "0":
          if (receipt.Items.Any())
          {
            var confirm = _input.GetConfirmation("В чеке есть товары. Вы уверены, что хотите выйти?");
            if (!confirm) continue;
          }
          _console.PrintHeader("Выход из режима кассира");
          return;
      }

      if (choice != "0") // Не ждем клавишу при выходе
      {
        _console.WaitForAnyKey();
        _console.Clear();
      }
    }
  }

  private void DisplayCurrentReceipt(Receipt receipt)
  {
    _console.PrintHeader("Текущий чек");

    if (!receipt.Items.Any())
    {
      _console.WriteLine("Чек пуст");
      return;
    }

    foreach (var item in receipt.Items)
    {
      _console.WriteLine($"{item.Product.Name} - {item.Product.Price:C} x {item.Quantity} = {item.TotalPrice:C}");
    }

    _console.PrintSeparator();
    _console.WriteLine($"ИТОГО: {receipt.TotalAmount:C}");
  }

  private void ScanProductAndAddToReceipt(Receipt receipt)
  {
    _console.PrintHeader("Сканирование штрих-кода товара");

    _console.Write("Введите штрих-код товара: ");
    var barcode = _scanner.Scan();

    var product = _productRepo.GetByBarcode(barcode.Value);

    if (product == null)
    {
      _console.WriteLine($"❌ Товар со штрих-кодом '{barcode}' не найден.");
      return;
    }

    var existingItem = receipt.GetItemByBarcode(barcode);

    if (existingItem != null)
    {
      var addMore = _input.GetConfirmation($"Товар '{product.Name}' уже в чеке (количество: {existingItem.Quantity}). Добавить еще?");
      if (addMore)
      {
        var additionalQuantity = _input.GetInteger($"Введите дополнительное количество: ", 1, 100);
        receipt.AddProduct(product, additionalQuantity);
        _console.WriteLine($"✅ Добавлено {additionalQuantity} шт. товара: {product.Name}");
      }
    }
    else
    {
      var quantity = _input.GetInteger($"Введите количество для '{product.Name}': ", 1, 100);
      receipt.AddProduct(product, quantity);
      _console.WriteLine($"✅ Добавлен товар: {product.Name} - {product.Price:C} x {quantity}");
    }
  }
  private void CompleteSaleAndGenerateReceipt(Receipt receipt)
  {
    _console.PrintHeader("Оформление продажи");

    if (!receipt.Items.Any())
    {
      _console.WriteLine("❌ Невозможно оформить продажу: чек пуст.");
      return;
    }

    _console.WriteLine("=== ФИНАЛЬНЫЙ ЧЕК ===");
    DisplayCurrentReceipt(receipt);

    var confirm = _input.GetConfirmation("Подтвердить оформление продажи?");

    if (!confirm)
    {
      _console.WriteLine("❌ Продажа отменена.");
      return;
    }

    receipt.Print();
    _receiptRepo.Add(receipt);

    _console.WriteLine("✅ Продажа успешно оформлена!");
    _console.WriteLine($"💰 Сумма продажи: {receipt.TotalAmount:C}");
    _console.WriteLine($"📄 Номер чека: {receipt.Id}");

    _console.PrintSeparator();
    _console.WriteLine("🖨️ Чек отправлен на печать...");
  }

  private void ProcessReturn()
  {
    _console.PrintHeader("Оформление возврата товара");

    var allReceipts = _receiptRepo.GetAll().ToList();

    if (!allReceipts.Any())
    {
      _console.WriteLine("❌ Нет доступных чеков для возврата.");
      return;
    }

    _console.WriteLine("Последние чеки:");
    var recentReceipts = allReceipts.TakeLast(5).ToList();

    for (int i = 0; i < recentReceipts.Count; i++)
    {
      var r = recentReceipts[i];
      _console.WriteLine($"{i + 1}. Чек #{r.Id} - {r.CreatedAt:dd.MM.yyyy HH:mm} - {r.TotalAmount:C}");
    }

    _console.WriteLine("0. Ввести номер чека вручную");
    _console.PrintSeparator();

    var choice = _input.GetChoice("Выберите чек для возврата: ",
        new List<string> { "0", "1", "2", "3", "4", "5" });

    Receipt? selectedReceipt = null;

    if (choice == "0")
    {
      var receiptId = _input.GetRequiredString("Введите номер чека: ", "номер чека");
      selectedReceipt = allReceipts.FirstOrDefault(r => r.Id.ToString() == receiptId);
    }
    else
    {
      var index = int.Parse(choice) - 1;
      selectedReceipt = recentReceipts[index];
    }

    if (selectedReceipt == null)
    {
      _console.WriteLine("❌ Чек не найден.");
      return;
    }

    _console.WriteLine($"\nТовары в чеке #{selectedReceipt.Id}:");
    foreach (var item in selectedReceipt.Items)
    {
      _console.WriteLine($"- {item.Product.Name} ({item.Product.Barcode}) - {item.Product.Price:C} x {item.Quantity} = {item.TotalPrice:C}");
    }

    var productBarcode = _input.GetRequiredString("Введите штрих-код товара для возврата: ", "штрих-код");
    var returnItem = selectedReceipt.GetItemByBarcode(productBarcode);

    if (returnItem == null)
    {
      _console.WriteLine("❌ Товар с указанным штрих-кодом не найден в чеке.");
      return;
    }

    var returnQuantity = _input.GetInteger($"Введите количество для возврата (максимум {returnItem.Quantity}): ", 1, returnItem.Quantity);

    var confirm = _input.GetConfirmation($"Подтвердить возврат {returnQuantity} шт. товара '{returnItem.Product.Name}' на сумму {returnItem.Product.Price * returnQuantity:C}?");

    if (confirm)
    {
      var returnReceipt = new Receipt(isReturn: true);
      returnReceipt.AddProduct(returnItem.Product, returnQuantity);

      _receiptRepo.Add(returnReceipt);

      _console.WriteLine("✅ Возврат успешно оформлен!");
      _console.WriteLine($"💰 Сумма к возврату: {returnReceipt.TotalAmount:C}");
      _console.WriteLine($"📄 Номер чека возврата: {returnReceipt.Id}");

      returnReceipt.Print();
    }
    else
    {
      _console.WriteLine("❌ Возврат отменен.");
    }
  }

  private void GenerateSalesReport()
  {
    _console.PrintHeader("Отчет о продажах/возвратах");

    var allReceipts = _receiptRepo.GetAll().ToList();
    var sales = allReceipts.Where(r => !r.IsReturn).ToList();
    var returns = allReceipts.Where(r => r.IsReturn).ToList();

    _console.WriteLine("📊 Общая статистика:");
    _console.WriteLine($"   Всего продаж: {sales.Count}");
    _console.WriteLine($"   Всего возвратов: {returns.Count}");
    _console.WriteLine($"   Общая выручка: {sales.Sum(r => r.TotalAmount):C}");
    _console.WriteLine($"   Сумма возвратов: {returns.Sum(r => r.TotalAmount):C}");
    _console.WriteLine($"   Чистая выручка: {sales.Sum(r => r.TotalAmount) - returns.Sum(r => r.TotalAmount):C}");

    _console.WriteLine("\n📦 Статистика по товарам:");

    var allSaleItems = sales.SelectMany(r => r.Items).ToList();
    var productSales = allSaleItems
        .GroupBy(i => i.Product.Barcode)
        .Select(g => new
        {
          Product = g.First().Product,
          TotalSold = g.Sum(i => i.Quantity),
          TotalRevenue = g.Sum(i => i.TotalPrice)
        })
        .OrderByDescending(p => p.TotalRevenue);

    foreach (var productStat in productSales)
    {
      _console.WriteLine($"   {productStat.Product.Name}:");
      _console.WriteLine($"     Продано: {productStat.TotalSold} шт.");
      _console.WriteLine($"     Выручка: {productStat.TotalRevenue:C}");
    }

    _console.WriteLine("\n🕒 Последние операции:");
    var recentOperations = allReceipts
        .OrderByDescending(r => r.CreatedAt)
        .Take(5)
        .ToList();

    foreach (var op in recentOperations)
    {
      var type = op.IsReturn ? "ВОЗВРАТ" : "ПРОДАЖА";
      _console.WriteLine($"   {op.CreatedAt:dd.MM.yyyy HH:mm} - {type} - {op.TotalAmount:C}");
    }
  }
}
