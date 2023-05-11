using WormsGame.Help.Model;

namespace WormsGame.Help.View
{
  /// <summary>
  /// Представление справки
  /// </summary>
  public abstract class HelpView : BaseMVC.View<HelpModel>
  {
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parModel">Модель</param>
    public HelpView(HelpModel parModel) : base(parModel)
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