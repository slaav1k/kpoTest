using Base.Models.Menu;

namespace Wpf.WpfModels.Menu
{
  /// <summary>
  /// WPF реализация пункта меню
  /// </summary>
  public class WpfMenuItem : MenuItemBase
  {
    /// <summary>
    /// Действие при выборе пункта
    /// </summary>
    private readonly Action _action;

    /// <summary>
    /// Инициализирует новый экземпляр WPF пункта меню
    /// </summary>
    /// <param name="text">Текст пункта меню</param>
    /// <param name="parAction">Действие при выборе</param>
    public WpfMenuItem(string text, Action parAction) : base(text)
    {
      _action = parAction;
    }

    /// <summary>
    /// Выполняет действие пункта меню
    /// </summary>
    public override void Execute()
    {
      _action?.Invoke();
    }
  }
}

