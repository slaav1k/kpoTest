namespace Base.Models.Obstacles
{
  /// <summary>
  /// Класс врага
  /// </summary>
  public class Enemy : BaseObstacle
  {
    /// <summary>
    /// Высота врага
    /// </summary>
    public const int HEIGHT = 4;

    /// <summary>
    /// Список пуль, выпущенных врагом
    /// </summary>
    private readonly List<Bullet> _bullets = new();

    /// <summary>
    /// Указывает, переместился ли враг
    /// </summary>
    private bool _hasMoved = false;

    /// <summary>
    /// Указывает, стрелял ли враг
    /// </summary>
    private bool _hasShot = false;

    /// <summary>
    /// Получает список пуль, выпущенных врагом
    /// </summary>
    public IReadOnlyList<Bullet> Bullets => _bullets;

    /// <summary>
    /// Создает экземпляр врага
    /// </summary>
    /// <param name="parX">Координата X начальной позиции</param>
    /// <param name="parY">Координата Y начальной позиции</param>
    public Enemy(float parX, float parY)
        : base(parX, parY, ObstacleTypes.Enemy) { }

    /// <summary>
    /// Обновляет состояние врага
    /// </summary>
    /// <param name="parDeltaTime">Время, прошедшее с последнего обновления</param>
    public override void Update(float parDeltaTime)
    {
      if (!_hasMoved && X <= 40)
      {
        _hasMoved = true;
        Y += 2;
      }

      if (_hasMoved && !_hasShot)
      {
        _hasShot = true;
        Shoot();
      }

      foreach (var elBullet in _bullets.ToList())
      {
        elBullet.Update(parDeltaTime);
        if (elBullet.X < 0 || elBullet.Y < 0 || elBullet.Y >= 20)
        {
          _bullets.Remove(elBullet);
        }
      }
    }

    /// <summary>
    /// Выпускает пули
    /// </summary>
    private void Shoot()
    {
      // Прямая пуля
      _bullets.Add(new Bullet(X, Y + HEIGHT / 2, 0));
      // Пуля вверх
      _bullets.Add(new Bullet(X, Y + HEIGHT / 2, -30));
      // Пуля вниз
      _bullets.Add(new Bullet(X, Y + HEIGHT / 2, 30));
    }

    /// <summary>
    /// Проверяет, является ли заданная координата Y частью врага
    /// </summary>
    /// <param name="parY">Координата Y</param>
    /// <returns>True, если координата Y находится в пределах высоты врага, иначе False</returns>
    public override bool IsPartOfObstacle(float parY) =>
        parY >= Y && parY < Y + HEIGHT;


  }

}
