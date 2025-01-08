namespace Base.Controllers
{
  /// <summary>
  /// Интерфейс контроллера меню
  /// </summary>
  public interface IMenuController
  {
    /// <summary>
    /// Запускает отображение меню
    /// </summary>
    void Start();

    /// <summary>
    /// Останавливает работу меню
    /// </summary>
    void Stop();
  }
}
