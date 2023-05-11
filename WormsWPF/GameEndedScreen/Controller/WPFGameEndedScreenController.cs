using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WormsGame.GameEndedScreen.Controller;
using WormsGame.GameEndedScreen.Model;
using WormsGame.GameEndedScreen.View;
using WormsWPF.GameEndedScreen.Model;

namespace WormsWPF.GameEndedScreen.Controller
{
  /// <summary>
  /// WPF контроллер экрана конца игры
  /// </summary>
  public class WPFGameEndedScreenController : GameEndedScreenController
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
    public WPFGameEndedScreenController(GameEndedScreenModel parModel, Window parWindow) : base(parModel, new WPFGameEndedScreenView(parModel, parWindow))
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
      if (e.Key == Key.Escape)
      {
        BackToMenu();
      }

      if (Model.IsRecord)
      {
        if (Regex.IsMatch(e.Key.ToString(), "^[A-Z]$"))
        {
          Model.Name += e.Key.ToString();
        }
        if (e.Key == Key.Back)
        {
          if (Model.Name.Length > 0)
          {
            Model.Name = Model.Name.Remove(Model.Name.Length - 1, 1);
          }
          
        }

        if (e.Key == Key.Enter)
        {
          if (Model.SaveRecord())
          {
            BackToMenu();
          }
        }
        
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
