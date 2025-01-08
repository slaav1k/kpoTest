namespace Base.Models
{
  /// <summary>
  /// Запись таблицы рекордов
  /// </summary>
  public class HighScoreRecord
  {
    /// <summary>
    /// Имя игрока
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Количество очков
    /// </summary>
    public int Score { get; set; }


    /// <summary>
    /// Инициализирует новую запись таблицы рекордов
    /// </summary>
    /// <param name="name">Имя игрока</param>
    /// <param name="score">Количество очков</param>
    public HighScoreRecord(string name, int score)
    {
      Name = name;
      Score = score;
    }
  }

}

