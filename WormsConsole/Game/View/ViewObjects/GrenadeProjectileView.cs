using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WormsGame.Base;
using WormsGame.Base.Weapons;

namespace WormsConsole.ViewConsole.ViewObjects
{
  /// <summary>
  /// Представление гранаты
  /// </summary>
  public class GrenadeProjectileView : PhysicalObjectView
  {
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parGrenadeProjectile">Граната</param>
    public GrenadeProjectileView(GrenadeProjectile parGrenadeProjectile) : base(parGrenadeProjectile)
    {
    }

    /// <summary>
    /// Нарисовать гранату
    /// </summary>
    public override void Draw()
    {
      for (int i = 0; i < PhysicalObject.Width / ConsoleGameView.COMPRESSION_RATIO + 1; i++)
      {
        for (int j = 0; j < PhysicalObject.Height / ConsoleGameView.COMPRESSION_RATIO + 1; j++)
        {
          FastConsoleWorker.SetPixel(PhysicalObject.X / ConsoleGameView.COMPRESSION_RATIO + i, PhysicalObject.Y / ConsoleGameView.COMPRESSION_RATIO + j, 0);
        }
      }
    }
  }
}
