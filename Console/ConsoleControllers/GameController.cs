
using Base.Controllers;
using Consolee.ConsoleModels;
using Consolee.ConsoleViews;
using System.Collections.Concurrent;

namespace Consolee.ConsoleControllers
{
  /// <summary>
  /// Консольный контроллер игры
  /// </summary>
  public class GameController : GameControllerBase
  {
    /// <summary>
    /// Очередь для хранения нажатых клавиш
    /// </summary>
    private readonly ConcurrentQueue<ConsoleKey> _keyPresses = new();

    /// <summary>
    /// Поток для обработки ввода с клавиатуры
    /// </summary>
    private Thread? _inputThread;

    /// <summary>
    /// Флаг, указывающий, что поток ввода активен
    /// </summary>
    private volatile bool _inputThreadRunning;

    /// <summary>
    /// Флаг паузы игры
    /// </summary>
    private bool _isPaused;

    /// <summary>
    /// Сигнал для ожидания ввода
    /// </summary>
    private readonly ManualResetEventSlim _inputReady = new(false);

    /// <summary>
    /// Инициализирует новый экземпляр консольного контроллера игры
    /// </summary>
    /// <param name="parHighScoresController">Контроллер таблицы рекордов</param>
    public GameController(IHighScoresController parHighScoresController)
        : base(new GameModel(), new ConsoleView(), parHighScoresController)
    {
    }

    /// <summary>
    /// Запускает игру
    /// </summary>
    public override void StartGame()
    {
      Console.CursorVisible = false;  // Инициализируем консоль
      Console.Clear();

      _inputThreadRunning = true;
      StartInputThread();
      base.StartGame();
    }

    /// <summary>
    /// Запускает поток обработки ввода
    /// </summary>
    private void StartInputThread()
    {
      if (_inputThread != null && _inputThread.IsAlive)
      {
        _inputThread.Join();
      }

      while (_keyPresses.TryDequeue(out _)) { }

      _inputThread = new Thread(() =>
      {
        while (_inputThreadRunning && _isRunning)
        {
          try
          {
            if (Console.KeyAvailable)
            {
              var key = Console.ReadKey(true);
              _keyPresses.Enqueue(key.Key);
            }
          }
          catch (InvalidOperationException)
          {
            continue;
          }
        }
      });

      _inputThread.IsBackground = true;
      _inputThread.Start();
    }

    /// <summary>
    /// Выполняет игровой цикл
    /// </summary>
    public override void GameLoop()
    {
      const float timeStep = 1.0f / 60.0f;
      float accumulator = 0.0f;

      while (_isRunning)
      {
        var currentTime = GetCurrentTime();
        var deltaTime = (float)(currentTime - _lastTime).TotalSeconds;
        _lastTime = currentTime;

        accumulator += deltaTime;

        ProcessInput();

        while (accumulator >= timeStep)
        {
          if (!_isPaused)  // Проверяем паузу здесь
          {
            _model.Update(timeStep);
            if (_model.CheckCollisions())
            {
              GameOver();
            }
          }
          accumulator -= timeStep;
        }
        RenderGame();
      }
    }

    /// <summary>
    /// Обрабатывает пользовательский ввод
    /// </summary>
    private void ProcessInput()
    {
      while (_keyPresses.TryDequeue(out ConsoleKey key))
      {
        if (key == ConsoleKey.P)
        {
          TogglePause();
          continue;
        }

        if (key == ConsoleKey.Escape && _isPaused)
        {
          _isRunning = false;
          new MenuController().Start();
          continue;
        }

        if (_isPaused) continue;

        if (key == ConsoleKey.Spacebar)
        {
          (_model as GameModel)?.Jump();
        }
        else if (key == ConsoleKey.Escape)
        {
          _isRunning = false;
          new MenuController().Start();
        }
        else if (key == ConsoleKey.UpArrow)
        {
          (_model as GameModel)?.Shoot();
        }
      }
    }

    /// <summary>
    /// Обрабочик паузы
    /// </summary>
    private void TogglePause()
    {
      _isPaused = !_isPaused;

      if (_isPaused)
        (_view as ConsoleView)?.ShowPauseMenu();
      else
        (_view as ConsoleView)?.HidePauseMenu();
    }

    /// <summary>
    /// Отрисовывает игру
    /// </summary>
    private void RenderGame()
    {
      _view.Render(_model.GetGameState());
    }

    /// <summary>
    /// Завершает игру
    /// </summary>
    public override void GameOver()
    {
      _inputThreadRunning = false;
      _isRunning = false;
      _inputReady.Reset();

      if (_inputThread != null && _inputThread.IsAlive)
      {
        try
        {
          _inputThread.Join(1000);
        }
        catch (ThreadStateException)
        {
          // Игнорируем ошибку, если поток уже завершен
        }
      }

      base.GameOver();

      if (_model.GetGameState().Score == 0)
      {
        new MenuController().Start();
      }
    }

    /// <summary>
    /// Получает текущее время
    /// </summary>
    /// <returns>Текущее время</returns>
    protected override DateTime GetCurrentTime()
    {
      return DateTime.Now;
    }

    /// <summary>
    /// Обрабатывает новый рекорд
    /// </summary>
    /// <param name="parScore">Количество очков</param>
    protected override void HandleNewHighScore(int parScore)
    {
      var addHighScoreController = new AddHighScoreController(parScore);
      addHighScoreController.Show();
    }
  }
}

