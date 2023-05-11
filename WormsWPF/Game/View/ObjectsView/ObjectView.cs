using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace WPFWorms.WPFView.ObjectsView
{
  /// <summary>
  /// Представление объекта
  /// </summary>
  public abstract class ObjectView
  {
    /// <summary>
    /// Конструктор
    /// </summary>
    public ObjectView()
    {
    }

    /// <summary>
    /// Нарисовать представление
    /// </summary>
    /// <param name="parCanvas">Канвас</param>
    public abstract void Draw(Canvas parCanvas);
  }
}
