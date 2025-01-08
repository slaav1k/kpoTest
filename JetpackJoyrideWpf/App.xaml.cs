using System.Windows;
using Wpf.WpfControllers;

namespace JetpackJoyrideWpf
{
  /// <summary>
  /// Главный класс приложения WPF.
  /// Управляет жизненным циклом приложения.
  /// </summary>
  public partial class App : Application
  {
    /// <summary>
    /// Обработчик события запуска приложения.
    /// Инициализирует и запускает контроллер меню для WPF.
    /// </summary>
    /// <param name="sender">Источник события.</param>
    /// <param name="e">Аргументы события запуска приложения.</param>
    private void Application_Startup(object sender, StartupEventArgs e)
    {
      var menuController = new WpfMenuController();
      menuController.Start();
    }
  }
}
