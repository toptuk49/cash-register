using CashRegister.Domain.Entities;

namespace CashRegister.UnitTests.Domain.Entities;

public class ReceiptTests
{
  private Product CreateTestProduct(string barcode = "4006381333931")
  {
    return new Product(barcode, "Тестовый продукт", 10.0m);
  }

  [Fact]
  public void Constructor_CreatesReceiptWithDefaultValues()
  {
    var receipt = new Receipt();

    Assert.NotEqual(Guid.Empty, receipt.Id);
    Assert.True(receipt.CreatedAt <= DateTime.Now);
    Assert.Empty(receipt.Items);
    Assert.Equal(0, receipt.TotalAmount);
    Assert.False(receipt.IsReturn);
  }

  [Fact]
  public void AddProduct_ValidProduct_AddsToItems()
  {
    var receipt = new Receipt();
    var product = CreateTestProduct();

    receipt.AddProduct(product);

    var item = receipt.Items.Single();
    Assert.Equal(product, item.Product);
    Assert.Equal(1, item.Quantity);
    Assert.Equal(product.Price, receipt.TotalAmount);
  }

  [Fact]
  public void AddProduct_WithQuantity_AddsCorrectQuantity()
  {
    var receipt = new Receipt();
    var product = CreateTestProduct();
    var quantity = 3;

    receipt.AddProduct(product, quantity);

    var item = receipt.Items.Single();
    Assert.Equal(quantity, item.Quantity);
    Assert.Equal(product.Price * quantity, receipt.TotalAmount);
  }

  [Fact]
  public void AddProduct_ExistingProduct_IncreasesQuantity()
  {
    var receipt = new Receipt();
    var product = CreateTestProduct();
    receipt.AddProduct(product, 2);

    receipt.AddProduct(product, 3);

    var item = receipt.Items.Single();
    Assert.Equal(5, item.Quantity);
  }

  [Fact]
  public void AddProduct_NullProduct_ThrowsArgumentNullException()
  {
    var receipt = new Receipt();

    Assert.Throws<ArgumentNullException>(() => receipt.AddProduct(null!));
  }

  [Theory]
  [InlineData(0)]
  [InlineData(-1)]
  public void AddProduct_InvalidQuantity_ThrowsArgumentException(int invalidQuantity)
  {
    var receipt = new Receipt();
    var product = CreateTestProduct();

    Assert.Throws<ArgumentException>(() => receipt.AddProduct(product, invalidQuantity));
  }

  [Fact]
  public void RemoveProduct_ExistingProduct_RemovesFromItems()
  {
    var receipt = new Receipt();
    var product = CreateTestProduct();
    receipt.AddProduct(product, 3);

    var result = receipt.RemoveProduct(product.Barcode, 2);

    Assert.True(result);
    var item = receipt.Items.Single();
    Assert.Equal(1, item.Quantity);
  }

  [Fact]
  public void RemoveProduct_CompleteQuantity_RemovesItem()
  {
    var receipt = new Receipt();
    var product = CreateTestProduct();
    receipt.AddProduct(product, 2);

    var result = receipt.RemoveProduct(product.Barcode, 2);

    Assert.True(result);
    Assert.Empty(receipt.Items);
  }

  [Fact]
  public void RemoveProduct_NonExistentProduct_ReturnsFalse()
  {
    var receipt = new Receipt();

    var result = receipt.RemoveProduct("Несуществующий продукт", 1);

    Assert.False(result);
  }

  [Fact]
  public void GetItemByBarcode_ExistingProduct_ReturnsItem()
  {
    var receipt = new Receipt();
    var product = CreateTestProduct();
    receipt.AddProduct(product);

    var item = receipt.GetItemByBarcode(product.Barcode);

    Assert.NotNull(item);
    Assert.Equal(product, item.Product);
  }

  [Fact]
  public void GetItemByBarcode_NonExistentProduct_ReturnsNull()
  {
    var receipt = new Receipt();

    var item = receipt.GetItemByBarcode("Несуществующий продукт");

    Assert.Null(item);
  }

  [Fact]
  public void CreateCopy_CreatesIdenticalReceipt()
  {
    var original = new Receipt();
    var product1 = CreateTestProduct("4006381333931");
    var product2 = CreateTestProduct("4006381333948");
    original.AddProduct(product1, 2);
    original.AddProduct(product2, 1);

    var copy = original.CreateCopy();

    Assert.NotEqual(original.Id, copy.Id);
    Assert.Equal(original.Items.Count, copy.Items.Count);
    Assert.Equal(original.TotalAmount, copy.TotalAmount);
    Assert.Equal(original.IsReturn, copy.IsReturn);
  }

  [Fact]
  public void CreateCopy_AsReturn_CreatesReturnReceipt()
  {
    var original = new Receipt();
    var product = CreateTestProduct();
    original.AddProduct(product);

    var copy = original.CreateCopy(asReturn: true);

    Assert.True(copy.IsReturn);
  }
}
