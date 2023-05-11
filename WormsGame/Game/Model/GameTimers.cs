using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WormsGame.Base
{
  /// <summary>
  /// Игровые таймеры
  /// </summary>
  public class GameTimers
  {
    /// <summary>
    /// Время на ход
    /// </summary>
    public const int TURN_TIME = 30;
    /// <summary>
    /// Время на игру
    /// </summary>
    public const int GAME_TIME = 900;

    /// <summary>
    /// Таймер хода
    /// </summary>
    private static int _turnTimeLast;
    /// <summary>
    /// Таймер игры
    /// </summary>
    private static int _gameTimeLast;

    /// <summary>
    /// Событие о смене оставвегося времени на ход
    /// </summary>
    public static event dTurnTimeChanged? TurnTimeChangedEvent;
    /// <summary>
    /// Событие о смене оставвегося времени игры
    /// </summary>
    public static event dGameTimeChanged? GameTimeChangedEvent;

    /// <summary>
    /// Таймер хода
    /// </summary>
    public static System.Timers.Timer TurnTimer { get; set; }
    /// <summary>
    /// Таймер игры
    /// </summary>
    public static System.Timers.Timer GameTimer { get; set; }

    /// <summary>
    /// Оставшееся время на ход
    /// </summary>
    public static int TurnTimeLast
    {
      get 
      { 
        return _turnTimeLast;
      }
      set
      {
        _turnTimeLast = value;
        TurnTimeChangedEvent?.Invoke(_turnTimeLast);
      }
    }

    /// <summary>
    /// Оставшееся время на игру
    /// </summary>
    public static int GameTimeLast
    {
      get
      {
        return _gameTimeLast;
      }
      set
      {
        _gameTimeLast = value;
        GameTimeChangedEvent?.Invoke(_gameTimeLast);
      }
    }

    /// <summary>
    /// Инициализировать таймеры
    /// </summary>
    public static void InitTimers()
    {
      TurnTimer = new System.Timers.Timer();
      GameTimer = new System.Timers.Timer();

      TurnTimeLast = TURN_TIME;

      TurnTimer.Interval = 1000;
      TurnTimer.Elapsed += CountDown;
      TurnTimer.AutoReset = true;
      TurnTimer.Enabled = true;

      GameTimeLast = GAME_TIME;

      GameTimer.Interval = 1000;
      GameTimer.Elapsed += CountDownGame;
      GameTimer.AutoReset = true;
      GameTimer.Enabled = true;
    }

    /// <summary>
    /// Метод, вызываемый при отсчете таймера хода
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    public static void CountDown(object source, ElapsedEventArgs e)
    {
      TurnTimeLast--;
      if (TurnTimeLast <= 0)
      {
        TurnTimer.Enabled = false;
        GameModel.RemoveControl();
        GameModel.TurnCompletion();
      }
    }

    /// <summary>
    /// Метод, вызываемый при отсчете таймера игры
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void CountDownGame(object sender, ElapsedEventArgs e)
    {
      GameTimeLast--;
      if (GameTimeLast <= 0)
      {
        GameTimer.Stop();
        TurnTimer.Stop();
        
        int max = -1;
        Player winner = null;
        foreach (Player player in GameModel.Players)
        {
          Console.WriteLine(player.Worms.Count);
          if (player.Worms.Count > max)
          {
            max = player.Worms.Count;
            winner = player;
          }
          else if (player.Worms.Count == max)
          {
            winner = null;
            break;
          }
        }

        GameModel.EndGame(winner);

        return;
        
      } 
    }

    /// <summary>
    /// Сбросить таймер хода
    /// </summary>
    public static void ResetTurnTimer()
    {
      TurnTimeLast = TURN_TIME;
    }

    /// <summary>
    /// Остановить таймеры
    /// </summary>
    public static void StopTimers()
    {
      TurnTimer.Stop();
      GameTimer.Stop();
    }

    /// <summary>
    /// Делегат на обновление времени таймера хода
    /// </summary>
    /// <param name="parTime"></param>
    public delegate void dTurnTimeChanged(int parTime);
    /// <summary>
    /// Делегат на обновление времени таймера игры
    /// </summary>
    /// <param name="parTime"></param>
    public delegate void dGameTimeChanged(int parTime);

  }
}
