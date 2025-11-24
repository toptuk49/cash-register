using CashRegister.Domain.ValueObjects;
using CashRegister.Infrastructure.Services;
using Moq;

namespace CashRegister.UnitTests.Infrastructure.Services;

public class FakeBarcodeScannerServiceTests
{
    [Fact]
    public void Scan_ReturnsPredefinedBarcodesInSequence()
    {
        var scanner = new FakeBarcodeScannerService();

        var barcode1 = scanner.Scan();
        Assert.Equal("4006381333931", barcode1.Value);

        var barcode2 = scanner.Scan();
        Assert.Equal("4006381333948", barcode2.Value);

        var barcode3 = scanner.Scan();
        Assert.Equal("4006381333955", barcode3.Value);

        var barcode4 = scanner.Scan();
        Assert.Equal("4006381333931", barcode4.Value);
    }

    [Fact]
    public void TryScan_AlwaysReturnsTrue()
    {
        var scanner = new FakeBarcodeScannerService();

        var result = scanner.TryScan(out var barcode);

        Assert.True(result);
        Assert.NotNull(barcode);
    }
}
