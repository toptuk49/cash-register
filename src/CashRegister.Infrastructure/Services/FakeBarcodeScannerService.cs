// CashRegister.Infrastructure/Services/FakeBarcodeScannerService.cs
using CashRegister.Domain.Interfaces;
using CashRegister.Domain.ValueObjects;

namespace CashRegister.Infrastructure.Services;

public class FakeBarcodeScannerService : IBarcodeScannerService
{
    private readonly List<string> _predefinedBarcodes = new()
    {
        "4601234567890", // Valid EAN-13
        "4609876543210", 
        "4612345678901"
    };

    private int _currentIndex = 0;

    public Barcode Scan()
    {
        var barcode = _predefinedBarcodes[_currentIndex];
        _currentIndex = (_currentIndex + 1) % _predefinedBarcodes.Count;
        
        Console.WriteLine($"[Имитация сканера] Отсканировал: {barcode}");
        return new Barcode(barcode);
    }

    public bool TryScan(out Barcode? barcode)
    {
        barcode = Scan();
        return true;
    }
}
