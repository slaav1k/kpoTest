using System.Windows;
using System.Windows.Input;

using Base.Controllers;
using Wpf.WpfViews;

namespace Wpf.WpfControllers
{
  /// <summary>
  /// WPF контроллер правил.
  /// </summary>
  public class WpfRulesController : IRulesController
  {
    /// <summary>
    /// Представление правил.
    /// </summary>
    private readonly WpfRulesView _view;

    /// <summary>
    /// Главное окно приложения.
    /// </summary>
    private readonly Window _mainWindow;

    /// <summary>
    /// Указывает, находится ли контроллер в состоянии работы.
    /// </summary>
    private bool _isRunning;

    /// <summary>
    /// Инициализирует новый экземпляр WPF контроллера правил.
    /// </summary>
    /// <param name="parView">Представление правил.</param>
    public WpfRulesController(WpfRulesView parView)
    {
      _view = parView;
      _mainWindow = MainScreen.GetInstance().Window;
    }

    /// <summary>
    /// Отображает правила игры.
    /// </summary>
    public void Show()
    {
      _isRunning = true;
      _view.Show();
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
    /// Останавливает работу контроллера.
    /// </summary>
    public void Stop()
    {
      _isRunning = false;
      _mainWindow.KeyDown -= Window_KeyDown;
    }
  }

}

