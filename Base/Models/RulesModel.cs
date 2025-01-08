namespace Base.Models
{
  /// <summary>
  /// Модель правил игры
  /// </summary>
  public class RulesModel
  {
    /// <summary>
    /// Экземпляр модели правил (реализует паттерн Singleton)
    /// </summary>
    private static RulesModel? _instance;

    /// <summary>
    /// Получает единственный экземпляр модели правил
    /// </summary>
    public static RulesModel Instance => _instance ??= new RulesModel();

    /// <summary>
    /// Список правил игры
    /// </summary>
    public IReadOnlyList<string> Rules { get; }

    /// <summary>
    /// Конструктор, который инициализирует список правил игры
    /// </summary>
    private RulesModel()
    {
      Rules = new List<string>
        {
            "1. ПРОБЕЛ - стрелять, ВВЕРХ - стрелять",
            "2. P - пауза, ESC - выход",
            "3. Избегайте препятствий и врагов",
            "4. Очки: пройденное препятствие — 1",
            "\tподобранная монета — 3",
            "\tубитый враг — 5",
            "5. Скорость игры увеличивается каждые 20 очков",
            "6. Новые типы препятствий появляются на 60 и 100 очках"
        };
    }
  }

}

