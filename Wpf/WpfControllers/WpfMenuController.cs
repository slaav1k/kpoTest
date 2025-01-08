using System.Windows;
using System.Windows.Input;

using Base.Controllers;
using Wpf.WpfModels.Menu;
using Wpf.WpfViews;

namespace Wpf.WpfControllers
{
  /// <summary>
  /// WPF контроллер меню.
  /// </summary>
  public class WpfMenuController : IMenuController
  {
    /// <summary>
    /// Представление меню.
    /// </summary>
    private readonly WpfMenuView _view;

    /// <summary>
    /// Меню.
    /// </summary>
    private readonly WpfMenu _menu;

    /// <summary>
    /// Главное окно приложения.
    /// </summary>
    private readonly Window _mainWindow;

    /// <summary>
    /// Указывает, находится ли контроллер в состоянии работы.
    /// </summary>
    private bool _isRunning;

    /// <summary>
    /// Контроллер игры.
    /// </summary>
    private readonly WpfGameController _gameController;

    /// <summary>
    /// Контроллер правил.
    /// </summary>
    private readonly WpfRulesController _rulesController;

    /// <summary>
    /// Контроллер таблицы рекордов.
    /// </summary>
    private readonly WpfHighScoresController _highScoresController;

    /// <summary>
    /// Инициализирует новый экземпляр WPF контроллера меню.
    /// </summary>
    public WpfMenuController()
    {
      _view = new WpfMenuView();
      _mainWindow = MainScreen.GetInstance().Window;

      var gameView = new WpfGameView();
      _gameController = new WpfGameController(gameView);
      _rulesController = new WpfRulesController(new WpfRulesView());
      _highScoresController = new WpfHighScoresController(new WpfHighScoresView());

      var menuFactory = new WpfMenuFactory(
          _gameController,
          _rulesController,
          _highScoresController);

      _menu = menuFactory.CreateMainMenu();

      _mainWindow.KeyDown += Window_KeyDown;
    }

    /// <summary>
    /// Запускает отображение меню.
    /// </summary>
    public void Start()
    {
      _isRunning = true;
      _view.Show();
      _view.Render(_menu);
    }

    /// <summary>
    /// Останавливает работу меню.
    /// </summary>
    public void Stop()
    {
      _isRunning = false;
      _mainWindow.KeyDown -= Window_KeyDown;

      _gameController.Stop();
      _rulesController.Stop();
      _highScoresController.Stop();
    }

    /// <summary>
    /// Обрабатывает нажатие клавиш в окне.
    /// </summary>
    /// <param name="parSender">Источник события.</param>
    /// <param name="parE">Аргументы события нажатия клавиши.</param>
    private void Window_KeyDown(object parSender, KeyEventArgs parE)
    {
      if (!_isRunning) return;

      switch (parE.Key)
      {
        case Key.Up:
          _menu.MoveUp();
          _view.Render(_menu);
          break;
        case Key.Down:
          _menu.MoveDown();
          _view.Render(_menu);
          break;
        case Key.Enter:
          Stop();
          _menu.ExecuteSelected();
          break;
      }
    }
  }

}

