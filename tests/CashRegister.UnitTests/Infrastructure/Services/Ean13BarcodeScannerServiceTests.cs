using CashRegister.Domain.ValueObjects;
using CashRegister.Infrastructure.Services;
using Moq;

namespace CashRegister.UnitTests.Infrastructure.Services;

public class Ean13BarcodeScannerServiceTests
{
    [Fact]
    public void Scan_ValidInput_ReturnsBarcode()
    {
        var scanner = new Ean13BarcodeScannerService();
        var input = new StringReader("4006381333931\n");
        Console.SetIn(input);

        var barcode = scanner.Scan();

        Assert.NotNull(barcode);
        Assert.Equal("4006381333931", barcode.Value);
    }

    [Fact]
    public void TryScan_ValidInput_ReturnsTrue()
    {
        var scanner = new Ean13BarcodeScannerService();
        var input = new StringReader("4006381333931\n");
        Console.SetIn(input);

        var result = scanner.TryScan(out var barcode);

        Assert.True(result);
        Assert.NotNull(barcode);
        Assert.Equal("4006381333931", barcode!.Value);
    }

    [Fact]
    public void TryScan_InvalidInput_ReturnsFalse()
    {
        var scanner = new Ean13BarcodeScannerService();
        var input = new StringReader("invalid\n");
        Console.SetIn(input);

        var result = scanner.TryScan(out var barcode);

        Assert.False(result);
        Assert.Null(barcode);
    }
}


