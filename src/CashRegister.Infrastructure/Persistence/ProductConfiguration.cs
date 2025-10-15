using CashRegister.Domain.ValueObjects;

namespace CashRegister.Infrastructure.Persistence;

public class ProductConfiguration
{
  public List<ProductData> DefaultProducts { get; } = new()
    {
        new ProductData("123", "Молоко", 1.50m),
        new ProductData("456", "Хлеб", 1.00m),
        new ProductData("789", "Яйца", 2.20m),
        new ProductData("111", "Сахар", 0.80m),
        new ProductData("222", "Соль", 0.50m),
        new ProductData("333", "Масло", 3.00m)
    };
}
