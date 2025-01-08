namespace Base.Models.Obstacles
{
  /// <summary>
  /// ����� �������
  /// </summary>
  public class Coin : BaseObstacle
  {
    /// <summary>
    /// ������ �������
    /// </summary>
    public const int HEIGHT = 20 / 6;

    /// <summary>
    /// ������� ��������� �������
    /// </summary>
    /// <param name="parX">���������� X ��������� �������</param>
    /// <param name="parY">���������� Y ��������� �������</param>
    public Coin(float parX, float parY)
        : base(parX, parY, ObstacleTypes.Coin) { }

    /// <summary>
    /// ���������, �������� �� �������� ���������� Y ������ �������
    /// </summary>
    /// <param name="parY">���������� Y</param>
    /// <returns>True, ���� ���������� Y ��������� � �������� ������ �������, ����� False</returns>
    public override bool IsPartOfObstacle(float parY) =>
        parY >= Y && parY < Y + HEIGHT;
  }

}
