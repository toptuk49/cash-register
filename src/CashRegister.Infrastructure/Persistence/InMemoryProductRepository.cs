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
    _products.Add(product);
  }

  public void AddProducts(IEnumerable<ProductData> datas)
  {
    var products = datas.Select(ProductFactory.Create);
    _products.AddRange(products);
  }

  public Product? GetByBarcode(string barcode) =>
    _products.FirstOrDefault(p => p.Barcode == barcode);

  public IEnumerable<Product> GetAll() => _products;
}
