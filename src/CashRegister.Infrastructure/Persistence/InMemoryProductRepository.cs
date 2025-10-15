using CashRegister.Domain.Entities;
using CashRegister.Domain.Interfaces;
using CashRegister.Domain.ValueObjects;

namespace CashRegister.Infrastructure.Persistence;

public class InMemoryProductRepository : IProductRepository
{
  private readonly List<Product> _products;

  public InMemoryProductRepository(ProductConfiguration? configuration = null)
  {
    configuration ??= new ProductConfiguration();
    _products = configuration.DefaultProducts.Select(ProductFactory.Create).ToList();
  }

  public void AddProduct(ProductData data)
  {
    var product = ProductFactory.Create(data);

    if (_products.Any(p => p.Barcode == data.Barcode))
    {
      throw new InvalidOperationException($"Товар со штрих-кодом '{data.Barcode}' уже существует.");
    }

    _products.Add(product);
  }

  public void AddProducts(IEnumerable<ProductData> datas)
  {
    foreach (var data in datas)
    {
      AddProduct(data);
    }
  }

  public Product? GetByBarcode(string barcode) =>
    _products.FirstOrDefault(p => p.Barcode == barcode);

  public IEnumerable<Product> GetAll() => _products;
}
