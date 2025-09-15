using CashRegister.Domain.Entities;

namespace CashRegister.Domain.Interfaces;

public interface IProductRepository
{
  Product? GetByBarcode(string barcode);
  IEnumerable<Product> GetAll();
}
