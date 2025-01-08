using Base.Models.Menu;

namespace Consolee.ConsoleModels.Menu
{
  /// <summary>
  /// Консольная реализация меню
  /// </summary>
  public class Menu : MenuBase
  {
    /// <summary>
    /// Инициализирует новый экземпляр консольного меню
    /// </summary>
    /// <param name="items">Список пунктов меню</param>
    public Menu(IEnumerable<MenuItem> items) : base(items.Cast<MenuItemBase>())
    {
    }
  }
}

