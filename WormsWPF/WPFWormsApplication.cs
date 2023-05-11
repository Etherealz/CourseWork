using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WormsGame;

namespace WormsWPF
{
  /// <summary>
  /// WPF приложение
  /// </summary>
  public class WPFWormsApplication : WormsApplication
  {
    /// <summary>
    /// Ширина окна
    /// </summary>
    public const int WINDOW_WIDTH = 1920;
    /// <summary>
    /// Высота окна
    /// </summary>
    public const int WINDOW_HEIGHT = 1080;

    /// <summary>
    /// Окно
    /// </summary>
    private readonly Window _window;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parWindow">Окно</param>
    public WPFWormsApplication(Window parWindow) : base(new WPFControllersFactory(parWindow))
    {
      _window = parWindow;
      ConfigureWindow();
    }

    /// <summary>
    /// Сконфигурировать окно
    /// </summary>
    private void ConfigureWindow()
    {
      _window.Width = WINDOW_WIDTH;
      _window.Height = WINDOW_HEIGHT;
      _window.ResizeMode = ResizeMode.NoResize;
      _window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
      NeedExitEvent += () => _window.Close();
    }

    /// <summary>
    /// Запустить приложение
    /// </summary>
    public override void Start()
    {
      _window.Show();
      base.Start();
    }
  }
}
