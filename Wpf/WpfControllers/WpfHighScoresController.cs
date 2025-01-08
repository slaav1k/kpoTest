using System.Windows;
using System.Windows.Input;

using Base.Controllers;
using Base.Models;
using Wpf.WpfViews;


namespace Wpf.WpfControllers
{
  /// <summary>
  /// WPF контроллер таблицы рекордов.
  /// </summary>
  public class WpfHighScoresController : IHighScoresController
  {
    /// <summary>
    /// Представление таблицы рекордов.
    /// </summary>
    private readonly WpfHighScoresView _view;

    /// <summary>
    /// Менеджер таблицы рекордов.
    /// </summary>
    private readonly HighScoresManager _manager;

    /// <summary>
    /// Главное окно приложения.
    /// </summary>
    private readonly Window _mainWindow;

    /// <summary>
    /// Указывает, находится ли контроллер в состоянии работы.
    /// </summary>
    private bool _isRunning;

    /// <summary>
    /// Инициализирует новый экземпляр WPF контроллера таблицы рекордов.
    /// </summary>
    /// <param name="parView">Представление таблицы рекордов.</param>
    public WpfHighScoresController(WpfHighScoresView parView)
    {
      _view = parView;
      _manager = new HighScoresManager();
      _mainWindow = MainScreen.GetInstance().Window;
    }

    /// <summary>
    /// Отображает таблицу рекордов.
    /// </summary>
    public void Show()
    {
      _isRunning = true;
      _view.Show();
      _view.Render(_manager.GetScores());
      _mainWindow.KeyDown += Window_KeyDown;
    }

    /// <summary>
    /// Обрабатывает нажатие клавиш в окне.
    /// </summary>
    /// <param name="parSender">Источник события.</param>
    /// <param name="parE">Аргументы события нажатия клавиши.</param>
    private void Window_KeyDown(object parSender, KeyEventArgs parE)
    {
      if (!_isRunning) return;

      if (parE.Key == Key.Escape)
      {
        _isRunning = false;
        _mainWindow.KeyDown -= Window_KeyDown;
        new WpfMenuController().Start();
      }
    }

    /// <summary>
    /// Добавляет новый рекорд.
    /// </summary>
    /// <param name="parName">Имя игрока.</param>
    /// <param name="parScore">Количество очков.</param>
    public void AddScore(string parName, int parScore)
    {
      _manager.AddScore(parName, parScore);
    }

    /// <summary>
    /// Проверяет, является ли счет новым рекордом.
    /// </summary>
    /// <param name="parScore">Количество очков.</param>
    /// <returns>true, если счет является новым рекордом; в противном случае false.</returns>
    public bool IsNewHighScore(int parScore)
    {
      return _manager.IsNewHighScore(parScore);
    }

    /// <summary>
    /// Останавливает контроллер.
    /// </summary>
    public void Stop()
    {
      _isRunning = false;
      _mainWindow.KeyDown -= Window_KeyDown;
    }
  }

}

