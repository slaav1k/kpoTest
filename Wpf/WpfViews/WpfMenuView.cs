using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Base.Views;
using Base.Models.Menu;

namespace Wpf.WpfViews
{
  /// <summary>
  /// WPF представление меню
  /// </summary>
  public class WpfMenuView : MenuViewBase
  {
    /// <summary>
    /// Экземпляр экрана, на котором отображается меню.
    /// </summary>
    private readonly MainScreen _screen;

    /// <summary>
    /// Панель, содержащая элементы меню.
    /// </summary>
    private readonly StackPanel _menuPanel;

    /// <summary>
    /// Текстовый блок, отображающий заголовок меню.
    /// </summary>
    private readonly TextBlock _titleBlock;

    /// <summary>
    /// Список текстовых блоков, представляющих пункты меню.
    /// </summary>
    private readonly List<TextBlock> _menuItems;

    /// <summary>
    /// Инициализирует новый экземпляр WPF представления меню.
    /// </summary>
    public WpfMenuView()
    {
      _screen = MainScreen.GetInstance();
      _menuItems = new List<TextBlock>();

      _titleBlock = new TextBlock
      {
        Text = "Jetpack Joyride",
        FontSize = 48,
        FontFamily = new FontFamily("Comic Sans MS"),
        Foreground = Brushes.Yellow,
        HorizontalAlignment = HorizontalAlignment.Center,
        Margin = new Thickness(0, 20, 0, 40)
      };

      _menuPanel = new StackPanel
      {
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center
      };
    }

    /// <summary>
    /// Отображает меню на экране.
    /// </summary>
    public void Show()
    {
      _screen.StackPanel.Children.Clear();
      _screen.StackPanel.Children.Add(_titleBlock);
      _screen.StackPanel.Children.Add(_menuPanel);

      _screen.Window.Width = WpfGameView.WINDOW_WIDTH;
      _screen.Window.Height = WpfGameView.WINDOW_HEIGHT;
      _screen.Window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
    }

    /// <summary>
    /// Отрисовывает меню, создавая текстовые блоки для каждого пункта.
    /// </summary>
    /// <param name="parMenu">Модель меню, содержащая список элементов.</param>
    public override void Render(MenuBase parMenu)
    {
      _menuPanel.Children.Clear();
      _menuItems.Clear();

      foreach (var elItem in parMenu.Items)
      {
        var menuItem = new TextBlock
        {
          Text = elItem.Text,
          FontSize = 24,
          FontFamily = new FontFamily("Comic Sans MS"),
          Foreground = elItem.IsSelected ? Brushes.Yellow : Brushes.White,
          HorizontalAlignment = HorizontalAlignment.Center,
          Margin = new Thickness(0, 10, 0, 10)
        };

        _menuItems.Add(menuItem);
        _menuPanel.Children.Add(menuItem);
      }
    }
  }
}

