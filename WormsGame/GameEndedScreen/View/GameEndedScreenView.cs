using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WormsGame.GameEndedScreen.Model;

namespace WormsGame.GameEndedScreen.View
{
  /// <summary>
  /// Представление экрана конца игры
  /// </summary>
  public abstract class GameEndedScreenView : BaseMVC.View<GameEndedScreenModel>
  {
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parModel">Модель</param>
    public GameEndedScreenView(GameEndedScreenModel parModel) : base(parModel)
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
