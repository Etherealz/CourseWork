using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WormsConsole.GameEndedScreen.View;
using WormsGame.GameEndedScreen.Controller;
using WormsGame.GameEndedScreen.Model;
using WormsGame.GameEndedScreen.View;

namespace WormsConsole.GameEndedScreen.Controller
{
  /// <summary>
  /// Консольный контроллер экрана конца игры
  /// </summary>
  public class ConsoleGameEndedScreenController : GameEndedScreenController
  {
    /// <summary>
    /// Работает ли считывание клавиш
    /// </summary>
    private bool _isWorking;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parModel">Модель</param>
    public ConsoleGameEndedScreenController(GameEndedScreenModel parModel) : base(parModel, new ConsoleGameEndedScreenView(parModel))
    {
    }

    /// <summary>
    /// Запустить считывание нажатых клавиш в консоли
    /// </summary>
    public void ReadKeysStart()
    {
      _isWorking = true;
      new Thread(() =>
      {
        do
        {
          ConsoleKeyInfo keyInfo = Console.ReadKey(true);

          if (keyInfo.Key == ConsoleKey.Escape)
          {
            BackToMenu();
          }

          if (Model.IsRecord)
          {
            if (Regex.IsMatch(keyInfo.KeyChar.ToString(), "^[A-Za-z0-9А-Яа-я-_]$"))
            {
              Model.Name += keyInfo.KeyChar.ToString();
            }
            if (keyInfo.Key == ConsoleKey.Backspace)
            {
              if (Model.Name.Length > 0)
              {
                Model.Name = Model.Name.Remove(Model.Name.Length - 1, 1);
              }

            }

            if (keyInfo.Key == ConsoleKey.Enter)
            {
              if (Model.SaveRecord())
              {
                BackToMenu();
              }
            }

          }


        } while (_isWorking);
      }).Start();
      
    }

    /// <summary>
    /// Остановить считывание клавиш
    /// </summary>
    public void ReadKeysStop()
    {
      _isWorking = false;
    }

    /// <summary>
    /// Запустить
    /// </summary>
    public override void Start()
    {
      base.Start();
      ReadKeysStart();
    }

    /// <summary>
    /// Остановить
    /// </summary>
    public override void Stop()
    {
      ReadKeysStop();
      base.Stop();
    }

  }
}
