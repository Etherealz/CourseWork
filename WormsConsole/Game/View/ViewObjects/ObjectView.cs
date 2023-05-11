using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WormsConsole.ViewConsole.ViewObjects
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
    /// Нарисовать объект
    /// </summary>
    public abstract void Draw();
  }
}
