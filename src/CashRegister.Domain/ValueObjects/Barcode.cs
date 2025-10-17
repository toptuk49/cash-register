namespace CashRegister.Domain.ValueObjects;

public record Barcode
{
  public string Value { get; }

  public Barcode(string value)
  {
    if (!IsValidEan13(value))
      throw new ArgumentException($"Некорректный штрих-код EAN-13: {value}");

    Value = value;
  }

  public static bool IsValidEan13(string barcode)
  {
    if (string.IsNullOrWhiteSpace(barcode))
      return false;

    if (barcode.Length != 13 || !barcode.All(char.IsDigit))
      return false;

    return CheckEan13Checksum(barcode);
  }

  private static bool CheckEan13Checksum(string barcode)
  {
    var digits = barcode.Select(c => int.Parse(c.ToString())).ToArray();

    int sum = 0;
    for (int i = 0; i < 12; i++)
    {
      // Нечетные позиции (считая с 1) умножаются на 3
      // В массиве они имеют четные индексы (0, 2, 4...)
      sum += i % 2 == 0 ? digits[i] * 1 : digits[i] * 3;
    }

    int checksum = (10 - (sum % 10)) % 10;
    return checksum == digits[12];
  }

  public static Barcode Create(string value)
  {
    return new Barcode(value);
  }

  public static bool TryCreate(string value, out Barcode? barcode)
  {
    barcode = null;

    if (IsValidEan13(value))
    {
      barcode = new Barcode(value);
      return true;
    }

    return false;
  }

  public override string ToString() => Value;

  public static implicit operator string(Barcode barcode) => barcode.Value;
}
