using CashRegister.Domain.Entities;
using CashRegister.Domain.ValueObjects;
using CashRegister.Infrastructure.Persistence;

namespace CashRegister.UnitTests.Infrastructure;

public class TestProductConfiguration : ProductConfiguration
{
    private readonly IEnumerable<ProductData> _testProducts;

    public TestProductConfiguration(IEnumerable<ProductData> testProducts)
    {
        _testProducts = testProducts;
    }

    public IEnumerable<ProductData> TestProducts => _testProducts;
}

public class TestInMemoryProductRepository
{
    private readonly List<Product> _products;

    public TestInMemoryProductRepository(IEnumerable<ProductData> testProducts)
    {
        _products = testProducts.Select(ProductFactory.Create).ToList();
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
