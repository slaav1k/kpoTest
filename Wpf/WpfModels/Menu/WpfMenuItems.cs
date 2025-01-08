namespace Wpf.WpfModels.Menu
{
  /// <summary>
  /// WPF реализация пункта меню "Играть"
  /// </summary>
  public class WpfPlayMenuItem : WpfMenuItem
  {
    /// <summary>
    /// Инициализирует новый экземпляр пункта меню "Играть"
    /// </summary>
    /// <param name="parStartGame">Действие запуска игры</param>
    public WpfPlayMenuItem(Action parStartGame) : base("Играть", parStartGame)
    {
    }
  }

  /// <summary>
  /// WPF реализация пункта меню "Правила"
  /// </summary>
  public class WpfRulesMenuItem : WpfMenuItem
  {
    /// <summary>
    /// Инициализирует новый экземпляр пункта меню "Правила"
    /// </summary>
    /// <param name="parShowRules">Действие показа правил</param>
    public WpfRulesMenuItem(Action parShowRules) : base("Правила", parShowRules)
    {
    }
  }

  /// <summary>
  /// WPF реализация пункта меню "Таблица рекордов"
  /// </summary>
  public class WpfHighScoresMenuItem : WpfMenuItem
  {
    /// <summary>
    /// Инициализирует новый экземпляр пункта меню "Таблица рекордов"
    /// </summary>
    /// <param name="parShowHighScores">Действие показа таблицы рекордов</param>
    public WpfHighScoresMenuItem(Action parShowHighScores) : base("Рекорды", parShowHighScores)
    {
    }
  }

  /// <summary>
  /// WPF реализация пункта меню "Выход"
  /// </summary>
  public class WpfExitMenuItem : WpfMenuItem
  {
    /// <summary>
    /// Инициализирует новый экземпляр пункта меню "Выход"
    /// </summary>
    /// <param name="parExit">Действие выхода</param>
    public WpfExitMenuItem(Action parExit) : base("Выход", parExit)
    {
    }
  }
}

