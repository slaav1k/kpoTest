namespace Base.Models.Menu
{
  /// <summary>
  /// Базовый класс пункта меню
  /// </summary>
  public abstract class MenuItemBase
  {
    /// <summary>
    /// Текст пункта меню
    /// </summary>
    public string Text { get; }

    /// <summary>
    /// Признак выбранного пункта меню
    /// </summary>
    public bool IsSelected { get; set; }

    /// <summary>
    /// Инициализирует новый экземпляр пункта меню
    /// </summary>
    /// <param name="parText">Текст пункта меню</param>
    protected MenuItemBase(string parText)
    {
      Text = parText;
    }

    /// <summary>
    /// Выполняет действие пункта меню
    /// </summary>
    public abstract void Execute();
  }
}
