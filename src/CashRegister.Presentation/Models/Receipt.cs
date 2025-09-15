namespace CashRegister.Presentation.Models;

public class Receipt
{
  public Guid Id { get; } = Guid.NewGuid();
  public List<Product> Products { get; } = new();
  public decimal Total => Products.Sum(p => p.Price);

  public void Print()
  {
    Console.WriteLine($"\nЧЕК #{Id}");
    foreach (var p in Products)
      Console.WriteLine($"{p.Name} - {p.Price:C}");
    Console.WriteLine($"СУММА: {Total:C}\n");
  }
}
