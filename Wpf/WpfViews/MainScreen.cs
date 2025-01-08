using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;


namespace Wpf.WpfViews
{
  /// <summary>
  /// Главное окно приложения.
  /// </summary>
  public class MainScreen
  {
    /// <summary>
    /// Экземпляр главного окна (паттерн Singleton).
    /// </summary>
    private static MainScreen _instance;

    /// <summary>
    /// Панель для размещения элементов в главном окне.
    /// </summary>
    public StackPanel StackPanel { get; set; }

    /// <summary>
    /// Окно приложения.
    /// </summary>
    public Window Window { get; set; }

    /// <summary>
    /// Конструктор, инициализирующий окно и панель.
    /// </summary>
    private MainScreen()
    {
      Window = new Window
      {
        Title = "Jetpack Joyride",
        WindowStyle = WindowStyle.SingleBorderWindow,
        ResizeMode = ResizeMode.NoResize,
        Background = Brushes.DarkBlue
      };

      StackPanel = new StackPanel
      {
        VerticalAlignment = VerticalAlignment.Center,
        HorizontalAlignment = HorizontalAlignment.Center
      };

      Window.Content = StackPanel;
      Window.Show();
    }

    /// <summary>
    /// Получает единственный экземпляр главного окна (паттерн Singleton).
    /// </summary>
    /// <returns>Экземпляр главного окна.</returns>
    public static MainScreen GetInstance()
    {
      if (_instance == null)
      {
        _instance = new MainScreen();
      }
      return _instance;
    }
  }

}

