namespace Base.Models.Obstacles
{
  /// <summary>
  /// Класс патрона игрока
  /// </summary>
  public class PlayerBullet : BaseObstacle
  {
    /// <summary>
    /// Высота патрона игрока
    /// </summary>
    public const int HEIGHT = 1;

    /// <summary>
    /// Скорость патрона игрока
    /// </summary>
    public const float SPEED = 40.0f;

    /// <summary>
    /// Создает экземпляр патрона игрока
    /// </summary>
    /// <param name="parX">Координата X начальной позиции</param>
    /// <param name="parY">Координата Y начальной позиции</param>
    public PlayerBullet(float parX, float parY)
        : base(parX, parY, ObstacleTypes.PlayerBullet)
    {
    }

    /// <summary>
    /// Обновляет состояние патрона игрока
    /// </summary>
    /// <param name="parDeltaTime">Время, прошедшее с последнего обновления</param>
    public override void Update(float parDeltaTime) { }

    /// <summary>
    /// Проверяет, является ли заданная координата Y частью патрона игрока
    /// </summary>
    /// <param name="parY">Координата Y</param>
    /// <returns>True, если координата Y находится в пределах высоты патрона, иначе False</returns>
    public override bool IsPartOfObstacle(float parY) =>
        Math.Abs(parY - Y) < 0.5f;
  }

}
