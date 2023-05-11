using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WormsGame.MainMenu.Model;
using WormsGame.MainMenu.View;

namespace WormsGame.MainMenu.Controller
{
  /// <summary>
  /// Контроллер главного меню
  /// </summary>
  public class MainMenuController : BaseMVC.Controller<MainMenuModel, MainMenuView>
  {
    /// <summary>
    /// Событие о необходимости перейти к игре
    /// </summary>
    public event dNeedGoToGame? NeedGoToGameEvent;
    /// <summary>
    /// Событие о необходимости перейти к справке
    /// </summary>
    public event dNeedGoToHelp? NeedGoToHelpEvent;
    /// <summary>
    /// Событие о необходимости перейти к рекордам
    /// </summary>
    public event dNeedGoToRecords? NeedGoToRecordsEvent;
    /// <summary>
    /// Событие о необходимости выйти
    /// </summary>
    public event dNeedGoToExit? NeedExitEvent;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parModel">Модель</param>
    /// <param name="parView">Представление</param>
    public MainMenuController(MainMenuModel parModel, MainMenuView parView) : base(parModel, parView)
    {
    }

    /// <summary>
    /// Запустить
    /// </summary>
    public override void Start()
    {
      Model.OnMenuClick += MenuClickHandler;
      base.Start();
    }

    /// <summary>
    /// Остановить
    /// </summary>
    public override void Stop()
    {
      Model.OnMenuClick -= MenuClickHandler;
      base.Stop();
    }

    /// <summary>
    /// Обработчик нажатия на выбранный пункт меню
    /// </summary>
    /// <param name="parSelectedMenuItem"></param>
    private void MenuClickHandler(MenuItem parSelectedMenuItem)
    {
      switch (parSelectedMenuItem)
      {
        case MenuItem.Game:
          NeedGoToGameEvent?.Invoke();
          break;
        case MenuItem.Help:
          NeedGoToHelpEvent?.Invoke();
          break;
        case MenuItem.Records:
          NeedGoToRecordsEvent?.Invoke();
          break;
        case MenuItem.Exit:
          NeedExitEvent?.Invoke();
          break;
      }
    }

    /// <summary>
    /// Делегат на необходимость перейти к игре
    /// </summary>
    public delegate void dNeedGoToGame();
    /// <summary>
    /// Делегат на необходимость перейти к справке
    /// </summary>
    public delegate void dNeedGoToHelp();
    /// <summary>
    /// Делегат на необходимость перейти к рекордам
    /// </summary>
    public delegate void dNeedGoToRecords();
    /// <summary>
    /// Делегат на необходимость выйти
    /// </summary>
    public delegate void dNeedGoToExit();

  }

}
