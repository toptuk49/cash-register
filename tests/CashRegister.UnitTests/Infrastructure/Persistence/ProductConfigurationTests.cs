using CashRegister.Domain.ValueObjects;
using CashRegister.Infrastructure.Persistence;

namespace CashRegister.UnitTests.Infrastructure.Persistence;

public class ProductConfigurationTests
{
    [Fact]
    public void DefaultProducts_ContainsExpectedProducts()
    {
        var configuration = new ProductConfiguration();

        var products = configuration.DefaultProducts;

        Assert.NotEmpty(products);
        Assert.Contains(products, p => p.Barcode == "4006381333931" && p.Name == "Молоко");
        Assert.Contains(products, p => p.Barcode == "4006381333948" && p.Name == "Хлеб");
    }

    [Fact]
    public void DefaultProducts_AllHaveValidEan13Barcodes()
    {
        var configuration = new ProductConfiguration();

        var products = configuration.DefaultProducts;

        foreach (var product in products)
        {
            Assert.True(Barcode.IsValidEan13(product.Barcode));
        }
    }
}
