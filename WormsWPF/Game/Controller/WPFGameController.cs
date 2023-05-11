using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WormsGame.Base;
using WormsGame.Controller;
using WormsGame.View;

namespace WormsWPF.WPFController
{
  /// <summary>
  /// WPF контроллер игры
  /// </summary>
  public class WPFGameController : GameController
  {
    /// <summary>
    /// Окно
    /// </summary>
    private readonly Window _window;

    /// <summary>
    /// Контроллер
    /// </summary>
    /// <param name="parModel">Модель</param>
    /// <param name="parWindow">Окно</param>
    public WPFGameController(GameModel parModel, Window parWindow) : base(parModel, new WPFGameView(parModel, parWindow))
    {
      _window = parWindow;
    }

    /// <summary>
    /// Обработчик закрытия окна
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnClose(object? sender, EventArgs e)
    {
      Stop();
    }

    /// <summary>
    /// Обработчик нажатия клавиши клавиатуры
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void KeyDownHandler(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Escape)
      {
        BackToMenu();
      }     

      Worm currentWorm = GameModel.GetCurrentWorm();

      while (Keyboard.IsKeyDown(Key.Space))
      {
        currentWorm?.Weapon?.PowerUp();
        return;
      }

      if (currentWorm != null && !Keyboard.IsKeyDown(Key.Space))
      {
        switch (e.Key)
        {
          case Key.F:
            currentWorm.JumpForward();
            break;
          case Key.R:
            currentWorm.BackFlip();

            break;
          case Key.D:
            currentWorm.MoveRight();
            break;
          case Key.A:
            currentWorm.MoveLeft();
            break;
          case Key.S:
            currentWorm.Weapon?.RiseAngle();

            break;
          case Key.W:
            if (currentWorm.Weapon != null)
            {
              currentWorm.Weapon.DownAngle();
            }

            break;
          case Key.D1:
            if (!currentWorm.IsUsedWeapon)
            {
              currentWorm.Weapon = currentWorm.Player.Inventory.Weapons[0];
            }
            break;
          case Key.D2:
            if (!currentWorm.IsUsedWeapon)
            {
              currentWorm.Weapon = currentWorm.Player.Inventory.Weapons[1];
            }
            break;

        }


      }
    }

    /// <summary>
    /// Обработчик отпускания клавиши клавиатуры
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void Window_KeyUp(object sender, KeyEventArgs e)
    {
      switch (e.Key)
      {
        case Key.Space:
          GameModel.GetCurrentWorm()?.UseWeapon();
          break;

      }
    }

    /// <summary>
    /// Запустить
    /// </summary>
    public override void Start()
    {
      _window.KeyDown += KeyDownHandler;
      _window.KeyUp += Window_KeyUp;
      _window.Closed += OnClose;
      base.Start();
    }

    /// <summary>
    /// Остановить
    /// </summary>
    public override void Stop()
    {
      _window.KeyDown -= KeyDownHandler;
      _window.KeyUp -= Window_KeyUp;
      _window.Closed -= OnClose;
      base.Stop();
    }


  }
}
