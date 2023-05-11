using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WormsConsole
{
  /// <summary>
  /// Срелства для осуществления работы помощника для работы с быстрым выводом консоли
  /// </summary>
  public class ConsoleUtils
  {
    /// <summary>
    /// Координата
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Coord
    {
      public short X;
      public short Y;

      public Coord(short X, short Y)
      {
        this.X = X;
        this.Y = Y;
      }
    };

    /// <summary>
    /// Объединение символов ASCII и Unicode
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct CharUnion
    {
      [FieldOffset(0)] public char UnicodeChar;
      [FieldOffset(0)] public byte AsciiChar;
    }

    /// <summary>
    /// Информация о символе
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct CharInfo
    {
      [FieldOffset(0)] public CharUnion Char;
      [FieldOffset(2)] public short Attributes;
    }

    /// <summary>
    /// Прямоугольник
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SmallRect
    {
      public short Left;
      public short Top;
      public short Right;
      public short Bottom;
    }

    /// <summary>
    /// Создать файл
    /// </summary>
    /// <param name="fileName">Название файла</param>
    /// <param name="fileAccess">Доступ к файлу</param>
    /// <param name="fileShare"></param>
    /// <param name="securityAttributes">Атрибуты безопасности</param>
    /// <param name="creationDisposition"></param>
    /// <param name="flags">Флаги</param>
    /// <param name="template">Шаблон</param>
    /// <returns>Дескриптор файла</returns>
    [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    public static extern SafeFileHandle CreateFile(
    string fileName,
    [MarshalAs(UnmanagedType.U4)] uint fileAccess,
    [MarshalAs(UnmanagedType.U4)] uint fileShare,
    IntPtr securityAttributes,
    [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
    [MarshalAs(UnmanagedType.U4)] int flags,
    IntPtr template);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool WriteConsoleOutput(
      SafeFileHandle hConsoleOutput,
      CharInfo[] lpBuffer,
      Coord dwBufferSize,
      Coord dwBufferCoord,
      ref SmallRect lpWriteRegion);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern Int32 SetCurrentConsoleFontEx(
        IntPtr ConsoleOutput,
        bool MaximumWindow,
        ref CONSOLE_FONT_INFO_EX ConsoleCurrentFontEx);

    public enum StdHandle
    {
      OutputHandle = -11
    }

    [DllImport("kernel32")]
    public static extern IntPtr GetStdHandle(StdHandle index);

    private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

    /// <summary>
    /// Информация о консольном шрифте
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct CONSOLE_FONT_INFO_EX
    {
      public uint cbSize;
      public uint nFont;
      public Coord dwFontSize;
      public int FontFamily;
      public int FontWeight;

      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
      public string FaceName;
    }
  }
}
