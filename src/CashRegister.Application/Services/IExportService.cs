namespace CashRegister.Application.Services;

public interface IExportService
{
  void ExportReceipts(IEnumerable<object> receipts); // можно уточнить DTO позже
}
