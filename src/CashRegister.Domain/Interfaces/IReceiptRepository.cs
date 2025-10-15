using CashRegister.Domain.Entities;

namespace CashRegister.Domain.Interfaces;

public interface IReceiptRepository
{
  void Add(Receipt receipt);
  Receipt? GetById(Guid id);
  IEnumerable<Receipt> GetAll();
}
