namespace Base.Controllers
{
  /// <summary>
  /// Интерфейс контроллера игры
  /// </summary>
  public interface IGameController
  {
    /// <summary>
    /// Запускает игру
    /// </summary>
    void StartGame();

    /// <summary>
    /// Выполняет игровой цикл
    /// </summary>
    void GameLoop();

    /// <summary>
    /// Завершает игру
    /// </summary>
    void GameOver();
  }
}
