using Base.Controllers;
using Consolee.ConsoleViews;

namespace Consolee.ConsoleControllers
{
  /// <summary>
  /// Консольный контроллер правил
  /// </summary>
  public class RulesController : IRulesController
  {
    /// <summary>
    /// Представление правил
    /// </summary>
    private readonly RulesView _view;

    /// <summary>
    /// Флаг, указывающий, что контроллер правил работает
    /// </summary>
    private bool _isRunning;

    /// <summary>
    /// Инициализирует новый экземпляр консольного контроллера правил
    /// </summary>
    public RulesController()
    {
      _view = new RulesView();
    }

    /// <summary>
    /// Отображает правила игры
    /// </summary>
    public void Show()
    {
      _isRunning = true;
      _view.Render();

      while (_isRunning)
      {
        var key = Console.ReadKey(true);
        if (key.Key == ConsoleKey.Escape || key.Key == ConsoleKey.Enter)
        {
          _isRunning = false;
        }
      }
    }
  }

}

