namespace CashRegister.Presentation.Services;

public class AdminService
{
  public void Run()
  {
    Console.WriteLine("Авторизован, как администратор.");
    Console.WriteLine("Доступные действия: сканировать, отчет, экспортировать, выход");

    while (true)
    {
      Console.Write("Введите действие: ");
      var cmd = Console.ReadLine();

      switch (cmd)
      {
        case "сканировать":
          Console.WriteLine("Сканируем продукт...");
          break;
        case "отчет":
          Console.WriteLine("Формируем отчет...");
          break;
        case "экспортировать":
          Console.WriteLine("Экспортируем данные в учетную систему...");
          break;
        case "выход":
          return;
        default:
          Console.WriteLine("Неизвестное действие.");
          break;
      }
    }
  }
}
