using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WormsGame.Records.Model;

namespace WormsGame.Records.View
{
  /// <summary>
  /// Представление рекордов
  /// </summary>
  public abstract class RecordsView : BaseMVC.View<RecordsModel>
  {
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parModel"></param>
    public RecordsView(RecordsModel parModel) : base(parModel)
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
