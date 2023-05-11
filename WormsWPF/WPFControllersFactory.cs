using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WormsGame;
using WormsGame.Controller;
using WormsGame.GameEndedScreen.Controller;
using WormsGame.GameEndedScreen.Model;
using WormsGame.Help.Controller;
using WormsGame.Help.Model;
using WormsGame.MainMenu.Controller;
using WormsGame.MainMenu.Model;
using WormsGame.Records.Controller;
using WormsGame.Records.Model;
using WormsWPF.GameEndedScreen.Controller;
using WormsWPF.Help.Controller;
using WormsWPF.MainMenu.Controller;
using WormsWPF.Records.Controller;
using WormsWPF.WPFController;

namespace WormsWPF
{
  /// <summary>
  /// Фабрика WPF контроллеров
  /// </summary>
  public class WPFControllersFactory : AbstractControllersFactory
  {
    /// <summary>
    /// Окно
    /// </summary>
    private readonly Window _window;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parWindow">Окно</param>
    public WPFControllersFactory(Window parWindow)
    {
      _window = parWindow;
    }

    /// <summary>
    /// Создать контроллер игры
    /// </summary>
    /// <returns>Контроллер игры</returns>
    public override GameController CreateGameController()
    {
      return new WPFGameController(null, _window);
    }

    /// <summary>
    /// Создать контроллер экрана конца игры
    /// </summary>
    /// <returns>Контроллер экрана конца игры</returns>
    public override GameEndedScreenController CreateGameEndedScreenController()
    {
      GameEndedScreenModel gameEndedScreenModel = new GameEndedScreenModel();
      return new WPFGameEndedScreenController(gameEndedScreenModel, _window);
    }

    /// <summary>
    /// Создать контроллер справки
    /// </summary>
    /// <returns>Контроллер справки</returns>
    public override HelpController CreateHelpController()
    {
      HelpModel helpModel = new HelpModel();
      return new WPFHelpController(helpModel, _window);
    }

    /// <summary>
    /// Создать контроллер меню
    /// </summary>
    /// <returns>Контроллер меню</returns>
    public override MainMenuController CreateMenuController()
    {
      MainMenuModel menuModel = new MainMenuModel();
      return new WPFMainMenuController(menuModel, _window);
    }

    /// <summary>
    /// Создать контроллер рекордов
    /// </summary>
    /// <returns>Контроллер рекордов</returns>
    public override RecordsController CreateRecordsController()
    {
      RecordsModel recordsModel = new RecordsModel();
      return new WPFRecordsController(recordsModel, _window);
    }
  }
}
