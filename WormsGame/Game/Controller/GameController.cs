using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WormsGame.Base;
using WormsGame.View;

namespace WormsGame.Controller
{
  /// <summary>
  /// Контроллер игры
  /// </summary>
  public abstract class GameController : BaseMVC.Controller<GameModel, GameView>
  {
    /// <summary>
    /// Червяк для управления
    /// </summary>
    private Worm? _currentWorm = null;

    /// <summary>
    /// Событие завершения игры
    /// </summary>
    public event dGameFinished? GameFinishedEvent;
    /// <summary>
    /// Событие возвращения в меню
    /// </summary>
    public event dBackToMenu? BackToMenuEvent;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parModel">Модель</param>
    /// <param name="parView">Представление</param>
    public GameController(GameModel parModel, GameView parView) : base(parModel, parView)
    {
      GameModel.WormControlChangedEvent += SelectWorm;

      GameModel.GameFinishedEvent += (parPlayer, parScore) =>
      {
        GameFinishedEvent?.Invoke(parPlayer, parScore);
      };
    }

    /// <summary>
    /// Выбрать червяка для управления
    /// </summary>
    /// <param name="parNextWorm">Червяк</param>
    private void SelectWorm(Worm parNextWorm)
    {
      _currentWorm = parNextWorm;
    }

    /// <summary>
    /// Вернуться в меню
    /// </summary>
    public void BackToMenu()
    {
       BackToMenuEvent?.Invoke();
    }

    /// <summary>
    /// Запустить
    /// </summary>
    public override void Start()
    {
      base.Start();
      GameModel.Start();
    }

    /// <summary>
    /// Остановить
    /// </summary>
    public override void Stop()
    {
      GameModel.Stop();
      base.Stop();
    }
    
    /// <summary>
    /// Делегат на возвращение в меню
    /// </summary>
    public delegate void dBackToMenu();
    /// <summary>
    /// Делегат на окончание игры
    /// </summary>
    /// <param name="parPlayer">Игрок</param>
    /// <param name="parScore">Очки</param>
    public delegate void dGameFinished(Player parPlayer, int parScore);

  }
}
