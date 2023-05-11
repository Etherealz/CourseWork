using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WormsGame.Help.Controller;
using WormsGame.Help.Model;
using WormsWPF.Help.View;

namespace WormsWPF.Help.Controller
{
  /// <summary>
  /// WPF контроллер справки
  /// </summary>
  public class WPFHelpController : HelpController
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
    public WPFHelpController(HelpModel parModel, Window parWindow) : base(parModel, new WPFHelpView(parModel, parWindow))
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
