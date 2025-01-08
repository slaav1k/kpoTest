namespace Base.Models.Obstacles
{
  /// <summary>
  /// Класс монетки
  /// </summary>
  public class Coin : BaseObstacle
  {
    /// <summary>
    /// Высота монетки
    /// </summary>
    public const int HEIGHT = 20 / 6;

    /// <summary>
    /// Создает экземпляр монетки
    /// </summary>
    /// <param name="parX">Координата X начальной позиции</param>
    /// <param name="parY">Координата Y начальной позиции</param>
    public Coin(float parX, float parY)
        : base(parX, parY, ObstacleTypes.Coin) { }

    /// <summary>
    /// Проверяет, является ли заданная координата Y частью монетки
    /// </summary>
    /// <param name="parY">Координата Y</param>
    /// <returns>True, если координата Y находится в пределах высоты монетки, иначе False</returns>
    public override bool IsPartOfObstacle(float parY) =>
        parY >= Y && parY < Y + HEIGHT;
  }

}
