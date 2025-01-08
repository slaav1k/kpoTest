namespace Base.Models.Obstacles
{
  /// <summary>
  /// ����� ����������� �����������
  /// </summary>
  public class MovingObstacle : BaseObstacle
  {
    /// <summary>
    /// ������ �����������
    /// </summary>
    public const int HEIGHT = 6;

    /// <summary>
    /// �������� ������������� �������� �����������
    /// </summary>
    private const float VERTICAL_SPEED = 10.0f;

    /// <summary>
    /// ���������, �������� �� ����������� ����
    /// </summary>
    private bool _movingDown = true;

    /// <summary>
    /// ������� ��������� ����������� �����������
    /// </summary>
    /// <param name="parX">���������� X ��������� �������</param>
    /// <param name="parY">���������� Y ��������� �������</param>
    public MovingObstacle(float parX, float parY)
        : base(parX, parY, ObstacleTypes.Moving) { }

    /// <summary>
    /// ��������� ��������� �����������
    /// </summary>
    /// <param name="parDeltaTime">�����, ��������� � ���������� ����������</param>
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
    /// ���������, �������� �� �������� ���������� Y ������ �����������
    /// </summary>
    /// <param name="parY">���������� Y</param>
    /// <returns>True, ���� ���������� Y ��������� � �������� ������ �����������, ����� False</returns>
    public override bool IsPartOfObstacle(float parY) =>
        parY >= Y && parY < Y + HEIGHT;
  }

}
