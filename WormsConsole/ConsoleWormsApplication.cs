using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WormsGame;

namespace WormsConsole
{
  /// <summary>
  /// Консольное приложение
  /// </summary>
  public class ConsoleWormsApplication : WormsApplication
  {
    /// <summary>
    /// Ширина окна
    /// </summary>
    public const int WINDOW_WIDTH = 320;
    /// <summary>
    /// Высота окна
    /// </summary>
    public const int WINDOW_HEIGHT = 180;

    /// <summary>
    /// Конструктор
    /// </summary>
    public ConsoleWormsApplication() : base (new ConsoleControllersFactory())
    {
      ConfigureWindow();
    }

    /// <summary>
    /// Скофигурировать окно
    /// </summary>
    private void ConfigureWindow()
    {
      Console.SetBufferSize(WINDOW_WIDTH, WINDOW_HEIGHT);
      Console.CursorVisible = false;
      NeedExitEvent += () =>
      {
        Console.Clear();
      };
    }
  }
}
