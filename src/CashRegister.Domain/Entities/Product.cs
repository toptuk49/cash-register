namespace CashRegister.Domain.Entities;

public class Product
{
  public string Barcode { get; }
  public string Name { get; }
  public decimal Price { get; }

  public Product(string barcode, string name, decimal price)
  {
    Barcode = barcode;
    Name = name;
    Price = price;
  }
}
