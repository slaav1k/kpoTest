namespace Base.Models.Obstacles
{
  /// <summary>
  /// Класс движущегося препятствия
  /// </summary>
  public class MovingObstacle : BaseObstacle
  {
    /// <summary>
    /// Высота препятствия
    /// </summary>
    public const int HEIGHT = 6;

    /// <summary>
    /// Скорость вертикального движения препятствия
    /// </summary>
    private const float VERTICAL_SPEED = 10.0f;

    /// <summary>
    /// Указывает, движется ли препятствие вниз
    /// </summary>
    private bool _movingDown = true;

    /// <summary>
    /// Создает экземпляр движущегося препятствия
    /// </summary>
    /// <param name="parX">Координата X начальной позиции</param>
    /// <param name="parY">Координата Y начальной позиции</param>
    public MovingObstacle(float parX, float parY)
        : base(parX, parY, ObstacleTypes.Moving) { }

    /// <summary>
    /// Обновляет положение препятствия
    /// </summary>
    /// <param name="parDeltaTime">Время, прошедшее с последнего обновления</param>
    public override void Update(float parDeltaTime)
    {
      if (_movingDown)
      {
        Y += VERTICAL_SPEED * parDeltaTime;
        if (Y >= 20 - HEIGHT)
        {
          Y = 20 - HEIGHT;
          _movingDown = false;
        }
      }
      else
      {
        Y -= VERTICAL_SPEED * parDeltaTime;
        if (Y <= 0)
        {
          Y = 0;
          _movingDown = true;
        }
      }
    }

    /// <summary>
    /// Проверяет, является ли заданная координата Y частью препятствия
    /// </summary>
    /// <param name="parY">Координата Y</param>
    /// <returns>True, если координата Y находится в пределах высоты препятствия, иначе False</returns>
    public override bool IsPartOfObstacle(float parY) =>
        parY >= Y && parY < Y + HEIGHT;
  }

}
