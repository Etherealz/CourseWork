using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WormsGame.Records.Model;

namespace WormsGame.Records
{
  /// <summary>
  /// Репозиторий рекордов
  /// </summary>
  public class RecordsRepository
  {
    /// <summary>
    /// Название файла
    /// </summary>
    private const string FILENAME = "records.txt";

    /// <summary>
    /// Держатель единственного экземпляра класса 
    /// </summary>
    private static readonly Lazy<RecordsRepository> instanceHolder =
        new Lazy<RecordsRepository>(() => new RecordsRepository());
    
    /// <summary>
    /// Приватный конструктор
    /// </summary>
    private RecordsRepository()
    {
    }

    /// <summary>
    /// Единственный экземпляр класса
    /// </summary>
    public static RecordsRepository Instance
    {
      get { return instanceHolder.Value; }
    }

    /// <summary>
    /// Является ли рекордом данное кол-во очков
    /// </summary>
    /// <param name="parScore">Очки</param>
    /// <returns></returns>
    public bool IsRecord(int parScore)
    {
      List<GameRecord> records = GetAllRecords();

      if (records.Count < 10)
      {
        return true;
      }

      foreach (GameRecord record in records)
      {
        if (record.Score > parScore)
        {
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Сохранить рекорд
    /// </summary>
    /// <param name="parRecord"></param>
    public void SaveRecord(GameRecord parRecord)
    {
      List<GameRecord> records = GetAllRecords();
      records.Add(parRecord);
      SortRecords(records);
      if (records.Count > 10)
      {
        SaveRecords(records.GetRange(0, 10));
      }
      else
      {
        SaveRecords(records);
      }
      

    }

    /// <summary>
    /// Сохранить рекорды
    /// </summary>
    /// <param name="parRecords"></param>
    public void SaveRecords(List<GameRecord> parRecords)
    {

      if (!File.Exists(FILENAME))
      {
        File.Create(FILENAME);
      }

      StreamWriter sw = new StreamWriter(FILENAME);
      foreach (GameRecord record in parRecords)
      {
        sw.WriteLine(record.ToString());
      }
      sw.Close();
    }

    /// <summary>
    /// Получить все рекорды
    /// </summary>
    /// <returns></returns>
    public List<GameRecord> GetAllRecords()
    {
      List<GameRecord> result = new List<GameRecord>();
      if (!File.Exists(FILENAME))
      {
        File.Create(FILENAME);
        return new List<GameRecord>();
      }

      StreamReader sr = new StreamReader(FILENAME);
      string recordLine = sr.ReadLine();
      while (recordLine != null)
      {
        string[] splittedRecordLine = recordLine.Split(' ');
        result.Add(new GameRecord(splittedRecordLine[0], int.Parse(splittedRecordLine[1])));
        recordLine = sr.ReadLine();
      }
      sr.Close();

      return result;
    }

    /// <summary>
    /// Сортировать рекорды
    /// </summary>
    /// <param name="parRecords"></param>
    private void SortRecords(List<GameRecord> parRecords)
    {
      parRecords.Sort((GameRecord a, GameRecord b) =>
      {
        return a.Score.CompareTo(b.Score);
      });
    }

  }
}
