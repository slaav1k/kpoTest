using System.Text.Json;


namespace Base.Models
{
  /// <summary>
  /// Менеджер таблицы рекордов.
  /// Управляет загрузкой, сохранением и добавлением новых рекордов в таблицу.
  /// </summary>
  public class HighScoresManager
  {
    /// <summary>
    /// Список рекордов.
    /// </summary>
    private readonly List<HighScoreRecord> _scores;

    /// <summary>
    /// Папка, в которой сохраняются рекорды.
    /// </summary>
    private readonly string _directoryPath = Path.Combine(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..")), "Files");

    /// <summary>
    /// Название файла для рекордов.
    /// </summary>
    private readonly string _fileName = "highscores.txt";

    /// <summary>
    /// Путь к файлу, в котором сохраняются рекорды.
    /// </summary>
    private readonly string _filePath;

    /// <summary>
    /// Инициализирует новый экземпляр менеджера таблицы рекордов.
    /// Загружает рекорды из файла при создании.
    /// </summary>
    public HighScoresManager()
    {
      // Формируем путь к файлу с рекордами
      _filePath = Path.Combine(_directoryPath, _fileName);

      // Убедимся, что папка Files существует
      if (!Directory.Exists(_directoryPath))
      {
        Directory.CreateDirectory(_directoryPath);
      }

      // Загружаем рекорды из файла
      _scores = LoadScores();
    }

    /// <summary>
    /// Получает список рекордов.
    /// Загружает актуальные данные рекордов перед возвратом.
    /// </summary>
    /// <returns>Список рекордов</returns>
    public IReadOnlyList<HighScoreRecord> GetScores()
    {
      _scores.Clear();
      _scores.AddRange(LoadScores());
      return _scores;
    }

    /// <summary>
    /// Проверяет, является ли счет новым рекордом.
    /// </summary>
    /// <param name="parScore">Количество очков</param>
    /// <returns>true, если счет является новым рекордом</returns>
    public bool IsNewHighScore(int parScore)
    {
      var scores = LoadScores();
      return scores.Count < 5 || parScore > scores.Last().Score;
    }

    /// <summary>
    /// Добавляет новый рекорд.
    /// </summary>
    /// <param name="parName">Имя игрока</param>
    /// <param name="parScore">Количество очков</param>
    public void AddScore(string parName, int parScore)
    {
      if (string.IsNullOrEmpty(parName))
      {
        parName = "Гость";
      }

      var scores = LoadScores();
      scores.Add(new HighScoreRecord(parName, parScore));
      scores.Sort((a, b) => b.Score.CompareTo(a.Score));
      if (scores.Count > 5)
      {
        scores.RemoveAt(5);
      }
      SaveScores(scores);
      _scores.Clear();
      _scores.AddRange(scores);
    }

    /// <summary>
    /// Загружает рекорды из файла.
    /// </summary>
    /// <returns>Список загруженных рекордов</returns>
    public List<HighScoreRecord> LoadScores()
    {
      if (!File.Exists(_filePath))
      {
        return new List<HighScoreRecord>();
      }

      try
      {
        using (var reader = new StreamReader(_filePath))
        {
          var json = reader.ReadToEnd();
          return JsonSerializer.Deserialize<List<HighScoreRecord>>(json) ?? new List<HighScoreRecord>();
        }
      }
      catch
      {
        return new List<HighScoreRecord>();
      }
    }

    /// <summary>
    /// Сохраняет рекорды в файл.
    /// </summary>
    /// <param name="parScores">Список рекордов для сохранения</param>
    public void SaveScores(List<HighScoreRecord> parScores)
    {
      try
      {
        // Убедимся, что папка Files существует
        string directory = Path.GetDirectoryName(_filePath);
        if (!Directory.Exists(directory))
        {
          Directory.CreateDirectory(directory);
        }

        using (var writer = new StreamWriter(_filePath))
        {
          var json = JsonSerializer.Serialize(parScores);
          writer.Write(json);
        }
      }
      catch
      {
        // Игнорируем ошибки записи
      }
    }
  }

}
