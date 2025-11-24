using CashRegister.Domain.ValueObjects;

namespace CashRegister.UnitTests.ValueObjects;

public class BarcodeTests
{
  [Theory]
  [InlineData("4006381333931")] // Valid EAN-13
  [InlineData("4006381333948")] // Valid EAN-13
  [InlineData("4006381333955")] // Valid EAN-13
  [InlineData("4006381333962")] // Valid EAN-13
  [InlineData("4006381333979")] // Valid EAN-13
  [InlineData("4006381333986")] // Valid EAN-13
  [InlineData("4006381333993")] // Valid EAN-13
  [InlineData("4006381334006")] // Valid EAN-13
  public void Constructor_ValidEan13_CreatesBarcode(string validBarcode)
  {
    var barcode = new Barcode(validBarcode);

    Assert.Equal(validBarcode, barcode.Value);
  }

  [Theory]
  [InlineData("")]
  [InlineData(" ")]
  [InlineData("123")]
  [InlineData("123456789012")] // 12 digits
  [InlineData("12345678901234")] // 14 digits
  [InlineData("123456789012a")] // contains letter
  [InlineData("4006381333932")] // invalid checksum
  [InlineData("4006381333949")] // invalid checksum
  [InlineData("4006381333956")] // invalid checksum
  public void Constructor_InvalidEan13_ThrowsArgumentException(string invalidBarcode)
  {
    Assert.Throws<ArgumentException>(() => new Barcode(invalidBarcode));
  }

  [Theory]
  [InlineData("4006381333931", true)]
  [InlineData("4006381333948", true)]
  [InlineData("4006381333955", true)]
  [InlineData("4006381333962", true)]
  [InlineData("4006381333979", true)]
  [InlineData("4006381333986", true)]
  [InlineData("4006381333993", true)]
  [InlineData("4006381334006", true)]
  [InlineData("", false)]
  [InlineData("123", false)]
  [InlineData("4006381333932", false)]
  [InlineData("4006381333949", false)]
  [InlineData("123456789012a", false)]
  public void IsValidEan13_VariousInputs_ReturnsCorrectResult(string barcode, bool expected)
  {
    var result = Barcode.IsValidEan13(barcode);

    Assert.Equal(expected, result);
  }

  [Theory]
  [InlineData("4006381333931", true)]
  [InlineData("4006381333948", true)]
  [InlineData("4006381334006", true)]
  [InlineData("4006381333932", false)]
  [InlineData("4006381333949", false)]
  public void TryCreate_VariousInputs_ReturnsCorrectResult(string barcode, bool expectedSuccess)
  {
    var result = Barcode.TryCreate(barcode, out var createdBarcode);

    Assert.Equal(expectedSuccess, result);
    if (expectedSuccess)
    {
      Assert.NotNull(createdBarcode);
      Assert.Equal(barcode, createdBarcode.Value);
    }
    else
    {
      Assert.Null(createdBarcode);
    }
  }

  [Fact]
  public void ToString_ReturnsValue()
  {
    var value = "4006381334006";
    var barcode = new Barcode(value);

    var result = barcode.ToString();

    Assert.Equal(value, result);
  }

  [Fact]
  public void ImplicitConversion_ToString_Works()
  {
    var value = "4006381334006";
    var barcode = new Barcode(value);

    string result = barcode;

    Assert.Equal(value, result);
  }

  [Fact]
  public void RecordEquality_SameValue_AreEqual()
  {
    var barcode1 = new Barcode("4006381334006");
    var barcode2 = new Barcode("4006381334006");

    Assert.Equal(barcode1, barcode2);
    Assert.True(barcode1 == barcode2);
  }

  [Fact]
  public void RecordEquality_DifferentValue_AreNotEqual()
  {
    var barcode1 = new Barcode("4006381334006");
    var barcode2 = new Barcode("4006381333931");

    Assert.NotEqual(barcode1, barcode2);
    Assert.True(barcode1 != barcode2);
  }
}
