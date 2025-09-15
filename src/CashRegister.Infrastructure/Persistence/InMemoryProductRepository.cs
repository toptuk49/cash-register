using CashRegister.Domain.Entities;
using CashRegister.Domain.Interfaces;

namespace CashRegister.Infrastructure.Persistence;

public class InMemoryProductRepository : IProductRepository
{
  private readonly List<Product> _products = new()
  {
    new Product("123", "Молоко", 1.50m),
    new Product("456", "Хлеб", 1.00m),
    new Product("789", "Яйца", 2.20m),
  };

  public Product? GetByBarcode(string barcode) =>
    _products.FirstOrDefault(p => p.Barcode == barcode);

  public IEnumerable<Product> GetAll() => _products;
}
