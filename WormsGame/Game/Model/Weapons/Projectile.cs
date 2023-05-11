using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WormsGame.Base.Weapons
{
  /// <summary>
  /// Снаряд
  /// </summary>
  public abstract class Projectile : PhysicalObject
  {
    /// <summary>
    /// Передаваемая скорость
    /// </summary>
    public int TransmittedSpeed { get; set; }
    /// <summary>
    /// Урон
    /// </summary>
    public int Damage { get; set; }
    /// <summary>
    /// Радиус взрыва
    /// </summary>
    public int ExplosionRadius { get; set; }

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
    public Projectile(
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
          parIsExposedToWind)
    {
      TransmittedSpeed = parTransmittedSpeed;
      Damage = parDamage;
      ExplosionRadius = parExplosionRadius;
    }

    /// <summary>
    /// Обработчик выхода за границы игровой карты
    /// </summary>
    public override void OnGoingOutOfBounds()
    {
      GameModel.RemoveObject(this);
    }

  }
}
