namespace Base.Controllers
{
  /// <summary>
  /// Интерфейс контроллера таблицы рекордов
  /// </summary>
  public interface IHighScoresController
  {
    /// <summary>
    /// Отображает таблицу рекордов
    /// </summary>
    void Show();

    /// <summary>
    /// Добавляет новый рекорд
    /// </summary>
    /// <param name="name">Имя игрока</param>
    /// <param name="score">Количество очков</param>
    void AddScore(string name, int score);

    /// <summary>
    /// Проверяет, является ли счет новым рекордом
    /// </summary>
    /// <param name="score">Количество очков</param>
    /// <returns>true, если счет является новым рекордом</returns>
    bool IsNewHighScore(int score);
  }
}
