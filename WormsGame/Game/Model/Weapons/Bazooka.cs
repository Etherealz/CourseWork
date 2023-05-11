using System;

namespace WormsGame.Base.Weapons
{
  /// <summary>
  /// Базука
  /// </summary>
  public class Bazooka : Weapon
  {
    /// <summary>
    /// Передаваемая снарядом скорость
    /// </summary>
    private const int PROJECTILE_TRANSMITTED_SPEED = 12;
    /// <summary>
    /// Урон снаряда
    /// </summary>
    private const int PROJECTILE_DAMAGE = 45;
    /// <summary>
    /// Радиус взрыва снаряда
    /// </summary>
    private const int PROJECTILE_EXPLOSION_RADIUS = 75;
    /// <summary>
    /// Подвержен ли снаряд ветру
    /// </summary>
    private const bool IS_PROJECTILE_EXPOSED_TO_WIND = true;
    /// <summary>
    /// Ширина снаряда
    /// </summary>
    private const int PROJECTILE_WIDTH = 20;
    /// <summary>
    /// Высота снаряда
    /// </summary>
    private const int PROJECTILE_HEIGHT = 20;
    /// <summary>
    /// Время на ход после использования оружия
    /// </summary>
    private const int TIME_AFTER_USE = 5;
    /// <summary>
    /// Смещение снаряда от червяка
    /// </summary>
    private const int PROJECTILE_BIAS = 20;

    /// <summary>
    /// Конструктор со всеми аргументами
    /// </summary>
    /// <param name="parUsesNumber">Кол-во использований</param>
    /// <param name="parTransmittedSpeed">Передаваемая скорость</param>
    /// <param name="parDamage">Урон</param>
    /// <param name="parExplosionRadius">Радиус взрыва</param>
    /// <param name="parIsExposedToWind">Подвержен ли ветру</param>
    /// <param name="parTimeAfterUse">Время после ипользования</param>
    public Bazooka(
      int parUsesNumber, 
      int parTransmittedSpeed, 
      int parDamage, 
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
    /// Констуктор с характеристиками по умолчанию
    /// </summary>
    /// <param name="parUsesNumber">Кол-во использований</param>
    public Bazooka(int parUsesNumber)
      : base(
          parUsesNumber, 
          PROJECTILE_TRANSMITTED_SPEED, 
          PROJECTILE_DAMAGE, 
          PROJECTILE_EXPLOSION_RADIUS, 
          IS_PROJECTILE_EXPOSED_TO_WIND, 
          TIME_AFTER_USE)
    {
    }

    /// <summary>
    /// Использовать
    /// </summary>
    /// <param name="parWorm">Червяк</param>
    public override void Use(Worm parWorm)
    {
      if (UsesNumber > 0)
      {
        int ySpeed = (int)(Power * Math.Sin(Angle * Math.PI / 180));
        int xSpeed = (int)(Power * Math.Cos(Angle * Math.PI / 180));
        int yBias = (int)(PROJECTILE_BIAS * Math.Sin(Angle * Math.PI / 180));
        int xBias = (int)(PROJECTILE_BIAS * Math.Cos(Angle * Math.PI / 180));

        int x;
        if (parWorm.State == State.Right)
        {
          x = parWorm.GetCenterX() + xBias - PROJECTILE_WIDTH / 2;
        }
        else
        {
          x = parWorm.GetCenterX() - xBias - PROJECTILE_WIDTH / 2;
          xSpeed = -xSpeed;
        }

        BazookaProjectile projectile = new BazookaProjectile(x, parWorm.GetCenterY() + yBias - PROJECTILE_HEIGHT / 2, xSpeed, ySpeed, PROJECTILE_WIDTH, PROJECTILE_HEIGHT, IS_PROJECTILE_EXPOSED_TO_WIND, TransmittedSpeed, Damage, ExplosionRadius);

        GameModel.AddObject(projectile);
        UsesNumber--;
        SetInitialState();
      }




    }

  }
}
