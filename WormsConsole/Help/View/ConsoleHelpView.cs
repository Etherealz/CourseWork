using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WormsGame.Help.Model;
using WormsGame.Help.View;

namespace WormsConsole.Help.View
{
  /// <summary>
  /// Консольное представление справки
  /// </summary>
  public class ConsoleHelpView : HelpView
  {
    /// <summary>
    /// Координата X для начала отрисовки
    /// </summary>
    private const int X_START = 6;
    /// <summary>
    /// Координата Y для начала отрисовки
    /// </summary>
    private const int Y_START = 5;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parModel">Модель</param>
    public ConsoleHelpView(HelpModel parModel) : base(parModel)
    {
    }

    /// <summary>
    /// Нарисовать представление
    /// </summary>
    public override void Draw()
    {
      Console.BackgroundColor = ConsoleColor.DarkGray;
      Console.Clear();

      string header = "Справка. Нажмите Esc, чтобы выйти";
      Console.SetCursorPosition(2, 2);
      Console.Write(header);

      string helpText = Model.GetHelpText();
      Console.SetCursorPosition(X_START, Y_START);
      int y = 0;
      int x = 0;
      for (int i = 0; i < helpText.Length; i++)
      {
        if (helpText[i] == '\n')
        {
          x = 0;
          y += 2;
          Console.SetCursorPosition(X_START, Y_START + y);
          continue;
        }

        if (x >= Console.WindowWidth - X_START * 2)
        {
          x = 0;
          y++;
          Console.SetCursorPosition(X_START, Y_START + y);
        }

        Console.Write(helpText[i]);
        x++;

      }

    }

    /// <summary>
    /// Запустить
    /// </summary>
    public override void Start()
    {
      Draw();
      base.Start();
    }

    /// <summary>
    /// Остановить
    /// </summary>
    public override void Stop()
    {
      base.Stop();
      Console.Clear();
    }

  }
}
