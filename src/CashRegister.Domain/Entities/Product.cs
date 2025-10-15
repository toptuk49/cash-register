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

    if (string.IsNullOrWhiteSpace(barcode))
      throw new ArgumentException("Barcode cannot be empty", nameof(barcode));

    if (string.IsNullOrWhiteSpace(name))
      throw new ArgumentException("Name cannot be empty", nameof(name));

    if (price < 0)
      throw new ArgumentException("Price cannot be negative", nameof(price));
  }

  public override bool Equals(object? obj)
  {
    return obj is Product product && Barcode == product.Barcode;
  }

  public override int GetHashCode()
  {
    return Barcode.GetHashCode();
  }
}
