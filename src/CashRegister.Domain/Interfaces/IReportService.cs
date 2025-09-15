using CashRegister.Domain.Entities;

namespace CashRegister.Domain.Interfaces;

public interface IReportService
{
  string GenerateSalesReport(IEnumerable<Receipt> receipts);
}
