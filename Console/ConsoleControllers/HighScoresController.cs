using Base.Controllers;
using Base.Models;
using Consolee.ConsoleViews;

namespace Consolee.ConsoleControllers
{
  /// <summary>
  /// Консольный контроллер таблицы рекордов
  /// </summary>
  public class HighScoresController : IHighScoresController
  {
    /// <summary>
    /// Представление таблицы рекордов
    /// </summary>
    private readonly HighScoresView _view;

    /// <summary>
    /// Менеджер для работы с таблицей рекордов
    /// </summary>
    private readonly HighScoresManager _manager;

    /// <summary>
    /// Флаг, указывающий, что контроллер работает
    /// </summary>
    private bool _isRunning;

    /// <summary>
    /// Инициализирует новый экземпляр консольного контроллера таблицы рекордов
    /// </summary>
    public HighScoresController()
    {
      _view = new HighScoresView();
      _manager = new HighScoresManager();
    }

    /// <summary>
    /// Отображает таблицу рекордов
    /// </summary>
    public void Show()
    {
      _isRunning = true;
      _view.Render(_manager.GetScores());

      while (_isRunning)
      {
        var key = Console.ReadKey(true);
        if (key.Key == ConsoleKey.Escape || key.Key == ConsoleKey.Enter)
        {
          _isRunning = false;
        }
      }
    }

    /// <summary>
    /// Добавляет новый рекорд
    /// </summary>
    /// <param name="parName">Имя игрока</param>
    /// <param name="parScore">Количество очков</param>
    public void AddScore(string parName, int parScore)
    {
      _manager.AddScore(parName, parScore);
    }

    /// <summary>
    /// Проверяет, является ли счет новым рекордом
    /// </summary>
    /// <param name="parScore">Количество очков</param>
    /// <returns>true, если счет является новым рекордом</returns>
    public bool IsNewHighScore(int parScore)
    {
      return _manager.IsNewHighScore(parScore);
    }
  }

}

