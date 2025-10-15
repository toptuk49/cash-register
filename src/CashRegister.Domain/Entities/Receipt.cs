namespace CashRegister.Domain.Entities;

public class Receipt
{
  public Guid Id { get; } = Guid.NewGuid();
  public DateTime CreatedAt { get; } = DateTime.Now;
  public List<ReceiptItem> Items { get; } = new();
  public decimal TotalAmount => Items.Sum(i => i.TotalPrice);
  public bool IsReturn { get; init; } // Флаг для возвратов

  public Receipt(bool isReturn = false)
  {
    IsReturn = isReturn;
  }

  public void AddProduct(Product product, int quantity = 1)
  {
    if (product == null)
      throw new ArgumentNullException(nameof(product));

    if (quantity <= 0)
      throw new ArgumentException("Quantity must be greater than 0", nameof(quantity));

    var existingItem = Items.FirstOrDefault(i => i.Product.Barcode == product.Barcode);

    if (existingItem != null)
    {
      existingItem.IncreaseQuantity(quantity);
    }
    else
    {
      Items.Add(new ReceiptItem(product, quantity));
    }
  }

  public bool RemoveProduct(string barcode, int quantity = 1)
  {
    var item = Items.FirstOrDefault(i => i.Product.Barcode == barcode);
    if (item == null) return false;

    if (item.Quantity <= quantity)
    {
      Items.Remove(item);
    }
    else
    {
      item.DecreaseQuantity(quantity);
    }

    return true;
  }

  public ReceiptItem? GetItemByBarcode(string barcode)
  {
    return Items.FirstOrDefault(i => i.Product.Barcode == barcode);
  }

  public void Print()
  {
    Console.WriteLine($"\n{(IsReturn ? "ВОЗВРАТ" : "ЧЕК")} #{Id}");
    Console.WriteLine($"Дата: {CreatedAt:dd.MM.yyyy HH:mm:ss}");
    Console.WriteLine(new string('-', 40));

    foreach (var item in Items)
    {
      Console.WriteLine($"{item.Product.Name}");
      Console.WriteLine($"  {item.Product.Price:C} x {item.Quantity} = {item.TotalPrice:C}");
    }

    Console.WriteLine(new string('-', 40));
    Console.WriteLine($"ИТОГО: {TotalAmount:C}");
    Console.WriteLine();
  }

  public Receipt CreateCopy(bool asReturn = false)
  {
    var copy = new Receipt(asReturn);
    foreach (var item in Items)
    {
      copy.AddProduct(item.Product, item.Quantity);
    }
    return copy;
  }
}
