using Base.Models;

namespace Consolee.ConsoleModels
{
  /// <summary>
  /// Консольная реализация модели игры
  /// </summary>
  public class GameModel : GameModelBase
  {
    /// <summary>
    /// Инициализирует новый экземпляр консольной модели игры
    /// </summary>
    public GameModel() : base(new GameState())
    {
    }
  }
}

