using Base.Models;

namespace Base.Views
{
  /// <summary>
  /// Базовый класс представления таблицы рекордов
  /// </summary>
  public abstract class HighScoresViewBase
  {
    /// <summary>
    /// Заголовок таблицы рекордов
    /// </summary>
    protected const string TITLE = "Рекорды";

    /// <summary>
    /// Сообщение при отсутствии рекордов
    /// </summary>
    protected const string NO_SCORES_MESSAGE = "Еще нет записей!";

    /// <summary>
    /// Сообщение о возврате в меню
    /// </summary>
    protected const string RETURN_MESSAGE = "Нажмите ESC для выхода";

    /// <summary>
    /// Отрисовывает таблицу рекордов
    /// </summary>
    /// <param name="parScores">Список рекордов</param>
    public abstract void Render(IReadOnlyList<HighScoreRecord> parScores);

    /// <summary>
    /// Форматирует строку записи рекорда
    /// </summary>
    /// <param name="parPosition">Позиция в таблице</param>
    /// <param name="parName">Имя игрока</param>
    /// <param name="parScore">Количество очков</param>
    /// <returns>Отформатированная строка</returns>
    protected virtual string FormatScoreEntry(int parPosition, string parName, int parScore)
    {
      return $"{parPosition,2}. {parName,-15} {parScore,6}";
    }
  }
}

