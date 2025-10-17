using CashRegister.Domain.ValueObjects;

namespace CashRegister.Infrastructure.Persistence;

public class ProductConfiguration
{
  public List<ProductData> DefaultProducts { get; } = new()
    {
        new ProductData("4006381333931", "Молоко", 1.50m),
        new ProductData("4006381333948", "Хлеб", 1.00m),
        new ProductData("4006381333955", "Яйца", 2.20m),
        new ProductData("4006381333962", "Сахар", 0.80m),
        new ProductData("4006381333979", "Соль", 0.50m),
        new ProductData("4006381333986", "Масло", 3.00m),
        new ProductData("4006381333993", "Кофе", 4.50m),
        new ProductData("4006381334006", "Чай", 3.20m)
    };
}
