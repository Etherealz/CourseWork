using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using WormsGame.Base;

namespace WPFWorms.WPFView.ObjectsView
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
