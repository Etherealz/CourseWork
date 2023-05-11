using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WormsGame.MainMenu.Model;

namespace WormsGame.MainMenu.View
{
  /// <summary>
  /// Представление главного меню
  /// </summary>
  public abstract class MainMenuView : BaseMVC.View<MainMenuModel>
  {
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parModel">Модель</param>
    public MainMenuView(MainMenuModel parModel) : base(parModel)
    {
      parModel.NeedRedrawEvent += Redraw;
    }

    /// <summary>
    /// Перерисовать
    /// </summary>
    private void Redraw()
    {
      if (IsStarted)
      {
        Draw();
      }
    }
  }
}
