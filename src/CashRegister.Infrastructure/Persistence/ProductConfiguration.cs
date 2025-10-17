using CashRegister.Domain.ValueObjects;

namespace CashRegister.Infrastructure.Persistence;

public class ProductConfiguration
{
  public List<ProductData> DefaultProducts { get; } = new()
    {
        new ProductData("4601234567890", "Молоко", 1.50m),
        new ProductData("4609876543210", "Хлеб", 1.00m),
        new ProductData("4612345678901", "Яйца", 2.20m),
        new ProductData("4623456789012", "Сахар", 0.80m),
        new ProductData("4634567890123", "Соль", 0.50m),
        new ProductData("4645678901234", "Масло", 3.00m),
        new ProductData("4656789012345", "Кофе", 4.50m),
        new ProductData("4667890123456", "Чай", 3.20m)    };
}
