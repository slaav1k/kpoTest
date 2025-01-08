namespace Base.Models.Obstacles
{
  /// <summary>
  /// ����� ������� ������
  /// </summary>
  public class PlayerBullet : BaseObstacle
  {
    /// <summary>
    /// ������ ������� ������
    /// </summary>
    public const int HEIGHT = 1;

    /// <summary>
    /// �������� ������� ������
    /// </summary>
    public const float SPEED = 40.0f;

    /// <summary>
    /// ������� ��������� ������� ������
    /// </summary>
    /// <param name="parX">���������� X ��������� �������</param>
    /// <param name="parY">���������� Y ��������� �������</param>
    public PlayerBullet(float parX, float parY)
        : base(parX, parY, ObstacleTypes.PlayerBullet)
    {
    }

    /// <summary>
    /// ��������� ��������� ������� ������
    /// </summary>
    /// <param name="parDeltaTime">�����, ��������� � ���������� ����������</param>
    public override void Update(float parDeltaTime) { }

    /// <summary>
    /// ���������, �������� �� �������� ���������� Y ������ ������� ������
    /// </summary>
    /// <param name="parY">���������� Y</param>
    /// <returns>True, ���� ���������� Y ��������� � �������� ������ �������, ����� False</returns>
    public override bool IsPartOfObstacle(float parY) =>
        Math.Abs(parY - Y) < 0.5f;
  }

}
