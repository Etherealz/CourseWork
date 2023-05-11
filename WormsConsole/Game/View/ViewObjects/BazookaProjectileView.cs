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
  /// Представление снаряда базуки
  /// </summary>
  public class BazookaProjectileView : PhysicalObjectView
  {
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parBazookaProjectile">Снаряд базуки</param>
    public BazookaProjectileView(BazookaProjectile parBazookaProjectile) : base(parBazookaProjectile)
    {
    }

    /// <summary>
    /// Нариовать снаряд базуки
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
