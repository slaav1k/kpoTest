using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Linq;

using Base.Views;
using Base.Models;

namespace Wpf.WpfViews
{
  /// <summary>
  /// WPF представление таблицы рекордов.
  /// </summary>
  public class WpfHighScoresView : HighScoresViewBase
  {
    /// <summary>
    /// Экземпляр экрана, на котором отображается таблица рекордов.
    /// </summary>
    private readonly MainScreen _screen;

    /// <summary>
    /// Панель, содержащая список рекордов.
    /// </summary>
    private readonly StackPanel _scoresPanel;

    /// <summary>
    /// Текстовый блок, отображающий заголовок таблицы рекордов.
    /// </summary>
    private readonly TextBlock _titleBlock;


    /// <summary>
    /// Инициализирует новый экземпляр WPF представления таблицы рекордов.
    /// </summary>
    public WpfHighScoresView()
    {
      _screen = MainScreen.GetInstance();

      _titleBlock = new TextBlock
      {
        Text = TITLE,
        FontSize = 48,
        FontFamily = new FontFamily("Comic Sans MS"),
        Foreground = Brushes.Yellow,
        HorizontalAlignment = HorizontalAlignment.Center,
        Margin = new Thickness(0, 20, 0, 40)
      };

      _scoresPanel = new StackPanel
      {
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center
      };
    }

    /// <summary>
    /// Отображает таблицу рекордов на экране.
    /// </summary>
    /// <remarks>
    /// Очищает текущие элементы на экране и добавляет заголовок и панель с рекордами.
    /// </remarks>
    public void Show()
    {
      _screen.StackPanel.Children.Clear();
      _screen.StackPanel.Children.Add(_titleBlock);
      _screen.StackPanel.Children.Add(_scoresPanel);
    }

    /// <summary>
    /// Отображает список рекордов на экране.
    /// </summary>
    /// <param name="parScores">Список рекордов для отображения.</param>
    /// <remarks>
    /// Если список рекордов пуст, отображается сообщение о его отсутствии.
    /// </remarks>
    public override void Render(IReadOnlyList<HighScoreRecord> parScores)
    {
      _scoresPanel.Children.Clear();

      if (parScores.Any())
      {
        // Добавляем рекорды в панель
        for (int i = 0; i < parScores.Count; i++)
        {
          var score = parScores[i];
          var scoreText = new TextBlock
          {
            Text = $"{i + 1}. {score.Name} - {score.Score}",
            FontSize = 20,
            FontFamily = new FontFamily("Comic Sans MS"),
            Foreground = Brushes.White,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(0, 5, 0, 5)
          };
          _scoresPanel.Children.Add(scoreText);
        }
      }
      else
      {
        // Если рекордов нет, отображаем соответствующее сообщение
        var noScoresText = new TextBlock
        {
          Text = NO_SCORES_MESSAGE,
          FontSize = 20,
          FontFamily = new FontFamily("Comic Sans MS"),
          Foreground = Brushes.White,
          HorizontalAlignment = HorizontalAlignment.Center,
          Margin = new Thickness(0, 5, 0, 5)
        };
        _scoresPanel.Children.Add(noScoresText);
      }

      // Добавляем кнопку для возвращения в меню
      var returnText = new TextBlock
      {
        Text = RETURN_MESSAGE,
        FontSize = 16,
        FontFamily = new FontFamily("Comic Sans MS"),
        Foreground = Brushes.Gray,
        HorizontalAlignment = HorizontalAlignment.Center,
        Margin = new Thickness(0, 15, 0, 0)
      };
      _scoresPanel.Children.Add(returnText);
    }

    /// <summary>
    /// Получает или задает экран, на котором отображается таблица рекордов.
    /// </summary>
    private MainScreen Screen => _screen;

    /// <summary>
    /// Получает или задает панель, на которой отображаются рекорды.
    /// </summary>
    private StackPanel ScoresPanel => _scoresPanel;

    /// <summary>
    /// Получает или задает текстовый блок, отображающий заголовок таблицы рекордов.
    /// </summary>
    private TextBlock TitleBlock => _titleBlock;
  }
}


