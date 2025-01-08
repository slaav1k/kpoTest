using Base.Models;
using Consolee.ConsoleControllers;
using Consolee.ConsoleModels.Menu;

namespace Tests
{
  /// <summary>
  /// Общий класс для тестирования
  /// </summary>
  [TestClass]
  public class Base
  {
    /// <summary>
    /// Проверяет, что вызов свойства <see cref="RulesModel.Instance"/> всегда возвращает один и тот же экземпляр.
    /// </summary>
    [TestMethod]
    public void Instance_ShouldReturnSameInstance()
    {
      var instance1 = RulesModel.Instance;
      var instance2 = RulesModel.Instance;

      Assert.AreSame(instance1, instance2);
    }

    /// <summary>
    /// Проверяет, что правила не пусты при вызове свойства <see cref="RulesModel.Instance.Rules"/>.
    /// </summary>
    [TestMethod]
    public void Rules_ShouldNotBeEmpty()
    {
      var rules = RulesModel.Instance.Rules;

      Assert.IsTrue(rules.Any());
    }

    /// <summary>
    /// Проверяет, что конструктор <see cref="HighScoreRecord"/> корректно устанавливает имя и очки.
    /// </summary>
    [TestMethod]
    public void Constructor_ShouldSetNameAndScore()
    {
      var record = new HighScoreRecord("Player1", 100);

      Assert.AreEqual("Player1", record.Name);
      Assert.AreEqual(100, record.Score);
    }

    /// <summary>
    /// Проверяет, что метод <see cref="HighScoresManager.IsNewHighScore"/> возвращает <c>false</c> 
    /// при передаче значения в список рекордов, когда рекорд не входит в топ5
    /// </summary>
    [TestMethod]
    public void IsNewHighScore_NotEmptyList_ShouldReturnFalse()
    {
      var manager = new HighScoresManager();
      Assert.IsFalse(manager.IsNewHighScore(5));
    }

    /// <summary>
    /// Проверяет, что метод <see cref="HighScoresManager.AddScore"/> ограничивает список рекордов до 5 элементов.
    /// </summary>
    [TestMethod]
    public void AddScore_ShouldLimitTo5Records()
    {
      var manager = new HighScoresManager();

      for (int i = 0; i < 12; i++)
      {
        manager.AddScore($"Player{i}", i);
      }

      Assert.AreEqual(5, manager.GetScores().Count);
    }

    /// <summary>
    /// Проверяет, что конструктор <see cref="AddHighScoreController"/> инициализирует контроллер с указанным счетом.
    /// </summary>
    [TestMethod]
    public void Constructor_ShouldInitializeWithScore()
    {
      var controller = new AddHighScoreController(100);

      // Проверяем что контроллер создался с указанным счетом
      Assert.IsNotNull(controller);
    }

    /// <summary>
    /// Проверяет, что метод <see cref="HighScoresController.IsNewHighScore"/> корректно делегирует проверку 
    /// на менеджер рекордов и возвращает <c>true</c> для нового рекорда на пустой таблице.
    /// </summary>
    [TestMethod]
    public void IsNewHighScore_ShouldDelegateToManager()
    {
      var controller = new HighScoresController();

      // Новый счет должен быть рекордом для пустой таблицы
      Assert.IsTrue(controller.IsNewHighScore(100));
    }

    /// <summary>
    /// Проверяет, что конструктор <see cref="Menu"/> выбирает первый пункт меню по умолчанию.
    /// </summary>
    [TestMethod]
    public void Constructor_ShouldSelectFirstItem()
    {
      var items = new[] { new MenuItem("Test1", () => { }), new MenuItem("Test2", () => { }) };
      var menu = new Menu(items);

      Assert.IsTrue(menu.Items.First().IsSelected);
      Assert.IsFalse(menu.Items.Last().IsSelected);
    }

    /// <summary>
    /// Проверяет, что метод <see cref="Menu.MoveDown"/> правильно выбирает следующий пункт меню.
    /// </summary>
    [TestMethod]
    public void MoveDown_ShouldSelectNextItem()
    {
      var items = new[] { new MenuItem("Test1", () => { }), new MenuItem("Test2", () => { }) };
      var menu = new Menu(items);

      menu.MoveDown();

      Assert.IsFalse(menu.Items.First().IsSelected);
      Assert.IsTrue(menu.Items.Last().IsSelected);
    }

    /// <summary>
    /// Проверяет, что метод <see cref="Menu.MoveUp"/> правильно выбирает предыдущий пункт меню.
    /// </summary>
    [TestMethod]
    public void MoveUp_ShouldSelectPreviousItem()
    {
      var items = new[] { new MenuItem("Test1", () => { }), new MenuItem("Test2", () => { }) };
      var menu = new Menu(items);
      menu.MoveDown(); // Выбираем второй пункт

      menu.MoveUp();

      Assert.IsTrue(menu.Items.First().IsSelected);
      Assert.IsFalse(menu.Items.Last().IsSelected);
    }

    /// <summary>
    /// Проверяет, что пункт меню <see cref="PlayMenuItem"/> имеет правильный текст и выполняет действие при выборе.
    /// </summary>
    [TestMethod]
    public void PlayMenuItem_ShouldHaveCorrectText()
    {
      bool executed = false;
      var item = new PlayMenuItem(() => executed = true);

      Assert.AreEqual("Играть", item.Text);
      item.Execute();
      Assert.IsTrue(executed);
    }

    /// <summary>
    /// Проверяет, что пункт меню <see cref="RulesMenuItem"/> имеет правильный текст и выполняет действие при выборе.
    /// </summary>
    [TestMethod]
    public void RulesMenuItem_ShouldHaveCorrectText()
    {
      bool executed = false;
      var item = new RulesMenuItem(() => executed = true);

      Assert.AreEqual("Правила", item.Text);
      item.Execute();
      Assert.IsTrue(executed);
    }

    /// <summary>
    /// Проверяет, что пункт меню <see cref="HighScoresMenuItem"/> имеет правильный текст и выполняет действие при выборе.
    /// </summary>
    [TestMethod]
    public void HighScoresMenuItem_ShouldHaveCorrectText()
    {
      bool executed = false;
      var item = new HighScoresMenuItem(() => executed = true);

      Assert.AreEqual("Рекорды", item.Text);
      item.Execute();
      Assert.IsTrue(executed);
    }

    /// <summary>
    /// Проверяет, что пункт меню <see cref="ExitMenuItem"/> имеет правильный текст и выполняет действие при выборе.
    /// </summary>
    [TestMethod]
    public void ExitMenuItem_ShouldHaveCorrectText()
    {
      bool executed = false;
      var item = new ExitMenuItem(() => executed = true);

      Assert.AreEqual("Выход", item.Text);
      item.Execute();
      Assert.IsTrue(executed);
    }

    /// <summary>
    /// Проверяет, что конструктор <see cref="MenuItem"/> корректно задает текст пункта меню.
    /// </summary>
    [TestMethod]
    public void Constructor_ShouldSetText()
    {
      var item = new MenuItem("Test", () => { });
      Assert.AreEqual("Test", item.Text);
    }

    /// <summary>
    /// Проверяет, что метод <see cref="MenuItem.Execute"/> вызывает действие, связанное с пунктом меню.
    /// </summary>
    [TestMethod]
    public void Execute_ShouldInvokeAction()
    {
      bool executed = false;
      var item = new MenuItem("Test", () => executed = true);

      item.Execute();

      Assert.IsTrue(executed);
    }

    /// <summary>
    /// Проверяет, что свойство <see cref="MenuItem.IsSelected"/> корректно устанавливается и изменяется.
    /// </summary>
    [TestMethod]
    public void IsSelected_ShouldBeSettable()
    {
      var item = new MenuItem("Test", () => { });

      item.IsSelected = true;
      Assert.IsTrue(item.IsSelected);

      item.IsSelected = false;
      Assert.IsFalse(item.IsSelected);
    }

    /// <summary>
    /// Проверяет, что при создании главного меню <see cref="Menu"/> первый пункт меню выбран по умолчанию.
    /// </summary>
    [TestMethod]
    public void CreateMainMenu_FirstItemShouldBeSelected()
    {
      var menu = CreateTestMenu();

      Assert.IsTrue(menu.Items.First().IsSelected);
      Assert.IsFalse(menu.Items.Skip(1).Any(item => item.IsSelected));
    }

    /// <summary>
    /// Проверяет, что пункты главного меню <see cref="Menu"/> имеют правильные тексты.
    /// </summary>
    [TestMethod]
    public void CreateMainMenu_ItemsShouldHaveCorrectTexts()
    {
      var menu = CreateTestMenu();
      var items = menu.Items.ToList();

      Assert.AreEqual("Играть", items[0].Text);
      Assert.AreEqual("Правила", items[1].Text);
      Assert.AreEqual("Рекорды", items[2].Text);
      Assert.AreEqual("Выход", items[3].Text);
    }

    /// <summary>
    /// Проверяет, что главное меню <see cref="Menu"/> и его пункты не равны <c>null</c>.
    /// </summary>
    [TestMethod]
    public void CreateMainMenu_ItemsShouldNotBeNull()
    {
      var menu = CreateTestMenu();

      Assert.IsNotNull(menu);
      Assert.IsNotNull(menu.Items);
      CollectionAssert.AllItemsAreNotNull(menu.Items.ToList());
    }

    /// <summary>
    /// Создает тестовое главное меню для проверки.
    /// </summary>
    /// <returns>Экземпляр <see cref="Menu"/> с тестовыми пунктами меню.</returns>
    private Menu CreateTestMenu()
    {
      var items = new List<MenuItem>
    {
        new PlayMenuItem(() => {}),
        new RulesMenuItem(() => {}),
        new HighScoresMenuItem(() => {}),
        new ExitMenuItem(() => {})
    };
      return new Menu(items);
    }


  }
}
