namespace Base.Models.Obstacles
{
  /// <summary>
  /// Класс неподвижного препятствия
  /// </summary>
  public class StaticObstacle : BaseObstacle
  {
    /// <summary>
    /// Высота неподвижного препятствия
    /// </summary>
    public const int HEIGHT = 6;

    /// <summary>
    /// Позиция препятствия на игровом поле
    /// </summary>
    public ObstaclePosition Position { get; }

    /// <summary>
    /// Перечисление возможных позиций препятствия
    /// </summary>
    public enum ObstaclePosition
    {
      /// <summary>Верхняя позиция</summary>
      Top,
      /// <summary>Средняя позиция</summary>
      Middle,
      /// <summary>Нижняя позиция</summary>
      Bottom,
      /// <summary>Верхняя и нижняя позиции</summary>
      TopBottom,
      /// <summary>Верхняя и средняя позиции</summary>
      TopMiddle,
      /// <summary>Средняя и нижняя позиции</summary>
      MiddleBottom
    }

    /// <summary>
    /// Создает экземпляр неподвижного препятствия
    /// </summary>
    /// <param name="parX">Координата X начальной позиции</param>
    /// <param name="parPosition">Позиция препятствия</param>
    public StaticObstacle(float parX, ObstaclePosition parPosition)
        : base(parX, GetYPosition(parPosition), ObstacleTypes.Static)
    {
      Position = parPosition;
    }

    /// <summary>
    /// Возвращает координату Y для заданной позиции препятствия
    /// </summary>
    /// <param name="parPosition">Позиция препятствия</param>
    /// <returns>Координата Y для позиции</returns>
    private static float GetYPosition(ObstaclePosition parPosition) => parPosition switch
    {
      ObstaclePosition.Top => 0,
      ObstaclePosition.Middle => (20 - HEIGHT) / 2,
      ObstaclePosition.Bottom => 20 - HEIGHT,
      _ => 0
    };

    /// <summary>
    /// Проверяет, является ли заданная координата Y частью препятствия
    /// </summary>
    /// <param name="parY">Координата Y</param>
    /// <returns>True, если координата Y находится в пределах высоты препятствия, иначе False</returns>
    public override bool IsPartOfObstacle(float parY) => Position switch
    {
      ObstaclePosition.Top => parY < HEIGHT,
      ObstaclePosition.Middle => parY >= (20 - HEIGHT) / 2 && parY < (20 - HEIGHT) / 2 + HEIGHT,
      ObstaclePosition.Bottom => parY >= 20 - HEIGHT,
      ObstaclePosition.TopBottom => parY < HEIGHT || parY >= 20 - HEIGHT,
      ObstaclePosition.TopMiddle => parY < HEIGHT || (parY >= (20 - HEIGHT) / 2 && parY < (20 - HEIGHT) / 2 + HEIGHT),
      ObstaclePosition.MiddleBottom => (parY >= (20 - HEIGHT) / 2 && parY < (20 - HEIGHT) / 2 + HEIGHT) || parY >= 20 - HEIGHT,
      _ => false
    };
  }

}
