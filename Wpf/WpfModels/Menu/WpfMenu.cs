using Base.Models.Menu;




namespace Wpf.WpfModels.Menu
{
  /// <summary>
  /// WPF реализация меню
  /// </summary>
  public class WpfMenu : MenuBase
  {
    /// <summary>
    /// Инициализирует новый экземпляр WPF меню
    /// </summary>
    /// <param name="parItems">Список пунктов меню</param>
    public WpfMenu(IEnumerable<WpfMenuItem> parItems) : base(parItems.Cast<MenuItemBase>())
    {
    }
  }
}

