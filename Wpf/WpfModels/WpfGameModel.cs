using Base.Models;
namespace Wpf.WpfModels
{
  /// <summary>
  /// WPF реализация модели игры
  /// </summary>
  public class WpfGameModel : GameModelBase
  {
    /// <summary>
    /// Инициализирует новый экземпляр WPF модели игры
    /// </summary>
    public WpfGameModel() : base(new WpfGameState())
    {
    }
  }
}

