using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WormsGame.Base.Weapons
{
  /// <summary>
  /// Граната
  /// </summary>
  public class Grenade : Weapon
  {
    /// <summary>
    /// Передаваемая снарядом скорость
    /// </summary>
    private const int PROJECTILE_TRANSMITTED_SPEED = 14;
    /// <summary>
    /// Наносимый снарядом урон
    /// </summary>
    private const int PROJECTILE_DAMAGE = 55;
    /// <summary>
    /// Радиус взрыва
    /// </summary>
    private const int PROJECTILE_EXPLOSION_RADIUS = 50;
    /// <summary>
    /// Подвержен ли ветру снаряд
    /// </summary>
    private const bool IS_PROJECTILE_EXPOSED_TO_WIND = false;
    /// <summary>
    /// Ширина снаряда
    /// </summary>
    private const int PROJECTILE_WIDTH = 15;
    /// <summary>
    /// Высота снаряда
    /// </summary>
    private const int PROJECTILE_HEIGHT = 15;
    /// <summary>
    /// Время после использования
    /// </summary>
    private const int TIME_AFTER_USE = 5;

    /// <summary>
    /// Конструктор со всеми параметрами
    /// </summary>
    /// <param name="parUsesNumber">Кол-во использований</param>
    /// <param name="parTransmittedSpeed">Передаваемая скорость</param>
    /// <param name="parDamage">Урон</param>
    /// <param name="parExplosionRadius">Радиус взрыва</param>
    /// <param name="parIsExposedToWind">Подвержен ли ветру снаряд</param>
    /// <param name="parTimeAfterUse">Время после использования</param>
    public Grenade(
      int parUsesNumber, 
      int parTransmittedSpeed, int parDamage, 
      int parExplosionRadius, 
      bool parIsExposedToWind, 
      int parTimeAfterUse)
      : base(
          parUsesNumber, 
          parTransmittedSpeed, 
          parDamage, 
          parExplosionRadius, 
          parIsExposedToWind, 
          parTimeAfterUse)
    {
    }

    /// <summary>
    /// Конструктор с характеристиками по умолчанию
    /// </summary>
    /// <param name="parUsesNumber">Кол-во использований</param>
    public Grenade(int parUsesNumber) : base(parUsesNumber, PROJECTILE_TRANSMITTED_SPEED, PROJECTILE_DAMAGE, PROJECTILE_EXPLOSION_RADIUS, IS_PROJECTILE_EXPOSED_TO_WIND, TIME_AFTER_USE)
    {
    }

    /// <summary>
    /// Использовать
    /// </summary>
    /// <param name="parWorm">Червяк</param>
    public override void Use(Worm parWorm)
    {
      int ySpeed = (int)(Power * Math.Sin(Angle * Math.PI / 180));
      int xSpeed = (int)(Power * Math.Cos(Angle * Math.PI / 180));
      int x = parWorm.GetCenterX() - PROJECTILE_WIDTH / 2;
      int y = parWorm.GetCenterY() - PROJECTILE_HEIGHT / 2;

      if (parWorm.State == State.Left)
      {
        xSpeed = -xSpeed;
      }

      GrenadeProjectile projectile = new GrenadeProjectile(x, y, xSpeed, ySpeed, PROJECTILE_WIDTH, PROJECTILE_HEIGHT, IS_PROJECTILE_EXPOSED_TO_WIND, PROJECTILE_TRANSMITTED_SPEED, PROJECTILE_DAMAGE, PROJECTILE_EXPLOSION_RADIUS);

      GameModel.AddObject(projectile);
      UsesNumber--;
      SetInitialState();
    }
  }
}
