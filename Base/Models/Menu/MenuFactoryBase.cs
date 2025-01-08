using Base.Controllers;

namespace Base.Models.Menu
{
  /// <summary>
  /// Базовый класс фабрики меню
  /// </summary>
  public abstract class MenuFactoryBase
  {
    /// <summary>
    /// Контроллер игры
    /// </summary>
    protected readonly IGameController _gameController;

    /// <summary>
    /// Контроллер правил
    /// </summary>
    protected readonly IRulesController _rulesController;

    /// <summary>
    /// Контроллер таблицы рекордов
    /// </summary>
    protected readonly IHighScoresController _highScoresController;

    /// <summary>
    /// Инициализирует новый экземпляр фабрики меню
    /// </summary>
    /// <param name="parGameController">Контроллер игры</param>
    /// <param name="parRulesController">Контроллер правил</param>
    /// <param name="parHighScoresController">Контроллер таблицы рекордов</param>
    protected MenuFactoryBase(
        IGameController parGameController,
        IRulesController parRulesController,
        IHighScoresController parHighScoresController)
    {
      _gameController = parGameController;
      _rulesController = parRulesController;
      _highScoresController = parHighScoresController;
    }

    /// <summary>
    /// Создает главное меню
    /// </summary>
    /// <returns>Экземпляр главного меню</returns>
    public abstract MenuBase CreateMainMenu();

    /// <summary>
    /// Создает пункт меню "Играть".
    /// </summary>
    /// <returns>Экземпляр пункта меню "Играть"</returns>
    protected abstract MenuItemBase CreatePlayMenuItem();

    /// <summary>
    /// Создает пункт меню "Правила".
    /// </summary>
    /// <returns>Экземпляр пункта меню "Правила"</returns>
    protected abstract MenuItemBase CreateRulesMenuItem();

    /// <summary>
    /// Создает пункт меню "Таблица рекордов".
    /// </summary>
    /// <returns>Экземпляр пункта меню "Таблица рекордов"</returns>
    protected abstract MenuItemBase CreateHighScoresMenuItem();

    /// <summary>
    /// Создает пункт меню "Выход".
    /// </summary>
    /// <returns>Экземпляр пункта меню "Выход"</returns>
    protected abstract MenuItemBase CreateExitMenuItem();
  }
}
