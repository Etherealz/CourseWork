using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WormsGame.Base;
using WormsGame.GameEndedScreen.Model;
using WormsGame.GameEndedScreen.View;
using WormsGame.Records.Model;

namespace WormsGame.GameEndedScreen.Controller
{
  /// <summary>
  /// Контроллер экрана конца игры
  /// </summary>
  public class GameEndedScreenController : BaseMVC.Controller<GameEndedScreenModel, GameEndedScreenView>
  {
    /// <summary>
    /// Событие возвращения в меню
    /// </summary>
    public event dBackToMenu? BackToMenuEvent;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parModel">Модель</param>
    /// <param name="parView">Представление</param>
    public GameEndedScreenController(GameEndedScreenModel parModel, GameEndedScreenView parView) : base(parModel, parView)
    {
    }

    /// <summary>
    /// Установить рещультат игры
    /// </summary>
    /// <param name="parPlayer">Игрок</param>
    /// <param name="parScore">Очки</param>
    public void SetGameResult(Player parPlayer, int parScore)
    {
      Model.Player = parPlayer;
      if (parPlayer != null)
      {
        Model.Score = parScore;
        Model.SetIsRecord();
      }
      else
      {
        Model.IsRecord = false;
      }
    }

    /// <summary>
    /// Вернуться в меню
    /// </summary>
    public void BackToMenu()
    {
      BackToMenuEvent?.Invoke();
    }

    /// <summary>
    /// Делегат на возвращение в меню
    /// </summary>
    public delegate void dBackToMenu();

  }
}
