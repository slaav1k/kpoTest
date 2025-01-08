using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

using Base.Views;


namespace Wpf.WpfViews
{
  /// <summary>
  /// WPF представление правил игры
  /// </summary>
  public class WpfRulesView : RulesViewBase
  {
    /// <summary>
    /// Экземпляр экрана, на котором отображаются правила игры.
    /// </summary>
    private readonly MainScreen _screen;

    /// <summary>
    /// Панель, содержащая правила игры.
    /// </summary>
    private readonly StackPanel _rulesPanel;

    /// <summary>
    /// Текстовый блок, отображающий заголовок окна с правилами.
    /// </summary>
    private readonly TextBlock _titleBlock;

    /// <summary>
    /// Инициализирует новый экземпляр WPF представления правил игры.
    /// </summary>
    public WpfRulesView()
    {
      _screen = MainScreen.GetInstance();

      _titleBlock = new TextBlock
      {
        Text = "Правила",
        FontSize = 48,
        FontFamily = new FontFamily("Comic Sans MS"),
        Foreground = Brushes.Yellow,
        HorizontalAlignment = HorizontalAlignment.Center,
        Margin = new Thickness(0, 20, 0, 40)
      };

      _rulesPanel = new StackPanel
      {
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center
      };
    }

    /// <summary>
    /// Отображает окно с правилами игры.
    /// </summary>
    public void Show()
    {
      _rulesPanel.Children.Clear();
      MainScreen.GetInstance().StackPanel.Children.Clear();
      Render();
    }

    /// <summary>
    /// Отрисовывает правила игры, создавая текстовые блоки для каждого правила.
    /// </summary>
    public override void Render()
    {
      var stackPanel = new StackPanel
      {
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center
      };

      var titleText = new TextBlock
      {
        Text = "ПРАВИЛА",
        FontSize = 32,
        Foreground = Brushes.Yellow,
        TextAlignment = TextAlignment.Center,
        Margin = new Thickness(0, 0, 0, 20)
      };

      var rulesPanel = new StackPanel
      {
        Margin = new Thickness(20)
      };

      foreach (var elRule in _rulesModel.Rules)
      {
        var ruleText = new TextBlock
        {
          Text = elRule,
          FontSize = 16,
          Foreground = Brushes.White,
          TextAlignment = TextAlignment.Left,
          TextWrapping = TextWrapping.Wrap,
          MaxWidth = 600,
          Margin = new Thickness(0, 5, 0, 5)
        };
        rulesPanel.Children.Add(ruleText);
      }

      var exitText = new TextBlock
      {
        Text = RETURN_MESSAGE,
        FontSize = 16,
        FontFamily = new FontFamily("Comic Sans MS"),
        Foreground = Brushes.Gray,
        TextAlignment = TextAlignment.Center,
        Margin = new Thickness(0, 20, 0, 0)
      };

      stackPanel.Children.Add(titleText);
      stackPanel.Children.Add(rulesPanel);
      stackPanel.Children.Add(exitText);
      MainScreen.GetInstance().StackPanel.Children.Add(stackPanel);
    }
  }
}

