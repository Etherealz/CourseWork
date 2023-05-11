
using WormsConsole.ControllerConsole;
using WormsConsole.GameEndedScreen.Controller;
using WormsConsole.Help.Controller;
using WormsConsole.MainMenu.Controller;
using WormsConsole.Records.Controller;
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

namespace WormsConsole
{
  /// <summary>
  /// Фабрика консольных контроллеров
  /// </summary>
  public class ConsoleControllersFactory : AbstractControllersFactory
  {
    /// <summary>
    /// Создать контроллер игры
    /// </summary>
    /// <returns>Контроллер игры</returns>
    public override GameController CreateGameController()
    {
      return new ConsoleGameController(null);
    }

    /// <summary>
    /// Создать контроллер экрана конца игры
    /// </summary>
    /// <returns>Контроллер экрана конца игры</returns>
    public override GameEndedScreenController CreateGameEndedScreenController()
    {
      return new ConsoleGameEndedScreenController(new GameEndedScreenModel());
    }

    /// <summary>
    /// Создать контроллер справки
    /// </summary>
    /// <returns>Контроллер справки</returns>
    public override HelpController CreateHelpController()
    {
      return new ConsoleHelpController(new HelpModel());
    }

    /// <summary>
    /// Создать контроллер главного меню
    /// </summary>
    /// <returns>Контроллер главного меню</returns>
    public override MainMenuController CreateMenuController()
    {
      return new ConsoleMainMenuController(new MainMenuModel());
    }

    /// <summary>
    /// Создать контроллер рекордов
    /// </summary>
    /// <returns>Контроллер рекордов</returns>
    public override RecordsController CreateRecordsController()
    {
      return new ConsoleRecordsController(new RecordsModel());
    }
  }
}
