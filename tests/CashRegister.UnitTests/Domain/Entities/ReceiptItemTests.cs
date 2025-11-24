using CashRegister.Domain.Entities;

namespace CashRegister.UnitTests.Domain.Entities;

public class ReceiptItemTests
{
  private Product CreateTestProduct()
  {
    return new Product("4006381333931", "Тестовый продукт", 10.0m);
  }

  [Fact]
  public void Constructor_ValidParameters_CreatesReceiptItem()
  {
    var product = CreateTestProduct();
    var quantity = 3;

    var receiptItem = new ReceiptItem(product, quantity);

    Assert.Equal(product, receiptItem.Product);
    Assert.Equal(quantity, receiptItem.Quantity);
    Assert.Equal(product.Price * quantity, receiptItem.TotalPrice);
  }

  [Fact]
  public void Constructor_NullProduct_ThrowsArgumentNullException()
  {
    Assert.Throws<ArgumentNullException>(() => new ReceiptItem(null!, 1));
  }

  [Theory]
  [InlineData(0)]
  [InlineData(-1)]
  public void Constructor_InvalidQuantity_UsesDefaultQuantity(int invalidQuantity)
  {
    var product = CreateTestProduct();

    var receiptItem = new ReceiptItem(product, invalidQuantity);

    Assert.Equal(1, receiptItem.Quantity);
  }

  [Fact]
  public void IncreaseQuantity_ValidAmount_IncreasesQuantity()
  {
    var product = CreateTestProduct();
    var receiptItem = new ReceiptItem(product, 2);

    receiptItem.IncreaseQuantity(3);

    Assert.Equal(5, receiptItem.Quantity);
  }

  [Theory]
  [InlineData(0)]
  [InlineData(-1)]
  public void IncreaseQuantity_InvalidAmount_DoesNotChangeQuantity(int invalidAmount)
  {
    var product = CreateTestProduct();
    var receiptItem = new ReceiptItem(product, 2);
    var initialQuantity = receiptItem.Quantity;

    receiptItem.IncreaseQuantity(invalidAmount);

    Assert.Equal(initialQuantity, receiptItem.Quantity);
  }

  [Fact]
  public void DecreaseQuantity_ValidAmount_DecreasesQuantity()
  {
    var product = CreateTestProduct();
    var receiptItem = new ReceiptItem(product, 5);

    receiptItem.DecreaseQuantity(2);

    Assert.Equal(3, receiptItem.Quantity);
  }

  [Fact]
  public void DecreaseQuantity_AmountExceedsQuantity_DoesNotChangeQuantity()
  {
    var product = CreateTestProduct();
    var receiptItem = new ReceiptItem(product, 3);
    var initialQuantity = receiptItem.Quantity;

    receiptItem.DecreaseQuantity(5);

    Assert.Equal(initialQuantity, receiptItem.Quantity);
  }

  [Theory]
  [InlineData(0)]
  [InlineData(-1)]
  public void DecreaseQuantity_InvalidAmount_DoesNotChangeQuantity(int invalidAmount)
  {
    var product = CreateTestProduct();
    var receiptItem = new ReceiptItem(product, 3);
    var initialQuantity = receiptItem.Quantity;

    receiptItem.DecreaseQuantity(invalidAmount);

    Assert.Equal(initialQuantity, receiptItem.Quantity);
  }
}
