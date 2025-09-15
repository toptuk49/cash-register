namespace CashRegister.Domain.Entities;

public class Receipt
{
  public Guid Id { get; } = Guid.NewGuid();
  public List<Product> Products { get; } = new();
  public decimal Total => Products.Sum(p => p.Price);

  public void AddProduct(Product product) => Products.Add(product);

  public void Print()
  {
    Console.WriteLine($"\nReceipt #{Id}");
    foreach (var p in Products)
      Console.WriteLine($"{p.Name} - {p.Price:C}");
    Console.WriteLine($"TOTAL: {Total:C}\n");
  }
}
