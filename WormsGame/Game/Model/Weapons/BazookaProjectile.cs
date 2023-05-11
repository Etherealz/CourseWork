using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WormsGame.Base.Weapons
{
  /// <summary>
  /// Снаряд базуки
  /// </summary>
  public class BazookaProjectile : Projectile
  {
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parX">X</param>
    /// <param name="parY">Y</param>
    /// <param name="parXSpeed">Скорость по X</param>
    /// <param name="parYSpeed">Скорость по Y</param>
    /// <param name="parWidth">Ширина</param>
    /// <param name="parHeight">Высота</param>
    /// <param name="parIsExposedToWind">Подвержен ли ветру</param>
    /// <param name="parTransmittedSpeed">Передаваемая скорость</param>
    /// <param name="parDamage">Урон</param>
    /// <param name="parExplosionRadius">Радиус взрыва</param>
    public BazookaProjectile(
      int parX, 
      int parY, 
      int parXSpeed, 
      int parYSpeed, 
      int parWidth, 
      int parHeight, 
      bool parIsExposedToWind, 
      int parTransmittedSpeed, 
      int parDamage, 
      int parExplosionRadius)
      : base(
          parX, 
          parY, 
          parXSpeed, 
          parYSpeed, 
          parWidth, 
          parHeight, 
          parIsExposedToWind, 
          parTransmittedSpeed, 
          parDamage, 
          parExplosionRadius)
    {
    }

    /// <summary>
    /// Обработчик столкновения с картой
    /// </summary>
    public override void OnCollapse()
    {
      ExplosionMaker.MakeExplosion(GetCenterX(), GetCenterY(), ExplosionRadius, TransmittedSpeed, Damage);
      GameModel.RemoveObject(this);
    }
  }
}
