using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WormsGame.Help.Model
{
  /// <summary>
  /// Модель справки
  /// </summary>
  public class HelpModel : BaseMVC.Model
  {
    /// <summary>
    /// Событие о необходимости перерисовки
    /// </summary>
    public event dNeedRedraw? NeedRedrawEvent;

    /// <summary>
    /// Текст спрваки
    /// </summary>
    private readonly string _text = Properties.Resources.HelpText;

    /// <summary>
    /// Получить текст справки
    /// </summary>
    /// <returns></returns>
    public string GetHelpText()
    {
      return _text;
    }

    /// <summary>
    /// Вызвать событие о необходимости перерисовки
    /// </summary>
    public void NeedRedraw()
    {
      NeedRedrawEvent?.Invoke();
    }

    /// <summary>
    /// Делегат о необходимости перерисовки
    /// </summary>
    public delegate void dNeedRedraw();
  }
}
