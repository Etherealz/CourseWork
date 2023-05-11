using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WormsConsole.ViewConsole;
using WormsGame.Base;
using WormsGame.Controller;
using WormsGame.View;

namespace WormsConsole.ControllerConsole
{
  /// <summary>
  /// Консольный контроллер игры
  /// </summary>
  public class ConsoleGameController : GameController
  {
    /// <summary>
    /// Работает ли считывание клавиш
    /// </summary>
    private bool _isWorking;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parModel">Модель</param>
    public ConsoleGameController(GameModel parModel) : base(parModel, new ConsoleGameView(parModel))
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

        Worm currentWorm = GameModel.GetCurrentWorm();

        if (ConsoleKey.Escape.Equals(keyInfo.Key))
        {
          BackToMenu();
        }

        if (currentWorm != null)
        {

          switch (keyInfo.Key)
          {
            case ConsoleKey.F:
              currentWorm.JumpForward();
              break;
            case ConsoleKey.R:
              currentWorm.BackFlip();

              break;
            case ConsoleKey.D:
              currentWorm.MoveRight();
              break;
            case ConsoleKey.A:
              currentWorm.MoveLeft();
              break;
            case ConsoleKey.S:
              currentWorm.Weapon?.RiseAngle();

              break;
            case ConsoleKey.W:
              if (currentWorm.Weapon != null)
              {
                currentWorm.Weapon.DownAngle();
              }

              break;
            case ConsoleKey.D1:
              if (!currentWorm.IsUsedWeapon)
              {
                currentWorm.Weapon = currentWorm.Player.Inventory.Weapons[0];
              }
              break;
            case ConsoleKey.D2:
              if (!currentWorm.IsUsedWeapon)
              {
                currentWorm.Weapon = currentWorm.Player.Inventory.Weapons[1];
              }
              break;
            case ConsoleKey.Spacebar:
              currentWorm.Weapon?.PowerUp();
              break;
            case ConsoleKey.Enter:
              currentWorm.UseWeapon();
              break;

          }
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
    /// Запустить контроллер
    /// </summary>
    public override void Start()
    {
      base.Start();
      ReadKeysStart();
    }

    /// <summary>
    /// Остановить контроллер
    /// </summary>
    public override void Stop()
    {
      ReadKeysStop();
      base.Stop();
    }

  }
}
