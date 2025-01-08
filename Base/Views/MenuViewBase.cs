using Base.Models.Menu;

namespace Base.Views
{
  /// <summary>
  /// Базовый класс представления меню
  /// </summary>
  public abstract class MenuViewBase
  {
    /// <summary>
    /// Начальная позиция меню по Y
    /// </summary>
    protected const int MENU_START_Y = 8;

    /// <summary>
    /// Отрисовывает меню
    /// </summary>
    /// <param name="parMenu">Модель меню</param>
    public abstract void Render(MenuBase parMenu);
  }
}

