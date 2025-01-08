using Base.Models;

namespace Base.Views
{
  /// <summary>
  /// Базовый класс представления игры
  /// </summary>
  public abstract class GameViewBase
  {
    /// <summary>
    /// Отрисовывает состояние игры
    /// </summary>
    /// <param name="parGameState">Состояние игры</param>
    public abstract void Render(GameStateBase parGameState);

    /// <summary>
    /// Отображает экран окончания игры
    /// </summary>
    /// <param name="parFinalScore">Финальный счет</param>
    public abstract void ShowGameOver(int parFinalScore);
  }
}
