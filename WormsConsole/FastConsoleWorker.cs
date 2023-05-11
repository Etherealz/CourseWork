using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static WormsConsole.ConsoleUtils;

namespace WormsConsole
{
  /// <summary>
  /// Помощник по работе с быстрым выводом в консоль
  /// </summary>
  public class FastConsoleWorker
  {
    /// <summary>
    /// Дескриптор файла
    /// </summary>
    private static SafeFileHandle _h;
    /// <summary>
    /// Буфер символов
    /// </summary>
    private static CharInfo[] _buf;
    /// <summary>
    /// Прямоугольник
    /// </summary>
    private static SmallRect _rect;
    /// <summary>
    /// Ширина 
    /// </summary>
    private static short _width;
    /// <summary>
    /// Высота
    /// </summary>
    private static short _height;

    /// <summary>
    /// Установить пиксель
    /// </summary>
    /// <param name="parX">Координата X</param>
    /// <param name="parY">Координата Y</param>
    /// <param name="attribute">Цвет</param>
    public static void SetPixel(int parX, int parY, short attribute)
    {
      if (parX < _width && parY < _height && parX >= 0 && parY >= 0)
      {
        _buf[parY * _width + parX].Attributes = attribute;
        _buf[parY * _width + parX].Char.AsciiChar = 219;
      }

    }

    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="parWidth">Ширина</param>
    /// <param name="parHeight">Высота</param>
    public static void Init(short parWidth, short parHeight)
    {
      _width = parWidth;
      _height = parHeight;
      _h = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);

      if (!_h.IsInvalid)
      {
        _buf = new CharInfo[_width * _height];
        _rect = new SmallRect() { Left = 0, Top = 0, Right = _width, Bottom = _height };
      }

    }

    /// <summary>
    /// Нарисовать все символы из буфера в консоли
    /// </summary>
    public static void Draw()
    {
      WriteConsoleOutput(_h, _buf,
                 new Coord() { X = _width, Y = _height },
                 new Coord() { X = 0, Y = 0 },
                 ref _rect);
    }

    /// <summary>
    /// Нарисовать прямоуголник в буфере
    /// </summary>
    /// <param name="parX">Координата X</param>
    /// <param name="parY">Координата Y</param>
    /// <param name="parWidth">Ширина</param>
    /// <param name="parHeight">Высота</param>
    /// <param name="parColor">Цвет</param>
    public static void DrawRectangle(int parX, int parY, int parWidth, int parHeight, short parColor)
    {
      for (int i = parX; i <= parX + parWidth; i++)
      {
        for (int j = parY; j <= parY + parHeight; j++)
        {
          SetPixel(i, j, parColor);
        }
      }
    }

    /// <summary>
    /// Установить размер шрифта
    /// </summary>
    /// <param name="parXSize">Размер по X</param>
    /// <param name="parYSize">Размер по Y</param>
    public static void SetFontSize(short parXSize, short parYSize)
    {
     
      CONSOLE_FONT_INFO_EX ConsoleFontInfo = new CONSOLE_FONT_INFO_EX();
      ConsoleFontInfo.cbSize = (uint)Marshal.SizeOf(ConsoleFontInfo);

      ConsoleFontInfo.FaceName = "Lucida Console";
      ConsoleFontInfo.dwFontSize.X = parXSize;
      ConsoleFontInfo.dwFontSize.Y = parYSize;

      SetCurrentConsoleFontEx(GetStdHandle(StdHandle.OutputHandle), false, ref ConsoleFontInfo);
         
    }

  }
}
