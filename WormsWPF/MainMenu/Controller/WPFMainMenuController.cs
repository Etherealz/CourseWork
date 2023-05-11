using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WormsGame.MainMenu.Controller;
using WormsGame.MainMenu.Model;
using WormsWPF.MainMenu.View;

namespace WormsWPF.MainMenu.Controller
{
  /// <summary>
  /// WPF контроллер главного меню
  /// </summary>
  public class WPFMainMenuController : MainMenuController
  {
    /// <summary>
    /// Окно
    /// </summary>
    private readonly Window _window;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parModel">Модель</param>
    /// <param name="parWindow">Окно</param>
    public WPFMainMenuController(MainMenuModel parModel, Window parWindow) : base (parModel, new WPFMainMenuView(parModel, parWindow))
    {
      _window = parWindow;
    }

    /// <summary>
    /// Обработчик нажатия клавиши клавиатуры
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void KeyDownHandler(object sender, KeyEventArgs e)
    {
      switch (e.Key)
      {
        case Key.Enter:
          Model.Enter();
          break;
        case Key.Up:
          Model.PreviousMenuItem();
          break;
        case Key.Down:
          Model.NextMenuItem();
          break;
      }
    }

    /// <summary>
    /// Запустить
    /// </summary>
    public override void Start()
    {
      _window.KeyDown += KeyDownHandler;
      base.Start();
    }

    /// <summary>
    /// Остановить
    /// </summary>
    public override void Stop()
    {
      _window.KeyDown -= KeyDownHandler;
      base.Stop();
    }
  }
}
