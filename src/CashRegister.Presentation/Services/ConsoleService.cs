namespace CashRegister.Presentation.Services;

public class ConsoleService
{
  public void Clear()
  {
    Console.Clear();
  }

  public void WriteLine(string message)
  {
    Console.WriteLine(message);
  }

  public void Write(string message)
  {
    Console.Write(message);
  }

  public void PrintSeparator(char separatorChar = '-', int length = 40)
  {
    Console.WriteLine(new string(separatorChar, length));
  }

  public void PrintHeader(string title, char separatorChar = '-', int length = 40)
  {
    PrintSeparator(separatorChar, length);
    Console.WriteLine(title);
    PrintSeparator(separatorChar, length);
  }

  public void WaitForAnyKey(string message = "Нажмите любую клавишу для продолжения...")
  {
    Console.WriteLine(message);
    Console.ReadKey();
  }
}
