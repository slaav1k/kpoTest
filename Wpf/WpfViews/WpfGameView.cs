using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Media.Imaging;
using System.Linq;
using System.Globalization;

using Base.Models.Obstacles;
using Base.Models;
using Base.Views;

namespace Wpf.WpfViews
{
  /// <summary>
  /// WPF представление игры
  /// </summary>
  public class WpfGameView : GameViewBase
  {
    /// <summary>
    /// Ширина игрового окна
    /// </summary>
    public const double WINDOW_WIDTH = 800;

    /// <summary>
    /// Высота игрового окна
    /// </summary>
    public const double WINDOW_HEIGHT = 440;

    /// <summary>
    /// Ширина канваса, на котором будет отображаться игра
    /// </summary>
    private const double CANVAS_WIDTH = 800;

    /// <summary>
    /// Высота канваса, на котором будет отображаться игра
    /// </summary>
    private const double CANVAS_HEIGHT = 440;

    /// <summary>
    /// Размер игрока в пикселях
    /// </summary>
    private const double PLAYER_SIZE = 40;

    /// <summary>
    /// Размер пули в пикселях
    /// </summary>
    private const double BULLET_SIZE = 10;

    /// <summary>
    /// Размер врага в пикселях
    /// </summary>
    private const double ENEMY_SIZE = 30;

    /// <summary>
    /// Размер монеты в пикселях
    /// </summary>
    private const double COIN_SIZE = 20;

    /// <summary>
    /// Высота препятствий (занимает треть высоты игрового экрана)
    /// </summary>
    private const double OBSTACLE_HEIGHT = WINDOW_HEIGHT / 3;

    /// <summary>
    /// Канвас для отрисовки игрового процесса
    /// </summary>
    private readonly Canvas _canvas;

    /// <summary>
    /// Изображение игрока на игровом поле
    /// </summary>
    private readonly Image _player;

    /// <summary>
    /// Изображение хвоста игрока (если это необходимо в игре)
    /// </summary>
    private readonly Image _playerTail;

    /// <summary>
    /// Текстовый блок для отображения текущего счета игры
    /// </summary>
    private readonly TextBlock _scoreText;

    /// <summary>
    /// Список всех препятствий, которые должны быть отображены
    /// на игровом поле в процессе игры
    /// </summary>
    private readonly List<UIElement> _obstacles = new();

    /// <summary>
    /// Экземпляр главного экрана игры, используемый для
    /// управления отображением различных видов экранов
    /// </summary>
    private readonly MainScreen _screen;

    /// <summary>
    /// Инициализирует новый экземпляр WPF представления игры
    /// </summary>
    public WpfGameView()
    {
      _screen = MainScreen.GetInstance();
      _canvas = new Canvas
      {
        Width = CANVAS_WIDTH,
        Height = CANVAS_HEIGHT,
        Background = Brushes.Black
      };

      _player = new Image
      {
        Width = PLAYER_SIZE,
        Height = PLAYER_SIZE,
        Source = LoadImage("Resources/player.png"),
        Stretch = Stretch.Fill,  // Изменяем режим растяжения
        RenderTransformOrigin = new Point(0.5, 0.5)  // Устанавливаем центр трансформации
      };

      _playerTail = new Image
      {
        Width = PLAYER_SIZE,
        Height = PLAYER_SIZE,
        Source = LoadImage("Resources/player_tail.png"),
        Stretch = Stretch.Fill,  // Изменяем режим растяжения
        RenderTransformOrigin = new Point(0.5, 0.5)  // Устанавливаем центр трансформации
      };

      _scoreText = new TextBlock
      {
        Foreground = Brushes.White,
        FontSize = 24
      };

      _canvas.Children.Add(_player);
      _canvas.Children.Add(_playerTail);
      _canvas.Children.Add(_scoreText);
    }

    /// <summary>
    /// Отображает игровое окно
    /// </summary>
    public void Show()
    {
      _screen.StackPanel.Children.Clear();
      _canvas.Children.Clear();
      _obstacles.Clear();

      // Добавляем основные элементы
      _canvas.Children.Add(_player);
      _canvas.Children.Add(_playerTail);
      _canvas.Children.Add(_scoreText);

      _screen.StackPanel.Children.Add(_canvas);

      // Сбрасываем позиции всех элементов
      Canvas.SetLeft(_player, 0);
      Canvas.SetTop(_player, 0);
      Canvas.SetLeft(_playerTail, 0);
      Canvas.SetTop(_playerTail, 0);
      Canvas.SetLeft(_scoreText, 20);
      Canvas.SetTop(_scoreText, 20);

      _screen.Window.Width = WINDOW_WIDTH;
      _screen.Window.Height = WINDOW_HEIGHT;
      _screen.Window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
    }

    /// <summary>
    /// Отрисовывает текущее состояние игры
    /// </summary>
    /// <param name="parGameState">Состояние игры</param>
    public override void Render(GameStateBase parGameState)
    {
      Application.Current.Dispatcher.Invoke(() =>
      {
        float clampedY = Math.Clamp(parGameState.PlayerY, 2, 18);

        // Сначала отрисовываем хвост немного позади игрока
        Canvas.SetLeft(_playerTail, (parGameState.PlayerX * 10 - PLAYER_SIZE / 2) - 10);
        Canvas.SetTop(_playerTail, clampedY * (WINDOW_HEIGHT / 20) - PLAYER_SIZE / 2);

        // Затем отрисовываем самого игрока
        Canvas.SetLeft(_player, parGameState.PlayerX * 10 - PLAYER_SIZE / 2);
        Canvas.SetTop(_player, clampedY * (WINDOW_HEIGHT / 20) - PLAYER_SIZE / 2);

        // Очищаем все препятствия перед обновлением
        _canvas.Children.Clear();
        _obstacles.Clear();

        // Добавляем обратно основные элементы
        _canvas.Children.Add(_player);
        _canvas.Children.Add(_playerTail);
        _canvas.Children.Add(_scoreText);

        foreach (var elObstacle in parGameState.Obstacles)
        {
          var shape = CreateObstacle(elObstacle);
          _obstacles.Add(shape);
          _canvas.Children.Add(shape);

          Canvas.SetLeft(shape, elObstacle.X * 10);

          if (elObstacle is StaticObstacle staticObstacle)
          {
            double topPosition = 0;
            switch (staticObstacle.Position)
            {
              case StaticObstacle.ObstaclePosition.Top:
                topPosition = 0;
                break;
              case StaticObstacle.ObstaclePosition.Middle:
                topPosition = (WINDOW_HEIGHT - OBSTACLE_HEIGHT) / 2;
                break;
              case StaticObstacle.ObstaclePosition.Bottom:
                topPosition = WINDOW_HEIGHT - OBSTACLE_HEIGHT;
                break;
              case StaticObstacle.ObstaclePosition.TopBottom:
              case StaticObstacle.ObstaclePosition.TopMiddle:
              case StaticObstacle.ObstaclePosition.MiddleBottom:
                topPosition = (WINDOW_HEIGHT - OBSTACLE_HEIGHT * 3) / 2;
                break;
            }
            Canvas.SetTop(shape, topPosition);
          }
          else
          {
            float obstacleY = elObstacle.Type == BaseObstacle.ObstacleTypes.PlayerBullet
                ? Math.Clamp(elObstacle.Y, 2, 18)
                : elObstacle.Y;
            double topPosition = obstacleY * (WINDOW_HEIGHT / 20);
            Canvas.SetTop(shape, topPosition);
          }
        }

        // Обновляем счет и его позицию в правом верхнем углу
        _scoreText.Text = $"Score: {parGameState.Score}";
        Canvas.SetLeft(_scoreText, CANVAS_WIDTH - 150); // Увеличиваем отступ справа
        Canvas.SetTop(_scoreText, 20);  // Отступ сверху

        _canvas.InvalidateVisual();
      });
    }
    /// <summary>
    /// Создает препятствие
    /// </summary>
    /// <param name="parObstacle"></param>
    /// <returns></returns>
    private UIElement CreateObstacle(BaseObstacle parObstacle)
    {
      if (parObstacle is StaticObstacle staticObstacle)
      {
        switch (staticObstacle.Position)
        {
          case StaticObstacle.ObstaclePosition.TopBottom:
            return CreateDoubleObstacle(true, true);
          case StaticObstacle.ObstaclePosition.TopMiddle:
            return CreateDoubleObstacle(true, false);
          case StaticObstacle.ObstaclePosition.MiddleBottom:
            return CreateDoubleObstacle(false, true);
          default:
            return new Image
            {
              Width = PLAYER_SIZE,
              Height = OBSTACLE_HEIGHT,
              Source = LoadImage("Resources/obstacle.png")
            };
        }
      }

      return parObstacle.Type switch
      {
        BaseObstacle.ObstacleTypes.Coin => new Image
        {
          Width = COIN_SIZE,
          Height = COIN_SIZE,
          Source = LoadImage("Resources/coin.png")
        },
        BaseObstacle.ObstacleTypes.Enemy => new Image
        {
          Width = ENEMY_SIZE,
          Height = ENEMY_SIZE,
          Source = LoadImage("Resources/enemy.png")
        },
        BaseObstacle.ObstacleTypes.Bullet => new Image
        {
          Width = BULLET_SIZE,
          Height = BULLET_SIZE,
          Source = LoadImage("Resources/bullet.png")
        },
        BaseObstacle.ObstacleTypes.PlayerBullet => new Image
        {
          Width = BULLET_SIZE * 1.5,
          Height = BULLET_SIZE / 2,
          Source = LoadImage("Resources/player_bullet.png"),
          RenderTransformOrigin = new Point(0.5, 0.5)
        },
        _ => new Image
        {
          Width = PLAYER_SIZE,
          Height = OBSTACLE_HEIGHT,
          Source = LoadImage("Resources/obstacle.png")
        }
      };
    }

    /// <summary>
    /// Создает двойное препятствие
    /// </summary>
    /// <param name="parHasTop"></param>
    /// <param name="parHasBottom"></param>
    /// <returns></returns>
    private Canvas CreateDoubleObstacle(bool parHasTop, bool parHasBottom)
    {
      var container = new Canvas
      {
        Width = PLAYER_SIZE,
        Height = OBSTACLE_HEIGHT * 3
      };

      if (parHasTop)
      {
        var top = new Image
        {
          Width = PLAYER_SIZE,
          Height = OBSTACLE_HEIGHT,
          Source = LoadImage("Resources/obstacle.png")
        };
        Canvas.SetTop(top, 0);
        container.Children.Add(top);
      }

      if (parHasTop != parHasBottom)
      {
        var middle = new Image
        {
          Width = PLAYER_SIZE,
          Height = OBSTACLE_HEIGHT,
          Source = LoadImage("Resources/obstacle.png")
        };
        Canvas.SetTop(middle, OBSTACLE_HEIGHT);
        container.Children.Add(middle);
      }

      if (parHasBottom)
      {
        var bottom = new Image
        {
          Width = PLAYER_SIZE,
          Height = OBSTACLE_HEIGHT,
          Source = LoadImage("Resources/obstacle.png")
        };
        Canvas.SetTop(bottom, OBSTACLE_HEIGHT * 2);
        container.Children.Add(bottom);
      }

      return container;
    }

    /// <summary>
    /// Отображает экран окончания игры
    /// </summary>
    /// <param name="parFinalScore">Финальный счет</param>
    public override void ShowGameOver(int parFinalScore)
    {
      Application.Current.Dispatcher.Invoke(() =>
      {
        _canvas.Children.Clear();

        var gameOverText = new TextBlock
        {
          Text = "GAME OVER",
          FontSize = 48,
          Foreground = Brushes.Red,
          TextAlignment = TextAlignment.Center
        };
        Canvas.SetLeft(gameOverText, (CANVAS_WIDTH - 300) / 2);
        Canvas.SetTop(gameOverText, CANVAS_HEIGHT / 2 - 50);
        _canvas.Children.Add(gameOverText);

        var scoreText = new TextBlock
        {
          Text = $"Ваш счет: {parFinalScore}",
          FontSize = 24,
          Foreground = Brushes.White,
          TextAlignment = TextAlignment.Center
        };
        Canvas.SetLeft(scoreText, (CANVAS_WIDTH - 200) / 2);
        Canvas.SetTop(scoreText, CANVAS_HEIGHT / 2 + 20);
        _canvas.Children.Add(scoreText);

        var continueText = new TextBlock
        {
          Text = "Press Q to continue",
          FontSize = 20,
          Foreground = Brushes.White,
          TextAlignment = TextAlignment.Center
        };
        Canvas.SetLeft(continueText, (CANVAS_WIDTH - 200) / 2 - 10);
        Canvas.SetTop(continueText, CANVAS_HEIGHT / 2 + 70);
        _canvas.Children.Add(continueText);
      });
    }

    /// <summary>
    /// Добавляет вспомогательный метод для загрузки изображений
    /// </summary>
    /// <param name="path">Путь к изображению</param>
    /// <returns>Загруженное изображение</returns>
    private BitmapImage LoadImage(string path)
    {
      var image = new BitmapImage();
      try
      {
        image.BeginInit();
        image.UriSource = new Uri($"pack://application:,,,/wpf;component/{path}");
        image.CacheOption = BitmapCacheOption.OnLoad;
        image.EndInit();
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Error loading image {path}: {ex.Message}");
      }
      return image;
    }

    /// <summary>
    /// Отображает или скрывает меню паузы на экране.
    /// </summary>
    /// <param name="parShow">
    /// Если значение true, отображается меню паузы. Если значение false, меню паузы скрывается.
    /// </param>
    public void ShowPauseMenu(bool parShow)
    {
      if (parShow)
      {
        var pausePanel = new StackPanel
        {
          VerticalAlignment = VerticalAlignment.Center,
          HorizontalAlignment = HorizontalAlignment.Center
        };

        var pauseText = new TextBlock
        {
          Text = "ПАУЗА",
          TextAlignment = TextAlignment.Center,
          FontSize = 20,
          Foreground = Brushes.White,
          Margin = new Thickness(0, 0, 0, 20)
        };

        var continueText = new TextBlock
        {
          Text = "Продолжить - P",
          TextAlignment = TextAlignment.Center,
          Foreground = Brushes.White,
          Margin = new Thickness(0, 0, 0, 10)
        };

        var menuText = new TextBlock
        {
          Text = "В меню - ESC",
          TextAlignment = TextAlignment.Center,
          Foreground = Brushes.White
        };

        pausePanel.Children.Add(pauseText);
        pausePanel.Children.Add(continueText);
        pausePanel.Children.Add(menuText);

        // Устанавливаем позицию панели в центре
        Canvas.SetLeft(pausePanel, (_canvas.Width - 200) / 2);
        Canvas.SetTop(pausePanel, (_canvas.Height - 150) / 2);

        _canvas.Children.Add(pausePanel);
      }
      else
      {
        var pausePanel = _canvas.Children.OfType<StackPanel>().FirstOrDefault();
        if (pausePanel != null)
        {
          _canvas.Children.Remove(pausePanel);
        }
      }
    }
  }
}

