using CashRegister.Domain.Entities;
using CashRegister.Infrastructure.Persistence;

namespace CashRegister.UnitTests.Infrastructure;

public class InMemoryReceiptRepositoryTests
{
  private InMemoryReceiptRepository CreateRepository()
  {
    return new InMemoryReceiptRepository();
  }

  private Receipt CreateTestReceipt()
  {
    var receipt = new Receipt();
    var product = new Product("4006381333931", "Тестовый продукт", 10.0m);
    receipt.AddProduct(product, 2);
    return receipt;
  }

  [Fact]
  public void Add_NewReceipt_AddsToRepository()
  {
    var repository = CreateRepository();
    var receipt = CreateTestReceipt();

    repository.Add(receipt);

    var retrieved = repository.GetById(receipt.Id);
    Assert.NotNull(retrieved);
    Assert.Equal(receipt.Id, retrieved.Id);
  }

  [Fact]
  public void Add_DuplicateReceipt_DoesNotThrow()
  {
    var repository = CreateRepository();
    var receipt = CreateTestReceipt();
    repository.Add(receipt);

    var exception = Record.Exception(() => repository.Add(receipt));
    Assert.Null(exception);
  }

  [Fact]
  public void GetById_ExistingReceipt_ReturnsReceipt()
  {
    var repository = CreateRepository();
    var receipt = CreateTestReceipt();
    repository.Add(receipt);

    var retrieved = repository.GetById(receipt.Id);

    Assert.NotNull(retrieved);
    Assert.Equal(receipt.Id, retrieved.Id);
    Assert.Equal(receipt.TotalAmount, retrieved.TotalAmount);
  }

  [Fact]
  public void GetById_NonExistentReceipt_ReturnsNull()
  {
    var repository = CreateRepository();

    var retrieved = repository.GetById(Guid.NewGuid());

    Assert.Null(retrieved);
  }

  [Fact]
  public void GetAll_WithReceipts_ReturnsAllReceipts()
  {
    var repository = CreateRepository();
    var receipt1 = CreateTestReceipt();
    var receipt2 = CreateTestReceipt();

    repository.Add(receipt1);
    repository.Add(receipt2);

    var allReceipts = repository.GetAll().ToList();

    Assert.Equal(2, allReceipts.Count);
    Assert.Contains(allReceipts, r => r.Id == receipt1.Id);
    Assert.Contains(allReceipts, r => r.Id == receipt2.Id);
  }

  [Fact]
  public void GetAll_EmptyRepository_ReturnsEmptyCollection()
  {
    var repository = CreateRepository();

    var allReceipts = repository.GetAll();

    Assert.Empty(allReceipts);
  }
}
