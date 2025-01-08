namespace Base.Models.Obstacles
{
  /// <summary>
  /// ����� �������
  /// </summary>
  public class Bullet : BaseObstacle
  {
    /// <summary>
    /// ������ �������
    /// </summary>
    public const int HEIGHT = 1;

    /// <summary>
    /// �������� �������
    /// </summary>
    private const float SPEED = 30.0f;

    /// <summary>
    /// ���� �������� ������� � ��������
    /// </summary>
    private readonly float _angle;

    /// <summary>
    /// ������� ��������� �������
    /// </summary>
    /// <param name="parX">���������� X ��������� �������</param>
    /// <param name="parY">���������� Y ��������� �������</param>
    /// <param name="parAngleInDegrees">���� �������� � ��������</param>
    public Bullet(float parX, float parY, float parAngleInDegrees)
        : base(parX, parY, ObstacleTypes.Bullet)
    {
      _angle = (float)(parAngleInDegrees * Math.PI / 180);
    }

    /// <summary>
    /// ��������� ��������� �������
    /// </summary>
    /// <param name="parDeltaTime">�����, ��������� � ���������� ����������</param>
    public override void Update(float parDeltaTime)
    {
      X -= SPEED * parDeltaTime * (float)Math.Cos(_angle);
      Y += SPEED * parDeltaTime * (float)Math.Sin(_angle);
    }

    /// <summary>
    /// ���������, �������� �� �������� ���������� Y ������ �������
    /// </summary>
    /// <param name="parY">���������� Y</param>
    /// <returns>True, ���� ���������� Y ������ � ������� �������, ����� False</returns>
    public override bool IsPartOfObstacle(float parY) =>
        Math.Abs(parY - Y) < 0.5f;
  }

}
