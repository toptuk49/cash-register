using CashRegister.Domain.Entities;
using CashRegister.Domain.Interfaces;

namespace CashRegister.Infrastructure.Persistence;

public class InMemoryReceiptRepository : IReceiptRepository
{
  private readonly Dictionary<Guid, Receipt> _receipts = new();

  public void Add(Receipt receipt)
  {
    if (!_receipts.ContainsKey(receipt.Id))
    {
      _receipts[receipt.Id] = receipt;
    }
  }

  public Receipt? GetById(Guid id) => _receipts.TryGetValue(id, out var receipt) ? receipt : null;

  public IEnumerable<Receipt> GetAll() => _receipts.Values;
}
