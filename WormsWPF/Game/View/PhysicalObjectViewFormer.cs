using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;
using WormsGame.Base;
using WormsGame.Base.Weapons;
using WPFWorms.WPFView.ObjectsView;

namespace WPFWorms.WPFView
{
  /// <summary>
  /// Формирователь представления для физичеких объектов
  /// </summary>
  public class PhysicalObjectViewFormer
  {
    /// <summary>
    /// Получить представление для физического объекта
    /// </summary>
    /// <param name="parPhysicalObject">Физический объект</param>
    /// <returns>Представление физического объекта</returns>
    /// <exception cref="ArgumentException"></exception>

    public static PhysicalObjectView GetShape(PhysicalObject parPhysicalObject)
    {
      switch (parPhysicalObject)
      {
        case Worm _:
          return new WormView(parPhysicalObject);
        case BazookaProjectile _:
          return new BazookaProjectileView(parPhysicalObject);
        case GrenadeProjectile _:
          return new GrenadeProjectileView(parPhysicalObject);
        default:
          throw new ArgumentException("Объект отсутствует!");
      }
    }
  }
}
