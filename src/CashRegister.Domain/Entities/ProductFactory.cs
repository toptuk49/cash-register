using CashRegister.Domain.ValueObjects;

namespace CashRegister.Domain.Entities;

public static class ProductFactory
{
  public static Product Create(ProductData data)
  {
    if (data == null) {
      throw new ArgumentNullException(nameof(data));
    }

    return new Product(
        data.Barcode,
        data.Name,
        data.Price
    );
  }
}
