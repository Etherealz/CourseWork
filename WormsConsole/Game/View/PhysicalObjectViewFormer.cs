using WormsConsole.ViewConsole.ViewObjects;
using WormsGame.Base;
using WormsGame.Base.Weapons;

namespace WormsConsole.ViewConsole
{
  /// <summary>
  /// Формирователь представлений физических объектов
  /// </summary>
  public class PhysicalObjectViewFormer
  {
    /// <summary>
    /// Получить представление для физического объекта
    /// </summary>
    /// <param name="parPhysicalObject">Физический объект</param>
    /// <returns>Представление физического объекта</returns>
    /// <exception cref="Exception">Исключение при отсутствии представления для физического объекта</exception>
    public static PhysicalObjectView GetShape(PhysicalObject parPhysicalObject)
    {
      switch (parPhysicalObject)
      {
        case Worm _:
          return new WormView((Worm)parPhysicalObject);
        case BazookaProjectile _:
          return new BazookaProjectileView((BazookaProjectile)parPhysicalObject);
        case GrenadeProjectile _:
          return new GrenadeProjectileView((GrenadeProjectile)parPhysicalObject);  
        default:
          throw new Exception($"Отсутствует представление для {parPhysicalObject}"); 
      }
    }
  }
}