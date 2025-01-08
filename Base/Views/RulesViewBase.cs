using Base.Models;

namespace Base.Views
{
  /// <summary>
  /// Базовый класс представления правил
  /// </summary>
  public abstract class RulesViewBase
  {
    /// <summary>
    /// Сообщение о возврате в меню
    /// </summary>
    protected const string RETURN_MESSAGE = "Нажмите ESC для выхода";

    /// <summary>
    /// Модель правил
    /// </summary>
    protected readonly RulesModel _rulesModel;

    /// <summary>
    /// Инициализирует новый экземпляр представления правил
    /// </summary>
    protected RulesViewBase()
    {
      _rulesModel = RulesModel.Instance;
    }

    /// <summary>
    /// Отрисовывает правила
    /// </summary>
    public abstract void Render();
  }
}

