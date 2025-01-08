using Base.Controllers;
using Consolee.ConsoleViews;
using Consolee.ConsoleModels.Menu;

namespace Consolee.ConsoleControllers
{
  /// <summary>
  /// Консольный контроллер меню
  /// </summary>
  public class MenuController : IMenuController
  {
    /// <summary>
    /// Представление меню
    /// </summary>
    private readonly MenuView _view;

    /// <summary>
    /// Меню
    /// </summary>
    private readonly Menu _menu;

    /// <summary>
    /// Флаг, указывающий, что меню работает
    /// </summary>
    private bool _isRunning;

    /// <summary>
    /// Инициализирует новый экземпляр консольного контроллера меню
    /// </summary>
    public MenuController()
    {
      var highScoresController = new HighScoresController();
      var gameController = new GameController(highScoresController);
      var rulesController = new RulesController();

      var menuFactory = new MenuFactory(
          gameController,
          rulesController,
          highScoresController);

      _menu = menuFactory.CreateMainMenu();
      _view = new MenuView();
    }

    /// <summary>
    /// Запускает отображение меню
    /// </summary>
    public void Start()
    {
      _isRunning = true;
      while (_isRunning)
      {
        _view.Render(_menu);

        var key = Console.ReadKey(true);
        switch (key.Key)
        {
          case ConsoleKey.UpArrow:
            _menu.MoveUp();
            break;
          case ConsoleKey.DownArrow:
            _menu.MoveDown();
            break;
          case ConsoleKey.Enter:
            _menu.ExecuteSelected();
            break;
        }
      }
    }

    /// <summary>
    /// Останавливает работу меню
    /// </summary>
    public void Stop()
    {
      _isRunning = false;
    }
  }

}

