using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WormsGame.GameEndedScreen.Model;
using WormsGame.GameEndedScreen.View;

namespace WormsConsole.GameEndedScreen.View
{
  /// <summary>
  /// Консольное представление экрана конца игры
  /// </summary>
  public class ConsoleGameEndedScreenView : GameEndedScreenView
  {
    /// <summary>
    /// Координата X для начала отрисовки
    /// </summary>
    private const int X_START = 5;
    /// <summary>
    /// Координата Y для начала отрисовки
    /// </summary>
    private const int Y_START = 5;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parModel">Модель</param>
    public ConsoleGameEndedScreenView(GameEndedScreenModel parModel) : base(parModel)
    {
    }

    /// <summary>
    /// Настроить окно
    /// </summary>
    public void SetupWindow()
    {
      FastConsoleWorker.SetFontSize(8, 14);
      Console.SetWindowSize(120, 50);

      Console.BackgroundColor = ConsoleColor.DarkGray;
      Console.Clear();
      InitialDraw();
      Draw();

    }

    /// <summary>
    /// Начальная отрисовка при запуске
    /// </summary>
    private void InitialDraw()
    {
      string helpText = Model.GetEndText();
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

      string header = "Результаты игры. Нажмите Esc, чтобы выйти.";
      if (Model.IsRecord)
      {
        string name = Model.Name;

        Console.SetCursorPosition(Console.WindowWidth / 2 - name.Length / 2, Console.WindowHeight / 2);
        Console.Write(name);
        header += " Нажмите Enter, чтобы подтвердить имя";
      }

      Console.SetCursorPosition(2, 2);
      Console.Write(header);
    }

    /// <summary>
    /// Нарисовать введенное имя победителя
    /// </summary>
    public override void Draw()
    {
      if (Model.IsRecord)
      {
        Console.SetCursorPosition(0, Console.WindowHeight / 2);
        for (int i = 0; i < Console.WindowWidth; i++)
        {
          Console.Write(' ');
        }
        string name = Model.Name;

        Console.SetCursorPosition(Console.WindowWidth / 2 - name.Length / 2, Console.WindowHeight / 2);
        Console.Write(name);
      }


    }

    /// <summary>
    /// Запустить
    /// </summary>
    public override void Start()
    {
      SetupWindow();
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
