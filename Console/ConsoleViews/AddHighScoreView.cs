namespace Consolee.ConsoleViews
{
  /// <summary>
  /// Консольное представление добавления нового рекорда
  /// </summary>
  public class AddHighScoreView
  {
    /// <summary>
    /// Отображает форму добавления рекорда
    /// </summary>
    /// <param name="parScore">Количество очков</param>
    public void Render(int parScore)
    {
      Console.BackgroundColor = ConsoleColor.DarkBlue;
      Console.Clear();

      Console.SetCursorPosition(ConsoleView.CONSOLE_WIDTH / 2 - 5, ConsoleView.CONSOLE_HEIGHT / 2 - 2);
      Console.WriteLine("Новый рекорд!");

      Console.SetCursorPosition(ConsoleView.CONSOLE_WIDTH / 2 - 10, ConsoleView.CONSOLE_HEIGHT / 2);
      Console.WriteLine($"Ваш счет: {parScore}");

      Console.SetCursorPosition(ConsoleView.CONSOLE_WIDTH / 2 - 12, ConsoleView.CONSOLE_HEIGHT / 2 + 2);
      Console.Write("Введите имя: ");
    }

    /// <summary>
    /// Считывает имя игрока
    /// </summary>
    /// <returns>Введенное имя</returns>
    public string ReadPlayerName()
    {
      return Console.ReadLine() ?? "Гость";
    }
  }
}

