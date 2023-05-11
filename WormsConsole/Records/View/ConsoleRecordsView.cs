using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WormsGame.Records.Model;
using WormsGame.Records.View;

namespace WormsConsole.Records.View
{
  /// <summary>
  /// Консольное представление рекордов
  /// </summary>
  public class ConsoleRecordsView : RecordsView
  {
    /// <summary>
    /// Начальная координата Y для отрисовки представления
    /// </summary>
    private const int Y_START = 5;
    /// <summary>
    /// Расстояние по Y между пунктами в списке рекордов
    /// </summary>
    private const int Y_BIAS = 4;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parModel"></param>
    public ConsoleRecordsView(RecordsModel parModel) : base(parModel)
    {
    }

    /// <summary>
    /// Нарисовать представление
    /// </summary>
    public override void Draw()
    {
      Console.BackgroundColor = ConsoleColor.DarkGray;
      Console.Clear();

      string header = "Рекорды. Нажмите Esc, для возврата в меню.";
      Console.SetCursorPosition(2, 2);
      Console.Write(header);

      List<GameRecord> records = Model.GetRecords();
      for (int i = 0; i < records.Count; i++)
      {
        GameRecord record = records[i];
        
        string textRecord = $"{i + 1}. {record.PlayerName} - {record.Score} сек.";
        Console.SetCursorPosition(Console.WindowWidth / 2 - textRecord.Length / 2, Y_START + i * Y_BIAS);
        Console.Write(textRecord);

      }
    }

    /// <summary>
    /// Запустить
    /// </summary>
    public override void Start()
    {
      Draw();
      base.Start();
    }

    /// <summary>
    /// Остановить
    /// </summary>
    public override void Stop()
    {
      base.Stop();
      Console.Clear();
    }
  }
}
