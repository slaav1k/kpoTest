using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

using Base.Controllers;
using Wpf.WpfViews;
using Wpf.WpfModels;


namespace Wpf.WpfControllers
{
  /// <summary>
  /// WPF-контроллер игры.
  /// </summary>
  public class WpfGameController : GameControllerBase
  {
    /// <summary>
    /// Представление игры.
    /// </summary>
    private readonly WpfGameView _view;

    /// <summary>
    /// Указывает, находится ли игра в состоянии паузы.
    /// </summary>
    private bool _isPaused;

    /// <summary>
    /// Главное окно приложения.
    /// </summary>
    private readonly Window _mainWindow;

    /// <summary>
    /// Инициализирует новый экземпляр WPF-контроллера игры.
    /// </summary>
    /// <param name="parView">Представление игры.</param>
    public WpfGameController(WpfGameView parView)
        : base(new WpfGameModel(), parView, new WpfHighScoresController(new WpfHighScoresView()))
    {
      _view = parView;
      _mainWindow = MainScreen.GetInstance().Window;
    }

    /// <summary>
    /// Запускает игру.
    /// </summary>
    public override void StartGame()
    {
      _view.Show();
      _isRunning = true;
      _lastTime = GetCurrentTime();
      CompositionTarget.Rendering += GameLoop_Update;
      _model.Reset();
      _mainWindow.KeyDown += Window_KeyDown;
    }

    /// <summary>
    /// Завершает игру.
    /// </summary>
    public override void GameOver()
    {
      if (_isPaused)
      {
        ClosePauseMenu();
      }

      _isRunning = false;
      _mainWindow.KeyDown -= Window_KeyDown;
      _mainWindow.KeyDown += GameOverKeyDown;
      _view.ShowGameOver(_model.GetGameState().Score);
    }

    /// <summary>
    /// Обрабатывает нажатие клавиш в окне.
    /// </summary>
    /// <param name="parSender">Источник события.</param>
    /// <param name="parE">Аргументы события нажатия клавиши.</param>
    private void Window_KeyDown(object parSender, KeyEventArgs parE)
    {
      if (!_isRunning)
      {
        if (parE.Key == Key.Q)
        {
          if (_model.GetGameState().Score > 0)
          {
            var addHighScoreController = new WpfAddHighScoreController(_model.GetGameState().Score);
            addHighScoreController.Show();
          }
          else
          {
            CompositionTarget.Rendering -= GameLoop_Update;
            new WpfMenuController().Start();
          }
        }
        return;
      }

      if (parE.Key == Key.P)
      {
        TogglePause();
        return;
      }

      if (parE.Key == Key.Escape && _isPaused)
      {
        ExitToMenu();
        return;
      }

      if (_isPaused) return;

      if (parE.Key == Key.Space)
      {
        (_model as WpfGameModel)?.Jump();
      }
      else if (parE.Key == Key.Escape)
      {
        ExitToMenu();
      }
      else if (parE.Key == Key.Up)
      {
        (_model as WpfGameModel)?.Shoot();
      }
    }

    /// <summary>
    /// Переключает состояние паузы.
    /// </summary>
    private void TogglePause()
    {
      _isPaused = !_isPaused;
      if (_isPaused)
      {
        CompositionTarget.Rendering -= GameLoop_Update;
      }
      else
      {
        _lastTime = GetCurrentTime();
        CompositionTarget.Rendering += GameLoop_Update;
      }
        (_view as WpfGameView)?.ShowPauseMenu(_isPaused);
    }

    /// <summary>
    /// Закрывает меню паузы.
    /// </summary>
    private void ClosePauseMenu()
    {
      (_view as WpfGameView)?.ShowPauseMenu(false);
    }

    /// <summary>
    /// Обновляет состояние игры при отрисовке кадра.
    /// </summary>
    /// <param name="parSender">Источник события.</param>
    /// <param name="parE">Аргументы события.</param>
    private void GameLoop_Update(object parSender, EventArgs parE)
    {
      if (!_isRunning) return;

      var currentTime = GetCurrentTime();
      float deltaTime = (float)(currentTime - _lastTime).TotalSeconds;
      _lastTime = currentTime;

      _model.Update(deltaTime);
      if (_model.CheckCollisions())
      {
        GameOver();
        return;
      }

      _view.Render(_model.GetGameState());
    }

    /// <summary>
    /// Получает текущее время.
    /// </summary>
    /// <returns>Текущее время.</returns>
    protected override DateTime GetCurrentTime()
    {
      return DateTime.Now;
    }

    /// <summary>
    /// Выполняет игровой цикл.
    /// </summary>
    public override void GameLoop()
    {
      // Игровой цикл управляется через CompositionTarget.Rendering.
    }

    /// <summary>
    /// Обрабатывает новый рекорд.
    /// </summary>
    /// <param name="parScore">Количество очков.</param>
    protected override void HandleNewHighScore(int parScore)
    {
      var addHighScoreController = new WpfAddHighScoreController(parScore);
      addHighScoreController.Show();
    }

    /// <summary>
    /// Выход в меню.
    /// </summary>
    private void ExitToMenu()
    {
      _isRunning = false;
      CompositionTarget.Rendering -= GameLoop_Update;
      _mainWindow.KeyDown -= Window_KeyDown;
      _model.Reset();
      new WpfMenuController().Start();
    }

    /// <summary>
    /// Останавливает игру.
    /// </summary>
    public void Stop()
    {
      _isRunning = false;
      _mainWindow.KeyDown -= Window_KeyDown;
    }

    /// <summary>
    /// Обрабатывает событие завершения игры по клавише.
    /// </summary>
    /// <param name="parSender">Источник события.</param>
    /// <param name="parE">Аргументы события нажатия клавиши.</param>
    private void GameOverKeyDown(object parSender, KeyEventArgs parE)
    {
      if (parE.Key == Key.Q)
      {
        _mainWindow.KeyDown -= GameOverKeyDown;
        CompositionTarget.Rendering -= GameLoop_Update;

        if (_highScoresController.IsNewHighScore(_model.GetGameState().Score))
        {
          HandleNewHighScore(_model.GetGameState().Score);
        }
        else
        {
          new WpfMenuController().Start();
        }
      }
    }
  }

}

