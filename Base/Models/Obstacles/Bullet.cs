namespace Base.Models.Obstacles
{
  /// <summary>
  /// Класс патрона
  /// </summary>
  public class Bullet : BaseObstacle
  {
    /// <summary>
    /// Высота патрона
    /// </summary>
    public const int HEIGHT = 1;

    /// <summary>
    /// Скорость патрона
    /// </summary>
    private const float SPEED = 30.0f;

    /// <summary>
    /// Угол движения патрона в радианах
    /// </summary>
    private readonly float _angle;

    /// <summary>
    /// Создает экземпляр патрона
    /// </summary>
    /// <param name="parX">Координата X начальной позиции</param>
    /// <param name="parY">Координата Y начальной позиции</param>
    /// <param name="parAngleInDegrees">Угол движения в градусах</param>
    public Bullet(float parX, float parY, float parAngleInDegrees)
        : base(parX, parY, ObstacleTypes.Bullet)
    {
      _angle = (float)(parAngleInDegrees * Math.PI / 180);
    }

    /// <summary>
    /// Обновляет положение патрона
    /// </summary>
    /// <param name="parDeltaTime">Время, прошедшее с последнего обновления</param>
    public override void Update(float parDeltaTime)
    {
      X -= SPEED * parDeltaTime * (float)Math.Cos(_angle);
      Y += SPEED * parDeltaTime * (float)Math.Sin(_angle);
    }

    /// <summary>
    /// Проверяет, является ли заданная координата Y частью патрона
    /// </summary>
    /// <param name="parY">Координата Y</param>
    /// <returns>True, если координата Y близка к позиции патрона, иначе False</returns>
    public override bool IsPartOfObstacle(float parY) =>
        Math.Abs(parY - Y) < 0.5f;
  }

}
