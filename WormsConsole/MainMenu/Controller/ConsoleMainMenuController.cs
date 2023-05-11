using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WormsConsole.MainMenu.View;
using WormsGame.MainMenu.Controller;
using WormsGame.MainMenu.Model;
using WormsGame.MainMenu.View;

namespace WormsConsole.MainMenu.Controller
{
  /// <summary>
  /// Консольный контроллер главного меню
  /// </summary>
  public class ConsoleMainMenuController : MainMenuController
  {
    /// <summary>
    /// Работает ли считывание клавиш
    /// </summary>
    private bool _isWorking;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parModel">Модель</param>
    public ConsoleMainMenuController(MainMenuModel parModel) : base(parModel, new ConsoleMainMenuView(parModel))
    {
    }

    /// <summary>
    /// Запустить считывание нажатых клавиш в консоли
    /// </summary>
    public void ReadKeysStart()
    {
      _isWorking = true;
      do
      {
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);

        switch (keyInfo.Key)
        {
          case ConsoleKey.UpArrow:
            Model.PreviousMenuItem();
            break;
          case ConsoleKey.DownArrow:
            Model.NextMenuItem();
            break;
          case ConsoleKey.Enter:
            Model.Enter();
            break;

        }


      } while (_isWorking); 
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
