using CashRegister.Domain.ValueObjects;

namespace CashRegister.Domain.Interfaces;

public interface IBarcodeScannerService
{
  Barcode Scan();
  bool TryScan(out Barcode? barcode);
}
