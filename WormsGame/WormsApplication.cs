using WormsGame.Controller;
using WormsGame.GameEndedScreen.Controller;
using WormsGame.Help.Controller;
using WormsGame.MainMenu.Controller;
using WormsGame.Records.Controller;

namespace WormsGame
{
  /// <summary>
  /// Приложение
  /// </summary>
  public class WormsApplication
  {
    /// <summary>
    /// Контроллер меню
    /// </summary>
    private readonly MainMenuController _menuController;
    /// <summary>
    /// Контроллер игры
    /// </summary>
    private readonly GameController _gameController;
    /// <summary>
    /// Контроллер рекордов
    /// </summary>
    private readonly RecordsController _recordsController;
    /// <summary>
    /// Контроллер справки
    /// </summary>
    private readonly HelpController _helpController;
    /// <summary>
    /// Контроллер экрана конца игры
    /// </summary>
    private readonly GameEndedScreenController _gameEndedScreenController;

    /// <summary>
    /// Событие о необходимость выйти
    /// </summary>
    public event dNeedExit? NeedExitEvent;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parFactory">Фабрика контроллеров</param>
    public WormsApplication(AbstractControllersFactory parFactory)
    {
      _menuController = parFactory.CreateMenuController();
      _gameController = parFactory.CreateGameController();
      _recordsController = parFactory.CreateRecordsController();
      _helpController = parFactory.CreateHelpController();
      _gameEndedScreenController = parFactory.CreateGameEndedScreenController();

      Init();
    }

    /// <summary>
    /// Инициализировать приложение
    /// </summary>
    private void Init()
    {
      InitMainMenuController();
      InitGameController();
      InitRecordsController();
      InitHelpController();
      InitGameEndedScreenController();
    }

    /// <summary>
    /// Инициализировать котроллер главного меню
    /// </summary>
    private void InitMainMenuController()
    {
      _menuController.NeedGoToGameEvent += () =>
      {
        _menuController.Stop();
        _gameController.Start();
      };
      _menuController.NeedGoToHelpEvent += () =>
      {
        _menuController.Stop();
        _helpController.Start();
      };
      _menuController.NeedGoToRecordsEvent += () =>
      {
        _menuController.Stop();
        _recordsController.Start();
      };
      _menuController.NeedExitEvent += () =>
      {
        _menuController.Stop();
        NeedExitEvent?.Invoke();
      };
    }

    /// <summary>
    /// Инициализировать котроллер игры
    /// </summary>
    private void InitGameController()
    {
      _gameController.GameFinishedEvent += (parPlayer, parScore) =>
      {
        _gameController.Stop();
        _gameEndedScreenController.SetGameResult(parPlayer, parScore);
        _gameEndedScreenController.Start();  
        
      };
      _gameController.BackToMenuEvent += () =>
      {
        _gameController.Stop();
        _menuController.Start();
      };
    }

    /// <summary>
    /// Инициализировать котроллер рекордов
    /// </summary>
    private void InitRecordsController()
    {
      _recordsController.BackToMenuEvent += () =>
      {
        _recordsController.Stop();
        _menuController.Start();
      };
    }

    /// <summary>
    /// Инициализировать котроллер справки
    /// </summary>
    private void InitHelpController()
    {
      _helpController.BackToMenuEvent += () =>
      {
        _helpController.Stop();
        _menuController.Start();
      };
    }

    /// <summary>
    /// Инициализировать котроллер экрана конца игры
    /// </summary>
    private void InitGameEndedScreenController()
    {
      _gameEndedScreenController.BackToMenuEvent += () =>
      {
        _gameEndedScreenController.Stop();
        _menuController.Start();
      };
    }

    /// <summary>
    /// Запустить
    /// </summary>
    public virtual void Start()
    {
      _menuController.Start();
    }

    /// <summary>
    /// Делегат о необходимости выйти
    /// </summary>
    public delegate void dNeedExit();
  }
}
