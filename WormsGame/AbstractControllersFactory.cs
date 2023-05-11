using WormsGame.Controller;
using WormsGame.GameEndedScreen.Controller;
using WormsGame.Help.Controller;
using WormsGame.MainMenu.Controller;
using WormsGame.Records.Controller;

namespace WormsGame
{
  /// <summary>
  /// Фабрика контроллеров
  /// </summary>
  public abstract class AbstractControllersFactory
  {
    /// <summary>
    /// Создать контроллер меню
    /// </summary>
    /// <returns>контроллер меню</returns>
    public abstract MainMenuController CreateMenuController();
    /// <summary>
    /// Создать контроллер игры
    /// </summary>
    /// <returns>Контроллер игры</returns>
    public abstract GameController CreateGameController();
    /// <summary>
    /// Создать контроллер рекордов
    /// </summary>
    /// <returns>контроллер рекордов</returns>
    public abstract RecordsController CreateRecordsController();
    /// <summary>
    /// Создать контроллер справки
    /// </summary>
    /// <returns>контроллер справки</returns>
    public abstract HelpController CreateHelpController();
    /// <summary>
    /// Создатьк контроллер экрана конца игры
    /// </summary>
    /// <returns>Контроллер экрана конца игры</returns>
    public abstract GameEndedScreenController CreateGameEndedScreenController();
  }
}
