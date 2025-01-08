using Base.Models;
using Base.Views;

namespace Consolee.ConsoleViews
{
  /// <summary>
  /// Консольное представление таблицы рекордов
  /// </summary>
  public class HighScoresView : HighScoresViewBase
  {
    /// <summary>
    /// Отрисовывает таблицу рекордов
    /// </summary>
    /// <param name="parScores">Список рекордов</param>
    public override void Render(IReadOnlyList<HighScoreRecord> parScores)
    {
      Console.BackgroundColor = ConsoleColor.DarkBlue;
      Console.Clear();
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.SetCursorPosition(ConsoleView.CONSOLE_WIDTH / 2 - TITLE.Length / 2, 2);
      Console.WriteLine(TITLE);
      Console.ForegroundColor = ConsoleColor.Gray;

      int startY = 5;
      if (parScores.Any())
      {
        for (int i = 0; i < parScores.Count; i++)
        {
          var scoreRecord = parScores[i];
          string line = FormatScoreEntry(i + 1, scoreRecord.Name, scoreRecord.Score);
          Console.SetCursorPosition(ConsoleView.CONSOLE_WIDTH / 2 - line.Length / 2, startY++);
          Console.WriteLine(line);
        }
      }
      else
      {
        Console.SetCursorPosition(ConsoleView.CONSOLE_WIDTH / 2 - NO_SCORES_MESSAGE.Length / 2, startY);
        Console.WriteLine(NO_SCORES_MESSAGE);
      }

      Console.SetCursorPosition(ConsoleView.CONSOLE_WIDTH / 2 - RETURN_MESSAGE.Length / 2, startY + 2);
      Console.WriteLine(RETURN_MESSAGE);
    }
  }
}

