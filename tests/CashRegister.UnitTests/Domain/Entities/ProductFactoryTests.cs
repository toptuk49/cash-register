using CashRegister.Domain.Entities;
using CashRegister.Domain.ValueObjects;

namespace CashRegister.UnitTests.Domain.Entities;

public class ProductFactoryTests
{
  [Fact]
  public void Create_ValidProductData_CreatesProduct()
  {
    var productData = new ProductData("4006381333931", "Тестовый продукт", 15.5m);

    var product = ProductFactory.Create(productData);

    Assert.Equal(productData.Barcode, product.Barcode);
    Assert.Equal(productData.Name, product.Name);
    Assert.Equal(productData.Price, product.Price);
  }

  [Fact]
  public void Create_NullProductData_ThrowsArgumentNullException()
  {
    Assert.Throws<ArgumentNullException>(() => ProductFactory.Create(null!));
  }
}
