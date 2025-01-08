namespace Base.Models.Obstacles
{
  /// <summary>
  /// ����� ������������ �����������
  /// </summary>
  public class StaticObstacle : BaseObstacle
  {
    /// <summary>
    /// ������ ������������ �����������
    /// </summary>
    public const int HEIGHT = 6;

    /// <summary>
    /// ������� ����������� �� ������� ����
    /// </summary>
    public ObstaclePosition Position { get; }

    /// <summary>
    /// ������������ ��������� ������� �����������
    /// </summary>
    public enum ObstaclePosition
    {
      /// <summary>������� �������</summary>
      Top,
      /// <summary>������� �������</summary>
      Middle,
      /// <summary>������ �������</summary>
      Bottom,
      /// <summary>������� � ������ �������</summary>
      TopBottom,
      /// <summary>������� � ������� �������</summary>
      TopMiddle,
      /// <summary>������� � ������ �������</summary>
      MiddleBottom
    }

    /// <summary>
    /// ������� ��������� ������������ �����������
    /// </summary>
    /// <param name="parX">���������� X ��������� �������</param>
    /// <param name="parPosition">������� �����������</param>
    public StaticObstacle(float parX, ObstaclePosition parPosition)
        : base(parX, GetYPosition(parPosition), ObstacleTypes.Static)
    {
      Position = parPosition;
    }

    /// <summary>
    /// ���������� ���������� Y ��� �������� ������� �����������
    /// </summary>
    /// <param name="parPosition">������� �����������</param>
    /// <returns>���������� Y ��� �������</returns>
    private static float GetYPosition(ObstaclePosition parPosition) => parPosition switch
    {
      ObstaclePosition.Top => 0,
      ObstaclePosition.Middle => (20 - HEIGHT) / 2,
      ObstaclePosition.Bottom => 20 - HEIGHT,
      _ => 0
    };

    /// <summary>
    /// ���������, �������� �� �������� ���������� Y ������ �����������
    /// </summary>
    /// <param name="parY">���������� Y</param>
    /// <returns>True, ���� ���������� Y ��������� � �������� ������ �����������, ����� False</returns>
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
