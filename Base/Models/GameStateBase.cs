using Base.Models.Obstacles;

namespace Base.Models
{
  /// <summary>
  /// Базовый класс состояния игры.
  /// Содержит информацию о позиции игрока, его скорости, счете и препятствиях.
  /// </summary>
  public abstract class GameStateBase
  {
    /// <summary>
    /// Позиция игрока по оси X.
    /// </summary>
    public float PlayerX { get; set; }

    /// <summary>
    /// Позиция игрока по оси Y.
    /// </summary>
    public float PlayerY { get; set; }

    /// <summary>
    /// Скорость движения игрока.
    /// </summary>
    public float PlayerVelocity { get; set; }

    /// <summary>
    /// Текущий счет игрока.
    /// </summary>
    public int Score { get; set; }

    /// <summary>
    /// Список всех препятствий на игровом поле.
    /// </summary>
    public List<BaseObstacle> Obstacles { get; }

    /// <summary>
    /// Конструктор класса GameStateBase. Инициализирует список препятствий и вызывает метод сброса состояния.
    /// </summary>
    protected GameStateBase()
    {
      Obstacles = new List<BaseObstacle>();
      Reset();
    }

    /// <summary>
    /// Сбрасывает состояние игры к начальным значениям.
    /// Включает сброс позиций игрока, его скорости, счета и очистку списка препятствий.
    /// </summary>
    public virtual void Reset()
    {
      PlayerX = 20;
      PlayerY = 10;
      PlayerVelocity = 0;
      Score = 0;
      Obstacles.Clear();
    }
  }

}

