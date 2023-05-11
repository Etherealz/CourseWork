using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WormsConsole
{
  /// <summary>
  /// Консольный рисовальщик
  /// </summary>
  public class ConsoleDrawer
  {
    /// <summary>
    /// Нарисовать прямоугольник
    /// </summary>
    /// <param name="parX">Координата X</param>
    /// <param name="parY">Координата Y</param>
    /// <param name="parWidth">Ширина прямоугольника</param>
    /// <param name="parHeight">Высота прямоугольника</param>
    /// <param name="color">Цвет</param>
    public static void DrawRectangle(int parX, int parY, int parWidth, int parHeight, ConsoleColor color)
    {
      Console.BackgroundColor = color;
      for (int i = 0; i < parHeight; i++)
      {
        Console.SetCursorPosition(parX, parY + i);
        for (int j = 0; j < parWidth; j++)
        {
          Console.Write(' ');
        }
        Console.WriteLine();
      }
    }
  }
}
