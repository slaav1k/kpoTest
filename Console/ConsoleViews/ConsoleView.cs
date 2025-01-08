using Base.Models.Obstacles;
using Base.Models;
using Base.Views;
using System.Runtime.InteropServices;

namespace Consolee.ConsoleViews
{
  /// <summary>
  /// Консольное представление игры
  /// </summary>
  public class ConsoleView : GameViewBase
  {
    /// <summary>
    /// Ширина консольного окна
    /// </summary>
    public const int CONSOLE_WIDTH = 80;

    /// <summary>
    /// Высота консольного окна
    /// </summary>
    public const int CONSOLE_HEIGHT = 20;

    /// <summary>
    /// Символ, представляющий игрока
    /// </summary>
    private const char PLAYER_CHAR = '@';

    /// <summary>
    /// Символ, представляющий препятствия
    /// </summary>
    private const char OBSTACLE_CHAR = '#';

    /// <summary>
    /// Символ, представляющий монеты
    /// </summary>
    private const char COIN_CHAR = 'O';

    /// <summary>
    /// Символ, представляющий врага
    /// </summary>
    private const char ENEMY_CHAR = 'X';

    /// <summary>
    /// Символ, представляющий пули
    /// </summary>
    private const char BULLET_CHAR = '*';

    /// <summary>
    /// Символ, представляющий пули игрока
    /// </summary>
    private const char PLAYER_BULLET_CHAR = '-';

    /// <summary>
    /// Символ, представляющий хвост игрока
    /// </summary>
    private const char PLAYER_TAIL = '>';

    /// <summary>
    /// Символ для пустого пространства
    /// </summary>
    private const char EMPTY_CHAR = ' ';

    /// <summary>
    /// Буфер для отображения консольного вывода
    /// </summary>
    private readonly CHAR_INFO[] _buffer;

    /// <summary>
    /// Прямоугольник, определяющий область для записи в консоль
    /// </summary>
    private SMALL_RECT _rect;

    /// <summary>
    /// Размеры буфера
    /// </summary>
    private readonly COORD _bufferSize;

    /// <summary>
    /// Координаты начала буфера
    /// </summary>
    private readonly COORD _bufferCoord;

    /// <summary>
    /// Стандартный дескриптор вывода консоли
    /// </summary>
    private readonly IntPtr _stdHandle;

    /// <summary>
    /// Флаг, указывающий на паузу игры
    /// </summary>
    private bool _isPaused;


    /// <summary>
    /// Инициализирует новый экземпляр консольного представления
    /// </summary>
    public ConsoleView()
    {
      Console.CursorVisible = false;
      System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
      Console.OutputEncoding = System.Text.Encoding.GetEncoding(866);

      if (OperatingSystem.IsWindows())
      {
        Console.SetWindowSize(CONSOLE_WIDTH, CONSOLE_HEIGHT);
        Console.SetBufferSize(CONSOLE_WIDTH, CONSOLE_HEIGHT);
      }

      _buffer = new CHAR_INFO[CONSOLE_WIDTH * CONSOLE_HEIGHT];
      _rect = new SMALL_RECT { Left = 0, Top = 0, Right = CONSOLE_WIDTH - 1, Bottom = CONSOLE_HEIGHT - 1 };
      _bufferSize = new COORD { X = CONSOLE_WIDTH, Y = CONSOLE_HEIGHT };
      _bufferCoord = new COORD { X = 0, Y = 0 };
      _stdHandle = GetStdHandle(STD_OUTPUT_HANDLE);
    }

    /// <summary>
    /// Отрисовывает состояние игры
    /// </summary>
    /// <param name="parGameState">Состояние игры</param>
    public override void Render(GameStateBase parGameState)
    {
      if (!_isPaused)  // Перерисовываем игру только если не на паузе
      {
        ClearBuffer();
        DrawObstacles(parGameState);
        DrawPlayer(parGameState);
        DrawScore(parGameState);
        WriteConsoleOutput(_stdHandle, _buffer, _bufferSize, _bufferCoord, ref _rect);
      }
    }

    /// <summary>
    /// Очищает буфер консоли
    /// </summary>
    private void ClearBuffer()
    {
      for (int i = 0; i < _buffer.Length; i++)
      {
        _buffer[i].Char.UnicodeChar = EMPTY_CHAR;
        _buffer[i].Attributes = (short)ConsoleColor.Gray;
      }
    }

    /// <summary>
    /// Отрисовывает препятствия
    /// </summary>
    /// <param name="parGameState">Состояние игры</param>
    private void DrawObstacles(GameStateBase parGameState)
    {
      foreach (var obstacle in parGameState.Obstacles)
      {
        int obstacleX = (int)obstacle.X;
        if (obstacleX >= 0 && obstacleX < CONSOLE_WIDTH)
        {
          for (int y = 0; y < CONSOLE_HEIGHT; y++)
          {
            if (obstacle.IsPartOfObstacle(y))
            {
              int index = y * CONSOLE_WIDTH + obstacleX;
              char charToDraw = obstacle.Type switch
              {
                BaseObstacle.ObstacleTypes.Coin => COIN_CHAR,
                BaseObstacle.ObstacleTypes.Enemy => ENEMY_CHAR,
                BaseObstacle.ObstacleTypes.Bullet => BULLET_CHAR,
                BaseObstacle.ObstacleTypes.PlayerBullet => PLAYER_BULLET_CHAR,
                _ => OBSTACLE_CHAR
              };
              _buffer[index].Char.UnicodeChar = charToDraw;

              _buffer[index].Attributes = (short)(obstacle.Type switch
              {
                BaseObstacle.ObstacleTypes.Coin => ConsoleColor.Yellow,
                BaseObstacle.ObstacleTypes.Enemy => ConsoleColor.Red,
                BaseObstacle.ObstacleTypes.Bullet => ConsoleColor.DarkYellow,
                _ => ConsoleColor.White
              });
            }
          }
        }
      }
    }

    /// <summary>
    /// Отрисовывает игрока
    /// </summary>
    /// <param name="parGameState">Состояние игры</param>
    private void DrawPlayer(GameStateBase parGameState)
    {
      int playerX = (int)parGameState.PlayerX;
      int playerY = (int)parGameState.PlayerY;

      // Корректируем позицию игрока, чтобы он не исчезал внизу экрана
      playerY = Math.Min(playerY, CONSOLE_HEIGHT - 2);

      if (playerX >= 0 && playerX < CONSOLE_WIDTH &&
          playerY >= 0 && playerY < CONSOLE_HEIGHT - 1)
      {
        // Отрисовка верхней части игрока (теперь это основная позиция)
        int topIndex = playerY * CONSOLE_WIDTH + playerX;
        _buffer[topIndex].Char.UnicodeChar = PLAYER_CHAR;
        _buffer[topIndex].Attributes = (short)ConsoleColor.Yellow;

        // Отрисовка нижней части игрока
        int bottomIndex = (playerY + 1) * CONSOLE_WIDTH + playerX;
        _buffer[bottomIndex].Char.UnicodeChar = PLAYER_CHAR;
        _buffer[bottomIndex].Attributes = (short)ConsoleColor.Yellow;

        // Отрисовка хвоста
        if (playerX > 0)
        {
          int topTailIndex = playerY * CONSOLE_WIDTH + playerX - 1;
          int bottomTailIndex = (playerY + 1) * CONSOLE_WIDTH + playerX - 1;
          _buffer[topTailIndex].Char.UnicodeChar = PLAYER_TAIL;
          _buffer[topTailIndex].Attributes = (short)ConsoleColor.Yellow;
          _buffer[bottomTailIndex].Char.UnicodeChar = PLAYER_TAIL;
          _buffer[bottomTailIndex].Attributes = (short)ConsoleColor.Yellow;
        }
      }
    }

    /// <summary>
    /// Отрисовывает счет
    /// </summary>
    /// <param name="parGameState">Состояние игры</param>
    private void DrawScore(GameStateBase parGameState)
    {
      string score = $"Score: {parGameState.Score}";
      for (int i = 0; i < score.Length; i++)
      {
        _buffer[i + CONSOLE_WIDTH - score.Length - 1].Char.UnicodeChar = score[i];
      }
    }

    /// <summary>
    /// Импортирует функцию из kernel32.dll для получения стандартного дескриптора вывода.
    /// </summary>
    /// <param name="nStdHandle">Идентификатор стандартного дескриптора.</param>
    /// <returns>Дескриптор стандартного устройства вывода.</returns>
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GetStdHandle(int nStdHandle);

    /// <summary>
    /// Импортирует функцию из kernel32.dll для записи данных в консольный вывод.
    /// </summary>
    /// <param name="hConsoleOutput">Дескриптор вывода консоли.</param>
    /// <param name="lpBuffer">Буфер данных для записи в консоль.</param>
    /// <param name="dwBufferSize">Размер буфера данных.</param>
    /// <param name="dwBufferCoord">Координаты начала записи.</param>
    /// <param name="lpWriteRegion">Область вывода для записи.</param>
    /// <returns>Возвращает true, если запись успешна, иначе false.</returns>
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool WriteConsoleOutput(
        IntPtr hConsoleOutput,
        CHAR_INFO[] lpBuffer,
        COORD dwBufferSize,
        COORD dwBufferCoord,
        ref SMALL_RECT lpWriteRegion);

    /// <summary>
    /// Идентификатор стандартного дескриптора вывода.
    /// </summary>
    private const int STD_OUTPUT_HANDLE = -11;

    /// <summary>
    /// Структура, представляющая координаты в консоли (X, Y).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    private struct COORD
    {
      /// <summary>
      /// Координата X.
      /// </summary>
      public short X;

      /// <summary>
      /// Координата Y.
      /// </summary>
      public short Y;
    }

    /// <summary>
    /// Структура, представляющая прямоугольник для записи в консоль.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    private struct SMALL_RECT
    {
      /// <summary>
      /// Левая граница прямоугольника.
      /// </summary>
      public short Left;

      /// <summary>
      /// Верхняя граница прямоугольника.
      /// </summary>
      public short Top;

      /// <summary>
      /// Правая граница прямоугольника.
      /// </summary>
      public short Right;

      /// <summary>
      /// Нижняя граница прямоугольника.
      /// </summary>
      public short Bottom;
    }

    /// <summary>
    /// Структура, представляющая символ и его атрибуты для записи в консоль.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    private struct CHAR_INFO
    {
      /// <summary>
      /// Символ, который будет отображаться в консоли.
      /// </summary>
      public CharUnion Char;

      /// <summary>
      /// Атрибуты (цвет) символа.
      /// </summary>
      public short Attributes;
    }

    /// <summary>
    /// Структура, представляющая объединение для хранения символа в формате Unicode или ASCII.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    private struct CharUnion
    {
      /// <summary>
      /// Символ в формате Unicode.
      /// </summary>
      [FieldOffset(0)] public char UnicodeChar;

      /// <summary>
      /// Символ в формате ASCII.
      /// </summary>
      [FieldOffset(0)] public byte AsciiChar;
    }


    /// <summary>
    /// Отображает экран окончания игры
    /// </summary>
    /// <param name="parFinalScore">Финальный счет</param>
    public override void ShowGameOver(int parFinalScore)
    {
      ResetConsole();
      Console.BackgroundColor = ConsoleColor.Black;
      Console.Clear();
      Console.SetCursorPosition(CONSOLE_WIDTH / 2 - 5, CONSOLE_HEIGHT / 2);
      Console.Write("GAME OVER");
      Console.SetCursorPosition(CONSOLE_WIDTH / 2 - 6, CONSOLE_HEIGHT / 2 + 1);
      Console.Write($"Ваш счет: {parFinalScore}");
      Console.ReadKey();
    }

    /// <summary>
    /// Сбрасывает настройки консоли
    /// </summary>
    public void ResetConsole()
    {
      Console.CursorVisible = true;
    }

    /// <summary>
    /// Показывает меню паузы
    /// </summary>
    public void ShowPauseMenu()
    {
      _isPaused = true;
      Console.SetCursorPosition(35, 10);
      Console.Write("ПАУЗА");
      Console.SetCursorPosition(25, 12);
      Console.Write("P - Продолжить, ESC - Выход в меню");
    }

    /// <summary>
    /// Скрывает меню паузы
    /// </summary>
    public void HidePauseMenu()
    {
      _isPaused = false;
    }
  }

}

