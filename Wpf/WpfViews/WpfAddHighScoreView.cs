using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Wpf.WpfViews
{
  /// <summary>
  /// WPF представление добавления нового рекорда
  /// </summary>
  public class WpfAddHighScoreView
  {
    /// <summary>
    /// Экземпляр главного экрана
    /// </summary>
    private readonly MainScreen _screen;

    /// <summary>
    /// Панель для отображения элементов управления рекордами
    /// </summary>
    private readonly StackPanel _scorePanel;

    /// <summary>
    /// Текстовый блок для заголовка "Новый рекорд!"
    /// </summary>
    private readonly TextBlock _titleBlock;

    /// <summary>
    /// Поле для ввода имени игрока
    /// </summary>
    private readonly TextBox _nameInput;

    /// <summary>
    /// Инициализирует новый экземпляр WPF представления добавления рекорда
    /// </summary>
    public WpfAddHighScoreView()
    {
      _screen = MainScreen.GetInstance();

      _titleBlock = new TextBlock
      {
        Text = "Новый рекорд!",
        FontSize = 48,
        FontFamily = new FontFamily("Comic Sans MS"),
        Foreground = Brushes.Yellow,
        HorizontalAlignment = HorizontalAlignment.Center,
        Margin = new Thickness(0, 20, 0, 40)
      };

      _scorePanel = new StackPanel
      {
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center
      };

      _nameInput = new TextBox
      {
        Width = 200,
        Height = 30,
        FontSize = 20,
        FontFamily = new FontFamily("Comic Sans MS"),
        HorizontalAlignment = HorizontalAlignment.Center,
        Margin = new Thickness(0, 20, 0, 0)
      };
    }

    /// <summary>
    /// Отображает форму добавления рекорда
    /// </summary>
    /// <param name="score">Количество очков</param>
    public void Show(int score)
    {
      _screen.StackPanel.Children.Clear();
      _screen.StackPanel.Children.Add(_titleBlock);

      var scoreText = new TextBlock
      {
        Text = $"Ваш счет: {score}",
        FontSize = 24,
        FontFamily = new FontFamily("Comic Sans MS"),
        Foreground = Brushes.White,
        HorizontalAlignment = HorizontalAlignment.Center,
        Margin = new Thickness(0, 10, 0, 20)
      };

      var promptText = new TextBlock
      {
        Text = "Введите имя:",
        FontSize = 24,
        FontFamily = new FontFamily("Comic Sans MS"),
        Foreground = Brushes.White,
        HorizontalAlignment = HorizontalAlignment.Center
      };

      _scorePanel.Children.Add(scoreText);
      _scorePanel.Children.Add(promptText);
      _scorePanel.Children.Add(_nameInput);
      _screen.StackPanel.Children.Add(_scorePanel);

      _nameInput.Focus();
    }

    /// <summary>
    /// Получает введенное имя игрока
    /// </summary>
    /// <returns>Имя игрока</returns>
    public string GetPlayerName()
    {
      return _nameInput.Text;
    }
  }
}


