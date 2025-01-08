using Base.Models;
using Base.Views;

namespace Consolee.ConsoleViews
{
  /// <summary>
  /// Консольное представление правил игры
  /// </summary>
  public class RulesView : RulesViewBase
  {
    /// <summary>
    /// Отрисовывает правила игры
    /// </summary>
    public override void Render()
    {
      Console.BackgroundColor = ConsoleColor.DarkBlue;
      Console.Clear();
      Console.ForegroundColor = ConsoleColor.Yellow;

      Console.SetCursorPosition(ConsoleView.CONSOLE_WIDTH / 2 - 3, 2);
      Console.WriteLine("Правила");
      Console.ForegroundColor = ConsoleColor.Gray;

      int startY = 5;
      foreach (var elRule in _rulesModel.Rules)
      {
        Console.SetCursorPosition(ConsoleView.CONSOLE_WIDTH / 2 - elRule.Length / 2, startY++);
        Console.WriteLine(elRule);
      }

      Console.SetCursorPosition(ConsoleView.CONSOLE_WIDTH / 2 - 11, startY + 2);
      Console.WriteLine(RETURN_MESSAGE);

    }
  }
}

