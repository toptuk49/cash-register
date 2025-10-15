namespace CashRegister.Domain.Entities;

public class ReceiptItem
{
  public Product Product { get; }
  public int Quantity { get; private set; }
  public decimal TotalPrice => Product.Price * Quantity;

  public ReceiptItem(Product product, int quantity = 1)
  {
    Product = product ?? throw new ArgumentNullException(nameof(product));
    Quantity = quantity > 0 ? quantity : 1;
  }

  public void IncreaseQuantity(int amount = 1)
  {
    if (amount > 0)
      Quantity += amount;
  }

  public void DecreaseQuantity(int amount = 1)
  {
    if (amount > 0 && Quantity > amount)
      Quantity -= amount;
  }
}
