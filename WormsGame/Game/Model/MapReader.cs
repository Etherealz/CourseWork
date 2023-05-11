using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WormsGame.Base
{
  /// <summary>
  /// Считыватель карты
  /// </summary>
  public class MapReader
  {
    /// <summary>
    /// Прочитать карту из файла
    /// </summary>
    /// <param name="parFileName">Название файла</param>
    public static void ReadMapFromFile(string parFileName)
    {
      String line;
      try
      {
        StreamReader sr = new StreamReader(parFileName);
        int i = 0;
        line = sr.ReadLine();
        while (line != null)
        {
          for (int j = 0; j < line.Length; j++)
          {
            GameModel.Map[i, j] = int.Parse(line[j].ToString());
          }

          i++;
          line = sr.ReadLine();
        }
        sr.Close();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }

    }
  }
}
