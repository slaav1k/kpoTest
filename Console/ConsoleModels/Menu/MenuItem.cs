using Base.Models.Menu;

namespace Consolee.ConsoleModels.Menu
{
  /// <summary>
  /// Консольная реализация пункта меню
  /// </summary>
  public class MenuItem : MenuItemBase
  {
    /// <summary>
    /// Действие, выполняемое при выборе пункта меню
    /// </summary>
    private readonly Action _action;

    /// <summary>
    /// Инициализирует новый экземпляр пункта меню
    /// </summary>
    /// <param name="parText">Текст пункта меню</param>
    /// <param name="parAction">Действие при выборе</param>
    public MenuItem(string parText, Action parAction) : base(parText)
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

