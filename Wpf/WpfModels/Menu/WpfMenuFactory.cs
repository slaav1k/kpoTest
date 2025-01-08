using Wpf.WpfControllers;
using Base.Models.Menu;
using Base.Controllers;


namespace Wpf.WpfModels.Menu
{
  /// <summary>
  /// WPF реализация фабрики меню
  /// </summary>
  public class WpfMenuFactory : MenuFactoryBase
  {
    /// <summary>
    /// Инициализирует новый экземпляр WPF фабрики меню
    /// </summary>
    /// <param name="parGameController">Контроллер игры</param>
    /// <param name="parRulesController">Контроллер правил</param>
    /// <param name="parHighScoresController">Контроллер таблицы рекордов</param>
    public WpfMenuFactory(
        IGameController parGameController,
        IRulesController parRulesController,
        IHighScoresController parHighScoresController) : base(parGameController, parRulesController, parHighScoresController)
    {

    }

    /// <summary>
    /// Создает главное меню
    /// </summary>
    /// <returns>Экземпляр главного меню</returns>
    public override WpfMenu CreateMainMenu()
    {
      var items = new List<WpfMenuItem>
      {
        CreatePlayMenuItem(),
        CreateRulesMenuItem(),
        CreateHighScoresMenuItem(),
        CreateExitMenuItem()
      };

      return new WpfMenu(items);
    }

    /// <summary>
    /// Создает пункт меню "Играть"
    /// </summary>
    protected override WpfMenuItem CreatePlayMenuItem()
    {
      var controller = _gameController as WpfGameController ?? throw new InvalidOperationException("Invalid controller type");
      return new WpfPlayMenuItem(() => controller.StartGame());
    }

    /// <summary>
    /// Создает пункт меню "Правила"
    /// </summary>
    protected override WpfMenuItem CreateRulesMenuItem()
    {
      var controller = _rulesController as WpfRulesController ?? throw new InvalidOperationException("Invalid controller type");
      return new WpfRulesMenuItem(() => controller.Show());
    }

    /// <summary>
    /// Создает пункт меню "Таблица рекордов"
    /// </summary>
    protected override WpfMenuItem CreateHighScoresMenuItem()
    {
      var controller = _highScoresController as WpfHighScoresController ?? throw new InvalidOperationException("Invalid controller type");
      return new WpfHighScoresMenuItem(() => controller.Show());
    }

    /// <summary>
    /// Создает пункт меню "Выход"
    /// </summary>
    protected override WpfMenuItem CreateExitMenuItem()
    {
      return new WpfExitMenuItem(() => Environment.Exit(0));
    }
  }
}

