using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WormsGame.Base.Weapons
{
  /// <summary>
  /// Снаряд гранаты
  /// </summary>
  public class GrenadeProjectile : Projectile
  {
    /// <summary>
    /// Время до взрыва
    /// </summary>
    private const int GRENADE_TIME_BEFORE_EXPLOSION = 3;

    /// <summary>
    /// Таймер взрыва гранаты
    /// </summary>
    private System.Timers.Timer GrenadeTimer { get; set; }
    /// <summary>
    /// Оставшееся до взрыва время
    /// </summary>
    public int TimeLast { get; set; }

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
    public GrenadeProjectile(
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
      GrenadeTimer = new System.Timers.Timer();
      TimeLast = GRENADE_TIME_BEFORE_EXPLOSION;

      GrenadeTimer.Interval = 1000;
      GrenadeTimer.Elapsed += CountDown;
      GrenadeTimer.AutoReset = true;
      GrenadeTimer.Enabled = true;
    }

    /// <summary>
    /// Обработчик отсчета таймера
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    public void CountDown(object source, System.Timers.ElapsedEventArgs e)
    {
      TimeLast--;
      if (TimeLast <= 0)
      {
        GrenadeTimer.Stop();
        ExplosionMaker.MakeExplosion(GetCenterX(), GetCenterY(), ExplosionRadius, TransmittedSpeed, Damage);
        GameModel.RemoveObject(this);
      }
    }

    /// <summary>
    /// Обработчик столкновения с картой
    /// </summary>
    public override void OnCollapse()
    {
      XSpeed = (int)(-XSpeed * 0.7);
      YSpeed = (int)(-YSpeed * 0.7);
    }
  }
}
