using Base.Models.Menu;
using Base.Views;

namespace Consolee.ConsoleViews
{
  /// <summary>
  /// Консольное представление меню
  /// </summary>
  public class MenuView : MenuViewBase
  {
    /// <summary>
    /// Название игры
    /// </summary>
    private static readonly string[] GAME_TITLE = new[]
    {
        @" _____       _                     _       _____                        _       ",
        @"(___  )     ( )_                  ( )     (___  )                 _    ( )      ",
        @"    | |  __ |  _)_ _     _ _   ___| |/ )      | |  _   _   _ _ __(_)  _| |  __  ",
        @" _  | |/ __ \ | (  _ \ / _  )/ ___)   (    _  | |/ _ \( ) ( )  __) |/ _  |/ __ \",
        @"( )_| |  ___/ |_| (_) ) (_| | (___| |\ \  ( )_| | (_) ) (_) | |  | | (_| |  ___/",
        @" \___/ \____)\__)  __/ \__ _)\____)_) (_)  \___/ \___/ \__  |_)  (_)\__ _)\____)",
        @"                | |                                   ( )_| |                    ",
        @"                (_)                                    \___/                     "

    };

    /// <summary>
    /// Отрисовывает меню
    /// </summary>
    /// <param name="parMenu">Модель меню</param>
    public override void Render(MenuBase parMenu)
    {
      Console.BackgroundColor = ConsoleColor.DarkBlue;
      Console.Clear();
      Console.ForegroundColor = ConsoleColor.Cyan;

      // Вывод названия игры
      int titleY = 1;
      foreach (string elLine in GAME_TITLE)
      {
        Console.SetCursorPosition((ConsoleView.CONSOLE_WIDTH - elLine.Length) / 2, titleY++);
        Console.WriteLine(elLine);
      }

      // Вывод меню
      int menuWidth = parMenu.Items.Max(item => item.Text.Length) + 4;
      int menuX = (ConsoleView.CONSOLE_WIDTH - menuWidth) / 2;
      int menuY = MENU_START_Y;

      foreach (var elItem in parMenu.Items)
      {
        menuY += 2;
        Console.SetCursorPosition(menuX, menuY);

        if (elItem.IsSelected)
        {
          Console.ForegroundColor = ConsoleColor.Yellow;
          Console.Write("> ");
        }
        else
        {
          Console.ForegroundColor = ConsoleColor.Gray;
          Console.Write("  ");
        }

        Console.WriteLine(elItem.Text.PadRight(menuWidth - 2));
      }

      Console.ForegroundColor = ConsoleColor.Gray;
    }

    /// <summary>
    /// Конструктор консольного представления меню
    /// </summary>
    public MenuView()
    {
      if (OperatingSystem.IsWindows())
      {
        Console.SetWindowSize(ConsoleView.CONSOLE_WIDTH, ConsoleView.CONSOLE_HEIGHT);
        Console.SetBufferSize(ConsoleView.CONSOLE_WIDTH, ConsoleView.CONSOLE_HEIGHT);
      }
    }
  }
}

