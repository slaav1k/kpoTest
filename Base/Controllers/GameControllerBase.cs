using Base.Models;
using Base.Views;

namespace Base.Controllers
{
  /// <summary>
  /// Базовый класс контроллера игры
  /// </summary>
  public abstract class GameControllerBase : IGameController
  {
    /// <summary>
    /// Модель игры, содержащая игровую логику и состояние
    /// </summary>
    protected readonly GameModelBase _model;

    /// <summary>
    /// Представление игры, отвечающее за отображение
    /// </summary>
    protected readonly GameViewBase _view;

    /// <summary>
    /// Контроллер таблицы рекордов для управления результатами игры
    /// </summary>
    protected readonly IHighScoresController _highScoresController;

    /// <summary>
    /// Флаг, указывающий, выполняется ли игра
    /// </summary>
    protected bool _isRunning;

    /// <summary>
    /// Временная отметка последнего обновления игрового цикла
    /// </summary>
    protected DateTime _lastTime;

    /// <summary>
    /// Инициализирует новый экземпляр базового контроллера игры
    /// </summary>
    /// <param name="parModel">Модель игры</param>
    /// <param name="parView">Представление игры</param>
    /// <param name="parHighScoresController">Контроллер таблицы рекордов</param>
    protected GameControllerBase(GameModelBase parModel, GameViewBase parView, IHighScoresController parHighScoresController)
    {
      _model = parModel;
      _view = parView;
      _highScoresController = parHighScoresController;
    }

    /// <summary>
    /// Запускает игру
    /// </summary>
    public virtual void StartGame()
    {
      _model.Reset();
      _isRunning = true;
      _lastTime = GetCurrentTime();
      GameLoop();
    }

    /// <summary>
    /// Выполняет игровой цикл
    /// </summary>
    public abstract void GameLoop();

    /// <summary>
    /// Завершает игру
    /// </summary>
    public virtual void GameOver()
    {
      _isRunning = false;
      _view.ShowGameOver(_model.Score);

      if (_highScoresController.IsNewHighScore(_model.Score))
      {
        HandleNewHighScore(_model.Score);
      }
    }

    /// <summary>
    /// Получает текущее время
    /// </summary>
    /// <returns>Текущее время</returns>
    protected abstract DateTime GetCurrentTime();

    /// <summary>
    /// Обрабатывает новый рекорд
    /// </summary>
    /// <param name="parScore">Количество очков</param>
    protected abstract void HandleNewHighScore(int parScore);
  }
}
