using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WormsGame.MainMenu.Model
{
  /// <summary>
  /// Формирователь текста для пунктов меню
  /// </summary>
  public class MenuItemTextFormer
  {
    /// <summary>
    /// Получить текст для пункта меню
    /// </summary>
    /// <param name="parMenuItem">Пункт меню</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static string GetTitle(MenuItem parMenuItem)
    {
      return parMenuItem switch
      {
        MenuItem.Game => "Играть",
        MenuItem.Records => "Рекорды",
        MenuItem.Help => "Справка",
        MenuItem.Exit => "Выход",
        _ => throw new NotImplementedException()
      };
    }
  }
}
