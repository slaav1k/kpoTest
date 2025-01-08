using Consolee.ConsoleControllers;

namespace JetpackJoyrideConsole
{
  /// <summary>
  /// Запускающий класс
  /// </summary>
  public class Runner
  {
    /// <summary>
    /// Точка входа в приложение. Инициализирует и запускает меню игры
    /// </summary>
    /// <param name="args">Массив аргументов командной строки (не используется)</param>
    static void Main(string[] args)
    {
      var menuController = new MenuController();
      menuController.Start();
    }
  }
}
