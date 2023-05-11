using WormsGame.Base;

namespace WormsConsole.ViewConsole.ViewObjects
{
  /// <summary>
  /// Представление физического объекта
  /// </summary>
  public abstract class PhysicalObjectView : ObjectView
  {
    /// <summary>
    /// Физический объект
    /// </summary>
    public PhysicalObject PhysicalObject { get; set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parPhysicalObject">Физический объект</param>
    public PhysicalObjectView(PhysicalObject parPhysicalObject) : base()
    {
      PhysicalObject = parPhysicalObject;
    }


  }
}