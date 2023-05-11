using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WormsGame.Base.Weapons
{
  /// <summary>
  /// Создатель взрывов
  /// </summary>
  public class ExplosionMaker
  {
    /// <summary>
    /// Множитель радуса взрыва для урона
    /// </summary>
    private const double EXPLOSION_DAMAGE_RADIUS_MULTIPLIER = 1.35;

    /// <summary>
    /// Событие взрыва
    /// </summary>
    public static event Explosion? ExplosionEvent;

    /// <summary>
    /// Создать взрыв
    /// </summary>
    /// <param name="parX">X центра взрыва</param>
    /// <param name="parY">Y центра взрыва</param>
    /// <param name="parRadius">Радиус взрыва</param>
    /// <param name="parTransmittedSpeed">Передаваемая скорость</param>
    /// <param name="parDamage">Урон</param>
    public static void MakeExplosion(int parX, int parY, int parRadius, int parTransmittedSpeed, int parDamage)
    {
      ExplosionEvent?.Invoke(parX, parY, parRadius);
      MapDestruction(parX, parY, parRadius);
      WormsInfluence(parX, parY, parRadius, parTransmittedSpeed, parDamage);
    }

    /// <summary>
    /// Разрушить карту
    /// </summary>
    /// <param name="parX">X центра взрыва</param>
    /// <param name="parY">Y центра взрыва</param>
    /// <param name="parRadius">Радиус взрыва</param>
    private static void MapDestruction(int parX, int parY, int parRadius)
    {
      for (int i = parX - parRadius; i < parX + parRadius; i++)
      {
        for (int j = parY - parRadius; j < parY + parRadius; j++)
        {
          if (i > 0 && i < 1920 && j > 0 && j < 1080)
          {
            if (IsInExplosion(i, j, parX, parY, parRadius))
            {
              GameModel.Map[j, i] = 0;
            }
          }

        }
      }
    }

    /// <summary>
    /// Проявить воздействие на червяков
    /// </summary>
    /// <param name="parX">X центра взрыва</param>
    /// <param name="parY">Y центра взрыва</param>
    /// <param name="parRadius">Радиус взрыва</param>
    /// <param name="parTransmittedSpeed">Передаваемая скорость</param>
    /// <param name="parDamage">Урон</param>
    private static void WormsInfluence(int parX, int parY, int parRadius, int parTransmittedSpeed, int parDamage)
    {
      List<Worm> worms = GameModel.GetAllWorms();

      for (int i = 0; i < worms.Count; i++)
      {
        int distance = (int)GetDistance(parX, parY, worms[i].GetCenterX(), worms[i].GetCenterY());

        if (distance <= parRadius * EXPLOSION_DAMAGE_RADIUS_MULTIPLIER)
        {
          GameModel.AddDamagedWorm(worms[i]);
          int damage = (int)Math.Ceiling(parDamage * (1 - (double)distance / (parRadius * 1.4)));
          int xSpeed;
          int ySpeed;
          if (distance != 0)
          {
            ySpeed = parTransmittedSpeed * (parY - worms[i].GetCenterY()) / distance;
            xSpeed = parTransmittedSpeed * (parX -worms[i].GetCenterX()) / distance;
          }
          else
          {
            xSpeed = 0;
            ySpeed = parTransmittedSpeed;
          }

          worms[i].GainedDamage += damage;
          worms[i].YSpeed = -ySpeed;
          worms[i].XSpeed = -xSpeed;

          if (worms[i].Health <= 0)
          {
            worms[i].Player.Worms.Remove(worms[i]);
          }

          if (GameModel._isSelectedWormControlling && GameModel.GetCurrentWorm().Equals(worms[i]))
          {
            GameModel.RemoveControl();
            GameModel.WaitingForChangingTurn();
          }

        }
      }
    }

    /// <summary>
    /// Получить расстояние между тчоками
    /// </summary>
    /// <param name="parX1">X 1 точки</param>
    /// <param name="parY1">Y 1 точки</param>
    /// <param name="parX2">X 2 точки</param>
    /// <param name="parY2">Y 2 точки</param>
    /// <returns>Расстояние</returns>
    private static double GetDistance(int parX1, int parY1, int parX2, int parY2)
    {
      return Math.Sqrt(Math.Pow(parX2 - parX1, 2) + Math.Pow(parY2 - parY1, 2));
    }

    /// <summary>
    /// Находится ли точка в радиусе взрыва
    /// </summary>
    /// <param name="parXPoint">X точки</param>
    /// <param name="parYPoint">Y точки</param>
    /// <param name="parXExplosion">X центра взрыва</param>
    /// <param name="parYExplosion">Y центра взрыва</param>
    /// <param name="parRadius">Радиус взрыва</param>
    /// <returns>Находится ли точка в взрыве</returns>
    public static bool IsInExplosion(int parXPoint, int parYPoint, int parXExplosion, int parYExplosion, int parRadius)
    {
      return Math.Pow(parXPoint - parXExplosion, 2) + Math.Pow(parYPoint - parYExplosion, 2) <= parRadius * parRadius;
    }

    /// <summary>
    /// Делегат на взрыв
    /// </summary>
    /// <param name="parX">X центра взрыва</param>
    /// <param name="parY">Y центра взрыва</param>
    /// <param name="parRadius">Радиус взрыва</param>
    public delegate void Explosion(int parX, int parY, int parRadius);

  }
}
