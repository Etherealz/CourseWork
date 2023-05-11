using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WormsGame.Base;

namespace WormsConsole.ViewConsole.ViewObjects
{
  /// <summary>
  /// Представление червяка
  /// </summary>
  public class WormView : PhysicalObjectView
  {
    /// <summary>
    /// Червяк
    /// </summary>
    public Worm Worm { get; set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parWorm">Червяк</param>
    public WormView(Worm parWorm) : base(parWorm)
    {
      Worm = parWorm;
    }

    /// <summary>
    /// Нарисовать червяка
    /// </summary>
    public override void Draw()
    {
      DrawWorm();
      DrawHealth();
      if (Worm.Equals(GameModel.GetCurrentWorm()))
      {
        DrawWormPointer();
        DrawWormWeapon();
      }
    }

    /// <summary>
    /// Нарисовать оружие червяка
    /// </summary>
    private void DrawWormWeapon()
    {
      if (Worm.Weapon != null)
      {
        DrawAim();
        if (Worm.Weapon.Power > Weapon.INITIAL_POWER)
        {
          DrawWeaponPower();
        }

      }

    }

    /// <summary>
    /// Нарисовать заряд оружия
    /// </summary>
    private void DrawWeaponPower()
    {
      int len = 12;

      for (int k = 0; k < Math.Ceiling(Worm.Weapon.Power / Weapon.MAX_POWER * len); k++)
      {
        for (int j = 0; j < 2; j++)
        {
          if (Worm.State == State.Right)
          {
            FastConsoleWorker.SetPixel(Worm.X / ConsoleGameView.COMPRESSION_RATIO + k - 2, Worm.GetCenterY() / ConsoleGameView.COMPRESSION_RATIO + j + 5, 12);
          }
          else
          {
            FastConsoleWorker.SetPixel(Worm.X / ConsoleGameView.COMPRESSION_RATIO + k - 2, Worm.GetCenterY() / ConsoleGameView.COMPRESSION_RATIO + j + 5, 12);
          }

        }
      }
        
    }

    /// <summary>
    /// Нарисовать прицел
    /// </summary>
    private void DrawAim()
    {
      int len = 12;
      int y = (int)(len * Math.Sin(Worm.Weapon.Angle * Math.PI / 180));
      int x = (int)(len * Math.Cos(Worm.Weapon.Angle * Math.PI / 180));

      for (int i = 0; i < 2; i++)
      {
        for (int j = 0; j < 2; j++)
        {
          if (Worm.State == State.Right)
          {
            FastConsoleWorker.SetPixel(Worm.GetCenterX() / ConsoleGameView.COMPRESSION_RATIO + i + x, Worm.GetCenterY() / ConsoleGameView.COMPRESSION_RATIO + j + y, 0);
          }
          else
          {
            FastConsoleWorker.SetPixel(Worm.GetCenterX() / ConsoleGameView.COMPRESSION_RATIO + i - x, Worm.GetCenterY() / ConsoleGameView.COMPRESSION_RATIO + j + y, 0);
          }

        }
      }
    }

    /// <summary>
    /// Нарисовать указатель на червяка
    /// </summary>
    private void DrawWormPointer()
    {
      for (int i = 0; i < 5; i++)
      {
        for (int j = 0; j < 7; j++)
        {
          FastConsoleWorker.SetPixel(Worm.X / ConsoleGameView.COMPRESSION_RATIO + i, Worm.Y / ConsoleGameView.COMPRESSION_RATIO + j - 13, 4);
        }
      }
    }

    /// <summary>
    /// Нарисовать здоровье червяка
    /// </summary>
    private void DrawHealth()
    {
      short color;
      switch (Worm.Player.TeamColor)
      {
        case PlayersColor.Orange:
          color = (int)ConsoleColor.DarkRed;
          break;
        case PlayersColor.Blue:
          color = (int)ConsoleColor.Blue;
          break;
        case PlayersColor.Green:
          color = (int)ConsoleColor.Green;
          break;
        case PlayersColor.Yellow:
          color = (int)ConsoleColor.Yellow;
          break;
        default:
          color = 11;
          break;
      }
      for (int i = 0; i < 10; i++)
      {
        for (int j = 0; j < 3; j++)
        {
          FastConsoleWorker.SetPixel(Worm.X / ConsoleGameView.COMPRESSION_RATIO + i - 2, Worm.Y / ConsoleGameView.COMPRESSION_RATIO + j - 4, 0);
        }
      }

      for (int i = 0; i < (int)Math.Ceiling((double)Worm.Health/Worm.WORMS_HEALTH*10); i++)
      {
        for (int j = 0; j < 3; j++)
        {
          FastConsoleWorker.SetPixel(Worm.X / ConsoleGameView.COMPRESSION_RATIO + i - 2, Worm.Y / ConsoleGameView.COMPRESSION_RATIO + j - 4, color);
        }
      }
    }

    /// <summary>
    /// Нарисовать тело червяка
    /// </summary>
    private void DrawWorm()
    {
      for (int i = 0; i < Worm.Width / ConsoleGameView.COMPRESSION_RATIO + 2; i++)
      {
        for (int j = 0; j < Worm.Height / ConsoleGameView.COMPRESSION_RATIO + 1; j++)
        {
          FastConsoleWorker.SetPixel(Worm.X / ConsoleGameView.COMPRESSION_RATIO + i, Worm.Y / ConsoleGameView.COMPRESSION_RATIO + j, 12);
          if (Worm.State == State.Right)
          {
            FastConsoleWorker.SetPixel(Worm.X / ConsoleGameView.COMPRESSION_RATIO + 3, Worm.Y / ConsoleGameView.COMPRESSION_RATIO + 1, 0);
          }
          else
          {
            FastConsoleWorker.SetPixel(Worm.X / ConsoleGameView.COMPRESSION_RATIO, Worm.Y / ConsoleGameView.COMPRESSION_RATIO + 1, 0);
          }
        }
      }
    }
  }
}
