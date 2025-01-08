using System.Windows.Input;
using System.Windows;

using Wpf.WpfViews;
using Base.Models;

namespace Wpf.WpfControllers
{
  /// <summary>
  /// WPF-контроллер для добавления нового рекорда.
  /// </summary>
  public class WpfAddHighScoreController
  {
    /// <summary>
    /// Представление формы добавления рекорда.
    /// </summary>
    private readonly WpfAddHighScoreView _view;

    /// <summary>
    /// Менеджер для управления таблицей рекордов.
    /// </summary>
    private readonly HighScoresManager _manager;

    /// <summary>
    /// Главное окно приложения.
    /// </summary>
    private readonly Window _mainWindow;

    /// <summary>
    /// Количество очков, которые нужно сохранить.
    /// </summary>
    private readonly int _score;

    /// <summary>
    /// Флаг, указывающий, активна ли форма добавления рекорда.
    /// </summary>
    private bool _isRunning;

    /// <summary>
    /// Инициализирует новый экземпляр WPF-контроллера добавления рекорда.
    /// </summary>
    /// <param name="parScore">Количество очков, которые нужно сохранить.</param>
    public WpfAddHighScoreController(int parScore)
    {
      _view = new WpfAddHighScoreView();
      _manager = new HighScoresManager();
      _score = parScore;
      _mainWindow = MainScreen.GetInstance().Window;
      _mainWindow.KeyDown += Window_KeyDown;
    }

    /// <summary>
    /// Отображает форму добавления рекорда.
    /// </summary>
    public void Show()
    {
      _isRunning = true;
      _view.Show(_score);
    }

    /// <summary>
    /// Обрабатывает нажатие клавиш в главном окне.
    /// </summary>
    /// <param name="parSender">Источник события.</param>
    /// <param name="parE">Аргументы события нажатия клавиши.</param>
    private void Window_KeyDown(object parSender, KeyEventArgs parE)
    {
      if (!_isRunning) return;

      if (parE.Key == Key.Enter)
      {
        _isRunning = false;
        _mainWindow.KeyDown -= Window_KeyDown;
        string playerName = _view.GetPlayerName();
        _manager.AddScore(playerName, _score);

        new WpfMenuController().Start();
      }
    }
  }

}

