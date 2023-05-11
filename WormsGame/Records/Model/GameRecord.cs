using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WormsGame.Records.Model
{
  /// <summary>
  /// Игровой рекорд
  /// </summary>
  public class GameRecord
  {
    /// <summary>
    /// Имя игрока
    /// </summary>
    public string PlayerName { get; set; }
    /// <summary>
    /// Очки
    /// </summary>
    public int Score { get; set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parPlayerName">Имя игрока</param>
    /// <param name="parScore">Очки</param>
    public GameRecord(string parPlayerName, int parScore)
    {
      PlayerName = parPlayerName;
      Score = parScore;
    }

    /// <summary>
    /// Получить строку, представляющую объект
    /// </summary>
    /// <returns>Строку, представляющую объект</returns>
    public override string ToString()
    {
      return $"{PlayerName} {Score}";
    }
  }
}
