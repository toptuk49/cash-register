using CashRegister.Presentation.Models;

namespace CashRegister.Presentation.Services;

public class CashierService
{
  private readonly List<Product> _catalog = new()
  {
    new("111", "Молоко", 1.20m),
    new("222", "Хлеб", 0.80m),
    new("333", "Кофе", 4.50m),
  };

  public void Run()
  {
    Console.WriteLine("Авторизован как кассир.");
    var receipt = new Receipt();

    while (true)
    {
      Console.Write(
        "Сканировать штрих-код (название продукта) (или 'оплата' / 'возврат' / 'выход'): "
      );
      var input = Console.ReadLine();

      if (input == "выход")
        break;
      if (input == "оплата")
      {
        receipt.Print();
        receipt = new Receipt();
        continue;
      }
      if (input == "возврат")
      {
        Console.WriteLine("Оформляем возврат...");
        continue;
      }

      var product = _catalog.FirstOrDefault(p => p.Barcode == input);
      if (product is null)
        Console.WriteLine("Продукт не найден.");
      else
      {
        receipt.Products.Add(product);
        Console.WriteLine($"Добавлен продукт {product.Name} - {product.Price:C}");
      }
    }
  }
}
