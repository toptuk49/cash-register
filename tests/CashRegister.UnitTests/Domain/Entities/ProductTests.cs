using CashRegister.Domain.Entities;
using CashRegister.Domain.ValueObjects;

namespace CashRegister.UnitTests.Domain.Entities;

public class ProductTests
{
  [Fact]
  public void Constructor_ValidParameters_CreatesProduct()
  {
    var barcode = "4006381333931";
    var name = "Тестовый продукт";
    var price = 10.50m;

    var product = new Product(barcode, name, price);

    Assert.Equal(barcode, product.Barcode);
    Assert.Equal(name, product.Name);
    Assert.Equal(price, product.Price);
  }

  [Theory]
  [InlineData("")]
  [InlineData(" ")]
  [InlineData(null)]
  public void Constructor_InvalidBarcode_ThrowsArgumentException(string invalidBarcode)
  {
    var name = "Тестовый продукт";
    var price = 10.50m;

    Assert.Throws<ArgumentException>(() => new Product(invalidBarcode, name, price));
  }

  [Theory]
  [InlineData("")]
  [InlineData(" ")]
  [InlineData(null)]
  public void Constructor_InvalidName_ThrowsArgumentException(string invalidName)
  {
    var barcode = "4006381333931";
    var price = 10.50m;

    Assert.Throws<ArgumentException>(() => new Product(barcode, invalidName, price));
  }

  [Fact]
  public void Constructor_NegativePrice_ThrowsArgumentException()
  {

    var barcode = "4006381333931";
    var name = "Тестовый продукт";
    var negativePrice = -1.0m;

    Assert.Throws<ArgumentException>(() => new Product(barcode, name, negativePrice));
  }

  [Fact]
  public void Equals_SameBarcode_ReturnsTrue()
  {

    var product1 = new Product("4006381333931", "Продукт 1", 10.0m);
    var product2 = new Product("4006381333931", "Продукт 2", 20.0m);

    var areEqual = product1.Equals(product2);

    Assert.True(areEqual);
  }

  [Fact]
  public void Equals_DifferentBarcode_ReturnsFalse()
  {

    var product1 = new Product("4006381333931", "Продукт", 10.0m);
    var product2 = new Product("4006381333948", "Продукт", 10.0m);

    var areEqual = product1.Equals(product2);

    Assert.False(areEqual);
  }

  [Fact]
  public void GetHashCode_SameBarcode_ReturnsSameHashCode()
  {

    var product1 = new Product("4006381333931", "Продукт 1", 10.0m);
    var product2 = new Product("4006381333931", "Продукт 2", 20.0m);

    var hashCode1 = product1.GetHashCode();
    var hashCode2 = product2.GetHashCode();

    Assert.Equal(hashCode1, hashCode2);
  }
}
