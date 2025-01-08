using Base.Models.Obstacles;

namespace Base.Models
{
  /// <summary>
  /// Базовый класс модели игры
  /// </summary>
  public abstract class GameModelBase
  {
    /// <summary>
    /// Сила тяжести
    /// </summary>
    protected const float GRAVITY = 30.0f;

    /// <summary>
    /// Сила прыжка
    /// </summary>
    protected const float JUMP_FORCE = -15.0f;

    /// <summary>
    /// Увеличение скорости препятствий
    /// </summary>
    protected const float SPEED_INCREASE = 5.0f;

    /// <summary>
    /// Интервал появления препятствий
    /// </summary>
    protected const float OBSTACLE_INTERVAL = 2.0f;

    /// <summary>
    /// Интервал появления монет
    /// </summary>
    protected const float COIN_INTERVAL = 1.0f;

    /// <summary>
    /// Порог для высокого счета
    /// </summary>
    private const int HIGH_SCORE_THRESHOLD = 10;

    /// <summary>
    /// Порог для среднего счета
    /// </summary>
    private const int MID_SCORE_THRESHOLD = 6;

    /// <summary>
    /// Шанс появления врага
    /// </summary>
    private const int ENEMY_SPAWN_CHANCE = 90;

    /// <summary>
    /// Максимальное количество врагов для высокого счета
    /// </summary>
    private const int MAX_RANDOM_HIGH_SCORE = 7;

    /// <summary>
    /// Максимальное количество врагов для среднего счета
    /// </summary>
    private const int MAX_RANDOM_MID_SCORE = 6;

    /// <summary>
    /// Максимальное количество врагов для низкого счета
    /// </summary>
    private const int MAX_RANDOM_LOW_SCORE = 3;

    /// <summary>
    /// Максимальная позиция Y для монеты
    /// </summary>
    private const int COIN_MAX_Y_POSITION = 18;

    /// <summary>
    /// Минимальная позиция Y для монеты
    /// </summary>
    private const int COIN_MIN_Y_POSITION = 2;

    /// <summary>
    /// Минимальная позиция Y для врага
    /// </summary>
    private const int ENEMY_MIN_Y_POSITION = 2;

    /// <summary>
    /// Максимальная позиция Y для врага
    /// </summary>
    private const int ENEMY_MAX_Y_POSITION = 16;

    /// <summary>
    /// Стартовая координата игрока по Х
    /// </summary>
    private const int START_PLAYER_X = 20;

    /// <summary>
    /// Стартовая координата игрока по Y
    /// </summary>
    private const int START_PLAYER_Y = 10;

    /// <summary>
    /// Вертикальная скорость игрока
    /// </summary>
    protected float _verticalVelocity = 0;

    /// <summary>
    /// Скорость движения препятствий
    /// </summary>
    protected float _obstacleSpeed = 20.0f;

    /// <summary>
    /// Время с последнего появления препятствия
    /// </summary>
    protected float _timeSinceLastObstacle = 0;

    /// <summary>
    /// Текущее состояние игры
    /// </summary>
    protected readonly GameStateBase _gameState;

    /// <summary>
    /// Генератор случайных чисел
    /// </summary>
    protected readonly Random _random = new Random();

    /// <summary>
    /// Количество пройденных препятствий
    /// </summary>
    protected int _passedObstacles = 0;

    /// <summary>
    /// Количество собранных монет
    /// </summary>
    protected int _collectedCoins = 0;

    /// <summary>
    /// Количество убитых врагов
    /// </summary>
    protected int _killedEnemies = 0;

    /// <summary>
    /// Флаг, указывающий на то, был ли предыдущий объект препятствием
    /// </summary>
    protected bool _lastWasObstacle = false;

    /// <summary>
    /// Текущий счет игры
    /// </summary>
    public int Score => _passedObstacles + (_collectedCoins * 3) + (_killedEnemies * 5);

    /// <summary>
    /// Инициализирует новый экземпляр модели игры
    /// </summary>
    /// <param name="parGameState">Состояние игры</param>
    protected GameModelBase(GameStateBase parGameState)
    {
      _gameState = parGameState;
      _gameState.PlayerX = START_PLAYER_X;
      _gameState.PlayerY = START_PLAYER_Y;
    }

    /// <summary>
    /// Выполняет прыжок
    /// </summary>
    public void Jump()
    {
      _verticalVelocity = JUMP_FORCE;
    }

    /// <summary>
    /// Обновляет состояние игры
    /// </summary>
    /// <param name="parDeltaTime">Время с последнего обновления</param>
    public void Update(float parDeltaTime)
    {
      UpdatePlayerPosition(parDeltaTime);
      UpdateObstacles(parDeltaTime);
      UpdateScore();
    }

    /// <summary>
    /// Обновляет позицию игрока
    /// </summary>
    /// <param name="parDeltaTime">Время с последнего обновления</param>
    protected virtual void UpdatePlayerPosition(float parDeltaTime)
    {
      _verticalVelocity += GRAVITY * parDeltaTime;
      _gameState.PlayerY += _verticalVelocity * parDeltaTime;

      if (_gameState.PlayerY < 0)
      {
        _gameState.PlayerY = 0;
        _verticalVelocity = 0;
      }
      if (_gameState.PlayerY >= 19)
      {
        _gameState.PlayerY = 19;
        _verticalVelocity = 0;
      }
    }

    /// <summary>
    /// Обновляет препятствия
    /// </summary>
    /// <param name="parDeltaTime">Время с последнего обновления</param>
    protected virtual void UpdateObstacles(float parDeltaTime)
    {
      MoveObstacles(parDeltaTime);
      GenerateNewObstacle(parDeltaTime);
    }

    /// <summary>
    /// Перемещает препятствия
    /// </summary>
    /// <param name="parDeltaTime">Время с последнего обновления</param>
    protected virtual void MoveObstacles(float parDeltaTime)
    {
      for (int i = _gameState.Obstacles.Count - 1; i >= 0; i--)
      {
        var obstacle = _gameState.Obstacles[i];
        var newX = obstacle.X - _obstacleSpeed * parDeltaTime;

        if (newX < 0)
        {
          HandleObstacleRemoval(i, obstacle);
        }
        else
        {
          obstacle.Update(parDeltaTime);
          AdjustBulletPosition(obstacle, newX, parDeltaTime);
        }
      }
    }

    /// <summary>
    /// Обрабатывает удаление препятствия
    /// </summary>
    /// <param name="parIndex">Индекс удаляемого препятствия</param>
    /// <param name="parObstacle">Препятствие для удаления</param>
    protected virtual void HandleObstacleRemoval(int parIndex, BaseObstacle parObstacle)
    {
      _gameState.Obstacles.RemoveAt(parIndex);
      if (parObstacle.Type != BaseObstacle.ObstacleTypes.PlayerBullet)
      {
        _passedObstacles++;
        IncreaseObstacleSpeedIfNeeded();
      }
    }

    /// <summary>
    /// Увеличивает скорость препятствий при достижении порога
    /// </summary>
    protected virtual void IncreaseObstacleSpeedIfNeeded()
    {
      if (Score % 20 == 0)
      {
        _obstacleSpeed += SPEED_INCREASE;
      }
    }

    /// <summary>
    /// Корректирует позицию пули
    /// </summary>
    /// <param name="parObstacle">Объект, для которого нужно скорректировать позицию</param>
    /// <param name="parNewX">Новая позиция по оси X</param>
    /// <param name="parDeltaTime">Время с последнего обновления</param>
    protected virtual void AdjustBulletPosition(BaseObstacle parObstacle, float parNewX, float parDeltaTime)
    {
      if (parObstacle.Type == BaseObstacle.ObstacleTypes.PlayerBullet)
      {
        parNewX += (PlayerBullet.SPEED + _obstacleSpeed) * parDeltaTime;
      }
      parObstacle.SetX(parNewX);

      if (parObstacle is Enemy enemy)
      {
        foreach (var elBullet in enemy.Bullets)
        {
          if (!_gameState.Obstacles.Contains(elBullet))
          {
            _gameState.Obstacles.Add(elBullet);
          }
        }
      }
    }

    /// <summary>
    /// Генерирует новое препятствие
    /// </summary>
    /// <param name="parDeltaTime">Время с последнего обновления</param>
    protected virtual void GenerateNewObstacle(float parDeltaTime)
    {
      _timeSinceLastObstacle += parDeltaTime;
      if (_timeSinceLastObstacle >= OBSTACLE_INTERVAL / 2 && !_lastWasObstacle)
      {
        GenerateObstacle();
        _lastWasObstacle = true;
      }
      if (_timeSinceLastObstacle >= OBSTACLE_INTERVAL)
      {
        _timeSinceLastObstacle = 0;
        GenerateObstacle();
        _lastWasObstacle = false;
      }
    }

    /// <summary>
    /// Генерирует новое препятствие
    /// </summary>
    protected virtual void GenerateObstacle()
    {
      BaseObstacle obstacle;

      if (_lastWasObstacle)
      {
        if (Score >= HIGH_SCORE_THRESHOLD && _random.Next(100) < ENEMY_SPAWN_CHANCE)
        {
          obstacle = new Enemy(79, _random.Next(ENEMY_MIN_Y_POSITION, ENEMY_MAX_Y_POSITION));
        }
        else
        {
          obstacle = new Coin(79, _random.Next(COIN_MIN_Y_POSITION, COIN_MAX_Y_POSITION - Coin.HEIGHT));
        }
      }
      else
      {
        if (Score >= HIGH_SCORE_THRESHOLD)
        {
          int type = _random.Next(MAX_RANDOM_HIGH_SCORE);
          obstacle = type switch
          {
            0 => new StaticObstacle(79, StaticObstacle.ObstaclePosition.Top),
            1 => new StaticObstacle(79, StaticObstacle.ObstaclePosition.Middle),
            2 => new StaticObstacle(79, StaticObstacle.ObstaclePosition.Bottom),
            3 => new StaticObstacle(79, StaticObstacle.ObstaclePosition.TopBottom),
            4 => new StaticObstacle(79, StaticObstacle.ObstaclePosition.TopMiddle),
            5 => new StaticObstacle(79, StaticObstacle.ObstaclePosition.MiddleBottom),
            _ => new MovingObstacle(79, 1)
          };
        }
        else if (Score >= MID_SCORE_THRESHOLD)
        {
          int type = _random.Next(MAX_RANDOM_MID_SCORE);
          obstacle = type switch
          {
            0 => new StaticObstacle(79, StaticObstacle.ObstaclePosition.Top),
            1 => new StaticObstacle(79, StaticObstacle.ObstaclePosition.Middle),
            2 => new StaticObstacle(79, StaticObstacle.ObstaclePosition.Bottom),
            3 => new StaticObstacle(79, StaticObstacle.ObstaclePosition.TopBottom),
            4 => new StaticObstacle(79, StaticObstacle.ObstaclePosition.TopMiddle),
            _ => new StaticObstacle(79, StaticObstacle.ObstaclePosition.MiddleBottom)
          };
        }
        else
        {
          int type = _random.Next(MAX_RANDOM_LOW_SCORE);
          obstacle = type switch
          {
            0 => new StaticObstacle(79, StaticObstacle.ObstaclePosition.Top),
            1 => new StaticObstacle(79, StaticObstacle.ObstaclePosition.Middle),
            _ => new StaticObstacle(79, StaticObstacle.ObstaclePosition.Bottom)
          };
        }
      }

      _gameState.Obstacles.Add(obstacle);
    }

    /// <summary>
    /// Обновляет счет
    /// </summary>
    protected virtual void UpdateScore()
    {
      _gameState.Score = Score;
    }

    /// <summary>
    /// Проверяет столкновения
    /// </summary>
    /// <returns>true, если произошло столкновение</returns>
    public virtual bool CheckCollisions()
    {
      float playerRadius = 0.5f;

      foreach (var obstacle in _gameState.Obstacles.ToList())
      {
        if (obstacle.Type == BaseObstacle.ObstacleTypes.PlayerBullet)
        {
          foreach (var elTarget in _gameState.Obstacles.ToList())
          {
            if (elTarget.Type == BaseObstacle.ObstacleTypes.Enemy &&
                Math.Abs(elTarget.X - obstacle.X) < 1 &&
                elTarget.IsPartOfObstacle(obstacle.Y))
            {
              _killedEnemies++;
              _gameState.Obstacles.Remove(elTarget);
              _gameState.Obstacles.Remove(obstacle);
              return false;
            }
          }
          continue;
        }

        if (Math.Abs(obstacle.X - _gameState.PlayerX) < 1 + playerRadius)
        {
          if (obstacle.IsPartOfObstacle(_gameState.PlayerY - playerRadius) ||
              obstacle.IsPartOfObstacle(_gameState.PlayerY + playerRadius))
          {
            if (obstacle.Type == BaseObstacle.ObstacleTypes.Coin)
            {
              _collectedCoins++;
              _gameState.Obstacles.Remove(obstacle);
              return false;
            }
            if (obstacle.Type == BaseObstacle.ObstacleTypes.Bullet)
            {
              return true;
            }
            return true;
          }
        }
      }
      return false;
    }

    /// <summary>
    /// Получает текущее состояние игры
    /// </summary>
    /// <returns>Состояние игры</returns>
    public virtual GameStateBase GetGameState()
    {
      return _gameState;
    }

    /// <summary>
    /// Сбрасывает состояние игры к начальным значениям
    /// </summary>
    public virtual void Reset()
    {
      _gameState.Reset();
      _verticalVelocity = 0;
      _obstacleSpeed = 20.0f;
      _timeSinceLastObstacle = 0;
      _passedObstacles = 0;
      _collectedCoins = 0;
      _killedEnemies = 0;
    }

    /// <summary>
    /// Выстрел игрока
    /// </summary>
    public void Shoot()
    {
      _gameState.Obstacles.Add(new PlayerBullet(_gameState.PlayerX + 1, _gameState.PlayerY));
    }
  }

}

