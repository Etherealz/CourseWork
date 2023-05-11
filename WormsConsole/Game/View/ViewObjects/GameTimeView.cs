using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WormsGame.Base;

namespace WormsConsole.ViewConsole.ViewObjects
{
  /// <summary>
  /// Представление игрового таймера
  /// </summary>
  public class GameTimeView : ObjectView
  {
    /// <summary>
    /// Координата X
    /// </summary>
    public int X { get; set; }
    /// <summary>
    /// Координата Y
    /// </summary>
    public int Y { get; set; }
    /// <summary>
    /// Длина таймера
    /// </summary>
    public int Length { get; set; }

    /// <summary>
    /// Конмтруктор
    /// </summary>
    /// <param name="parX">Координата X</param>
    /// <param name="parY">Координата Y</param>
    /// <param name="parLength">Длина таймера</param>
    public GameTimeView(int parX, int parY, int parLength) : base()
    {
      X = parX;
      Y = parY;
      Length = parLength;
    }

    /// <summary>
    /// Нарисовать игровой таймер
    /// </summary>
    public override void Draw()
    {
      int len = Length;

      for (int k = 0; k < Math.Ceiling((double)GameTimers.GameTimeLast / GameTimers.GAME_TIME * len); k++)
      {
        for (int j = 0; j < 5; j++)
        {
          FastConsoleWorker.SetPixel(X + k, Y + j, (int)ConsoleColor.DarkGreen);

        }
      }
    }
  }
}
