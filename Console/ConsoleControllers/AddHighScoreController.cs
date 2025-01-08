using Base.Models;
using Consolee.ConsoleViews;

namespace Consolee.ConsoleControllers
{
  /// <summary>
  /// Контроллер добавления нового рекорда
  /// </summary>
  public class AddHighScoreController
  {
    /// <summary>
    /// Представление для отображения формы добавления рекорда
    /// </summary>
    private readonly AddHighScoreView _view;

    /// <summary>
    /// Менеджер для управления таблицей рекордов
    /// </summary>
    private readonly HighScoresManager _manager;

    /// <summary>
    /// Количество очков, которое будет добавлено в таблицу рекордов
    /// </summary>
    private readonly int _score;

    /// <summary>
    /// Инициализирует новый экземпляр контроллера добавления рекорда
    /// </summary>
    /// <param name="parScore">Количество очков</param>
    public AddHighScoreController(int parScore)
    {
      _view = new AddHighScoreView();
      _manager = new HighScoresManager();
      _score = parScore;
    }

    /// <summary>
    /// Отображает форму добавления рекорда
    /// </summary>
    public void Show()
    {
      _view.Render(_score);
      string playerName = _view.ReadPlayerName();
      _manager.AddScore(playerName, _score);
    }
  }
}

