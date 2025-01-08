namespace Base.Models.Menu
{
  /// <summary>
  /// Базовый класс меню
  /// </summary>
  public abstract class MenuBase
  {
    /// <summary>
    /// Список пунктов меню
    /// </summary>
    private readonly List<MenuItemBase> _items;

    /// <summary>
    /// Индекс выбранного пункта меню
    /// </summary>
    private int _selectedIndex;

    /// <summary>
    /// Инициализирует новый экземпляр меню
    /// </summary>
    /// <param name="parItems">Список пунктов меню</param>
    protected MenuBase(IEnumerable<MenuItemBase> parItems)
    {
      _items = parItems.ToList();
      if (_items.Any())
      {
        _items[0].IsSelected = true;
      }
    }

    /// <summary>
    /// Получает список пунктов меню
    /// </summary>
    public IReadOnlyList<MenuItemBase> Items => _items;

    /// <summary>
    /// Перемещает выбор вверх
    /// </summary>
    public void MoveUp()
    {
      _items[_selectedIndex].IsSelected = false;
      _selectedIndex = (_selectedIndex - 1 + _items.Count) % _items.Count;
      _items[_selectedIndex].IsSelected = true;
    }

    /// <summary>
    /// Перемещает выбор вниз
    /// </summary>
    public void MoveDown()
    {
      _items[_selectedIndex].IsSelected = false;
      _selectedIndex = (_selectedIndex + 1) % _items.Count;
      _items[_selectedIndex].IsSelected = true;
    }

    /// <summary>
    /// Выполняет выбранный пункт меню
    /// </summary>
    public void ExecuteSelected()
    {
      _items[_selectedIndex].Execute();
    }
  }
}
