namespace Consolee.ConsoleModels.Menu
{
  /// <summary>
  /// Реализация пункта меню "Играть"
  /// </summary>
  public class PlayMenuItem : MenuItem
  {
    /// <summary>
    /// Инициализирует новый экземпляр пункта меню "Играть"
    /// </summary>
    /// <param name="parStartGame">Действие запуска игры</param>
    public PlayMenuItem(Action parStartGame) : base("Играть", parStartGame)
    {
    }
  }

  /// <summary>
  /// Реализация пункта меню "Правила"
  /// </summary>
  public class RulesMenuItem : MenuItem
  {
    /// <summary>
    /// Инициализирует новый экземпляр пункта меню "Правила"
    /// </summary>
    /// <param name="parShowRules">Действие показа правил</param>
    public RulesMenuItem(Action parShowRules) : base("Правила", parShowRules)
    {
    }
  }

  /// <summary>
  /// Реализация пункта меню "Таблица рекордов"
  /// </summary>
  public class HighScoresMenuItem : MenuItem
  {
    /// <summary>
    /// Инициализирует новый экземпляр пункта меню "Таблица рекордов"
    /// </summary>
    /// <param name="parShowHighScores">Действие показа таблицы рекордов</param>
    public HighScoresMenuItem(Action parShowHighScores) : base("Рекорды", parShowHighScores)
    {
    }
  }

  /// <summary>
  /// Реализация пункта меню "Выход"
  /// </summary>
  public class ExitMenuItem : MenuItem
  {
    /// <summary>
    /// Инициализирует новый экземпляр пункта меню "Выход"
    /// </summary>
    /// <param name="parExit">Действие выхода</param>
    public ExitMenuItem(Action parExit) : base("Выход", parExit)
    {
    }
  }

}

