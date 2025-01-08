using Base.Controllers;
using Base.Models.Menu;

namespace Consolee.ConsoleModels.Menu
{
  /// <summary>
  /// Консольная реализация фабрики меню
  /// </summary>
  public class MenuFactory : MenuFactoryBase
  {
    /// <summary>
    /// Инициализирует новый экземпляр консольной фабрики меню
    /// </summary>
    /// <param name="parGameController">Контроллер игры</param>
    /// <param name="parRulesController">Контроллер правил</param>
    /// <param name="parHighScoresController">Контроллер таблицы рекордов</param>
    public MenuFactory(
        IGameController parGameController,
        IRulesController parRulesController,
        IHighScoresController parHighScoresController)
        : base(parGameController, parRulesController, parHighScoresController)
    {
    }

    /// <summary>
    /// Создает главное меню
    /// </summary>
    /// <returns>Экземпляр главного меню</returns>
    public override Menu CreateMainMenu()
    {
      var items = new List<MenuItem>
        {
            CreatePlayMenuItem(),
            CreateRulesMenuItem(),
            CreateHighScoresMenuItem(),
            CreateExitMenuItem()
        };

      return new Menu(items);
    }

    /// <summary>
    /// Создает пункт меню "Играть"
    /// </summary>
    /// <returns>Пункт меню "Играть"</returns>
    protected override MenuItem CreatePlayMenuItem()
    {
      return new PlayMenuItem(() => _gameController.StartGame());
    }

    /// <summary>
    /// Создает пункт меню "Правила"
    /// </summary>
    /// <returns>Пункт меню "Правила"</returns>
    protected override MenuItem CreateRulesMenuItem()
    {
      return new RulesMenuItem(() => _rulesController.Show());
    }

    /// <summary>
    /// Создает пункт меню "Таблица рекордов"
    /// </summary>
    /// <returns>Пункт меню "Таблица рекордов"</returns>
    protected override MenuItem CreateHighScoresMenuItem()
    {
      return new HighScoresMenuItem(() => _highScoresController.Show());
    }

    /// <summary>
    /// Создает пункт меню "Выход"
    /// </summary>
    /// <returns>Пункт меню "Выход"</returns>
    protected override MenuItem CreateExitMenuItem()
    {
      return new ExitMenuItem(() => Environment.Exit(0));
    }
  }

}

