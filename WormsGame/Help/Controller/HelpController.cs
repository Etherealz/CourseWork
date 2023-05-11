using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WormsGame.Help.Model;
using WormsGame.Help.View;

namespace WormsGame.Help.Controller
{
  /// <summary>
  /// Контроллер справки
  /// </summary>
  public abstract class HelpController : BaseMVC.Controller<HelpModel, HelpView>
  {
    /// <summary>
    /// Событие возвращения в меню
    /// </summary>
    public event dBackToMenu? BackToMenuEvent;

    /// <summary>
    /// Контроллер
    /// </summary>
    /// <param name="parModel">Модель</param>
    /// <param name="parView">Представление</param>
    public HelpController(HelpModel parModel, HelpView parView) : base(parModel, parView)
    {

    }

    /// <summary>
    /// Вернуться в меню
    /// </summary>
    public void BackToMenu()
    {
      BackToMenuEvent?.Invoke();
    }
    
    /// <summary>
    /// Делегат на возвращение в меню
    /// </summary>
    public delegate void dBackToMenu();
  }
}
