namespace Base.Models.Obstacles
{
  /// <summary>
  /// Базовый класс препятствия
  /// </summary>
  public abstract class BaseObstacle
  {
    /// <summary>
    /// Координата X препятствия
    /// </summary>
    public float X { get; protected set; }

    /// <summary>
    /// Координата Y препятствия
    /// </summary>
    public float Y { get; protected set; }

    /// <summary>
    /// Тип препятствия
    /// </summary>
    public ObstacleTypes Type { get; }

    /// <summary>
    /// Перечисление возможных типов препятствий
    /// </summary>
    public enum ObstacleTypes
    {
      /// <summary>
      /// Обычное статическое препятствие
      /// </summary>
      Static,

      /// <summary>
      /// Движущееся препятствие
      /// </summary>
      Moving,

      /// <summary>
      /// Монетка
      /// </summary>
      Coin,

      /// <summary>
      /// Враг
      /// </summary>
      Enemy,

      /// <summary>
      /// Пуля врага
      /// </summary>
      Bullet,

      /// <summary>
      /// Пуля игрока
      /// </summary>
      PlayerBullet
    }

    /// <summary>
    /// Создает экземпляр препятствия
    /// </summary>
    /// <param name="parX">Координата X</param>
    /// <param name="parY">Координата Y</param>
    /// <param name="parType">Тип препятствия</param>
    protected BaseObstacle(float parX, float parY, ObstacleTypes parType)
    {
      X = parX;
      Y = parY;
      Type = parType;
    }

    /// <summary>
    /// Обновляет состояние препятствия
    /// </summary>
    /// <param name="deltaTime">Время, прошедшее с последнего обновления</param>
    public virtual void Update(float deltaTime) { }

    /// <summary>
    /// Проверяет, является ли заданная координата Y частью препятствия
    /// </summary>
    /// <param name="y">Координата Y</param>
    /// <returns>True, если координата является частью препятствия, иначе False</returns>
    public abstract bool IsPartOfObstacle(float y);

    /// <summary>
    /// Устанавливает новое значение координаты X
    /// </summary>
    /// <param name="x">Новое значение координаты X</param>
    public void SetX(float x) => X = x;
  }

}

