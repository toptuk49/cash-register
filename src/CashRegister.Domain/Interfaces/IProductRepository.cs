using CashRegister.Domain.Entities;
using CashRegister.Domain.ValueObjects;

namespace CashRegister.Domain.Interfaces;

public interface IProductRepository
{
  Product? GetByBarcode(string barcode);
  IEnumerable<Product> GetAll();
  void AddProduct(ProductData data);
  void AddProducts(IEnumerable<ProductData> datas);
}
