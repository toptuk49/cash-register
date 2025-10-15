namespace CashRegister.Presentation.Services;

public class InputService
{
  private readonly ConsoleService _console;

  public InputService(ConsoleService console)
  {
    _console = console;
  }

  public string GetRequiredString(string prompt, string fieldName = "значение")
  {
    while (true)
    {
      _console.Write(prompt);
      var input = Console.ReadLine()?.Trim();

      if (!string.IsNullOrEmpty(input))
        return input;

      _console.WriteLine($"Ошибка: {fieldName} не может быть пустым. Попробуйте снова.");
      _console.PrintSeparator();
    }
  }

  public decimal GetDecimal(string prompt, decimal minValue = 0, decimal maxValue = decimal.MaxValue)
  {
    while (true)
    {
      _console.Write(prompt);
      var input = Console.ReadLine()?.Trim();

      if (decimal.TryParse(input, out decimal result) && result >= minValue && result <= maxValue)
        return result;

      _console.WriteLine($"Ошибка: введите корректное число от {minValue} до {maxValue}.");
      _console.PrintSeparator();
    }
  }

  public int GetInteger(string prompt, int minValue = 0, int maxValue = int.MaxValue)
  {
    while (true)
    {
      _console.Write(prompt);
      var input = Console.ReadLine()?.Trim();

      if (int.TryParse(input, out int result) && result >= minValue && result <= maxValue)
        return result;

      _console.WriteLine($"Ошибка: введите целое число от {minValue} до {maxValue}.");
      _console.PrintSeparator();
    }
  }

  public string GetChoice(string prompt, List<string> validChoices, bool caseSensitive = false)
  {
    while (true)
    {
      _console.Write(prompt);
      var input = Console.ReadLine()?.Trim();

      var comparison = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
      var choice = validChoices.FirstOrDefault(c => c.Equals(input, comparison));

      if (choice != null)
        return choice;

      _console.WriteLine($"Ошибка: допустимые значения: {string.Join(", ", validChoices)}");
      _console.PrintSeparator();
    }
  }

  public bool GetConfirmation(string prompt)
  {
    while (true)
    {
      _console.Write($"{prompt} (д/н): ");
      var input = Console.ReadLine()?.Trim().ToLower();

      switch (input)
      {
        case "д":
        case "да":
          return true;
        case "н":
        case "нет":
          return false;
        default:
          _console.WriteLine("Ошибка: введите 'д' для да или 'н' для нет.");
          _console.PrintSeparator();
          break;
      }
    }
  }
}
