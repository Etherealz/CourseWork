using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using WormsGame.Base.Weapons;

namespace WormsGame.Base
{
  /// <summary>
  /// Модель игры
  /// </summary>
  public class GameModel : BaseMVC.Model
  {
    /// <summary>
    /// Ширина карты
    /// </summary>
    public const int MAP_WIDTH = 1920;
    /// <summary>
    /// Высота карты
    /// </summary>
    public const int MAP_HEIGHT = 1080;
    /// <summary>
    /// Максимальная сила ветра
    /// </summary>
    public const int WIND_STRENGTH_MAX = 1;
    /// <summary>
    /// Минимальная сила ветра
    /// </summary>
    public const int WIND_STRENGTH_MIN = -1;

    /// <summary>
    /// Объект для генерации случайных чисел
    /// </summary>
    private static Random _random = new Random();
    /// <summary>
    /// Индекс управляемого червяка
    /// </summary>
    private static int _currentPlayerIndex;

    /// <summary>
    /// Список всех физических объектов
    /// </summary>
    private volatile static List<PhysicalObject> _allObjects;
    /// <summary>
    /// Список червяков, которым во время хода нанесло урон
    /// </summary>
    private volatile static List<Worm> _damagedWorms;
    /// <summary>
    /// Закончена ли игра
    /// </summary>
    private static bool _isGameEnded;
    /// <summary>
    /// Под контролем ли сейчас червяк
    /// </summary>
    public volatile static bool _isSelectedWormControlling;

    /// <summary>
    /// Событие изменения текущего управляемого червяка
    /// </summary>
    public static event dWormControlChanged? WormControlChangedEvent;
    /// <summary>
    /// Событие завершения игры
    /// </summary>
    public static event dGameEnded? GameFinishedEvent;
    /// <summary>
    /// Событие получения урона червяком в конце хода
    /// </summary>
    public static event dDamagedGained? DamageGainedEvent;
    /// <summary>
    /// Событие добавления нового физического объекта
    /// </summary>
    public static event dNewObjectAdded? NewObjectAddedEvent;
    /// <summary>
    /// Событие удаления физического объекта
    /// </summary>
    public static event dObjectDeleted? ObjectDeletedEvent;

    /// <summary>
    /// Карта
    /// </summary>
    public static int[,] Map { get; set; }
    /// <summary>
    /// Список всех физических объектов
    /// </summary>
    public static List<PhysicalObject> AllObjects { get { return _allObjects; } set { _allObjects = value; } }
    /// <summary>
    /// Список игроков
    /// </summary>
    public static List<Player> Players { get; set; }
    /// <summary>
    /// Список червяков, которым во время хода нанесло урон 
    /// </summary>
    public static List<Worm> DamagedWorms { get { return _damagedWorms; } set { _damagedWorms = value; } }
    /// <summary>
    /// Сила ветра
    /// </summary>
    public static int WindStrength { get; set; }
    
    /// <summary>
    /// Запустить
    /// </summary>
    public static void Start()
    {
      GameInitializer.Init();
      WormControlChangedEvent?.Invoke(GetCurrentWorm());
      _isGameEnded = false;
      _isSelectedWormControlling = true;

      new Thread(() =>
      {
        while (!_isGameEnded)
        {
          lock (AllObjects)
          {
            for (int i = 0; i < AllObjects.Count; i++)
            {
              PhysicalObject obj = AllObjects[i];
              obj.Update();
            }
          }

          Thread.Sleep(32);
        }
      }).Start();


    }

    /// <summary>
    /// Остановить
    /// </summary>
    public static void Stop()
    {
      GameTimers.StopTimers();
      _isGameEnded = true;
    }

    /// <summary>
    /// Получить всех червяков
    /// </summary>
    /// <returns>Список всех червей</returns>
    public static List<Worm> GetAllWorms()
    {
      List<Worm> worms = new List<Worm>();
      for (int i = 0; i < Players.Count; i++)
      {
        Player player = Players[i];
        for (int j = 0; j < Players[i].Worms.Count; j++)
        {
          worms.Add(player.Worms[j]);
        }
      }

      return worms;
    }

    /// <summary>
    /// Добавить новый физический объект в список всех физических объектов
    /// </summary>
    /// <param name="parPhysicalObject">Физический объект</param>
    public static void AddObject(PhysicalObject parPhysicalObject)
    {
      lock (AllObjects)
      {
        AllObjects.Add(parPhysicalObject);
        NewObjectAddedEvent?.Invoke(parPhysicalObject);
      }
      
    }

    /// <summary>
    /// Удалить физический объект из списка всех физических объектов
    /// </summary>
    /// <param name="parPhysicalObject">Физический объект</param>
    public static void RemoveObject(PhysicalObject parPhysicalObject)
    {
      lock (AllObjects)
      {
        AllObjects.Remove(parPhysicalObject);
        ObjectDeletedEvent?.Invoke(parPhysicalObject);
      }
    }

    /// <summary>
    /// Добавить червяка в список червяков, которым нанесли урон
    /// </summary>
    /// <param name="parWorm">Червяк</param>
    public static void AddDamagedWorm(Worm parWorm)
    {
      if (!DamagedWorms.Contains(parWorm))
      {
        DamagedWorms.Add(parWorm);
      }
    }

    /// <summary>
    /// Удалить червяка из списка червяков, которым нанесли урон
    /// </summary>
    /// <param name="parWorm">Червяк</param>
    public static void RemoveDamagedWorm(Worm parWorm)
    {
      DamagedWorms.Remove(parWorm);
    }

    /// <summary>
    /// Удалить игроков без червяков
    /// </summary>
    private static void RemovePlayersWithoutWorms()
    {
      int i = 0;
      while (i < Players.Count)
      {
        if (Players[i].Worms.Count == 0)
        {
          Players.RemoveAt(i);
        }
        else
        {
          i++;
        }
      }
    }

    /// <summary>
    /// Закончилась ли игра
    /// </summary>
    /// <returns>true, если игра закончилась, false - иначе</returns>
    private static bool IsGameEnd()
    {
      if (Players.Count <= 1)
      {
        GameTimers.GameTimer.Stop();
        if (Players.Count == 1)
        {
          EndGame(Players[0]);
        }
        else
        {
          EndGame(null);
        }

        return true;
      }

      return false;
    }

    /// <summary>
    /// Вызвать событие окончания игры
    /// </summary>
    /// <param name="parPlayer">Победивший игрок</param>
    public static void EndGame(Player parPlayer)
    {
      GameFinishedEvent?.Invoke(parPlayer, GameTimers.GAME_TIME - GameTimers.GameTimeLast);
    }

    /// <summary>
    /// Установить индекс следующего червяка
    /// </summary>
    private static void SetNextPlayerIndex()
    {
      _currentPlayerIndex++;

      if (_currentPlayerIndex >= Players.Count)
      {
        _currentPlayerIndex = 0;
      }
    }

    /// <summary>
    /// Приготовить червяка для хода
    /// </summary>
    private static void PrepareWormForTurn()
    {
      Players[_currentPlayerIndex].NextWorm();
      Players[_currentPlayerIndex].GetCurrentWorm().IsUsedWeapon = false;
      _isSelectedWormControlling = true;
    }

    /// <summary>
    /// Запустить следующий ход
    /// </summary>
    private static void StartNextTurn()
    {
      WormControlChangedEvent?.Invoke(GetCurrentWorm());
      GameTimers.TurnTimer.Enabled = true;
      GameTimers.ResetTurnTimer();
    }

    /// <summary>
    /// Установить случайную силу ветра
    /// </summary>
    public static void SetRandomWindStrength()
    {
      WindStrength = _random.Next(WIND_STRENGTH_MIN, WIND_STRENGTH_MAX + 1);
    }

    /// <summary>
    /// Приготовить следующий ход
    /// </summary>
    private static void PrepareNextTurn()
    {
      RemovePlayersWithoutWorms();
      if (IsGameEnd())
      {
        return;
      }
      Thread.Sleep(2000);
      SetRandomWindStrength();
      SetNextPlayerIndex();
      PrepareWormForTurn();
      StartNextTurn();
    }

    /// <summary>
    /// Запустить ожидание смены хода
    /// </summary>
    public static void WaitingForChangingTurn()
    {
      GameTimers.TurnTimeLast = 3;
    }

    /// <summary>
    /// Получить текущего управляемого червяка
    /// </summary>
    /// <returns></returns>
    public static Worm GetCurrentWorm()
    {
      if (_isSelectedWormControlling)
      {
        Player currentPlayer = Players[_currentPlayerIndex];
        return currentPlayer.GetCurrentWorm();
      }
      else
      {
        return null;
      }
      
    }

    /// <summary>
    /// Завершить ход
    /// </summary>
    public static void TurnCompletion()
    {
      while (DamagedWorms.Count != 0)
      {
        Worm worm = DamagedWorms[0];
        worm.Health -= worm.GainedDamage;
        DamageGainedEvent?.Invoke(worm, worm.GainedDamage);
        worm.GainedDamage = 0;
        Thread.Sleep(2000);
        if (worm.Health <= 0)
        {
          worm.KillWithExplosion();
          Thread.Sleep(2000);
        }
        DamagedWorms.Remove(worm);
      }

      PrepareNextTurn();

    }

    /// <summary>
    /// Убрать контроль над текущим червяком
    /// </summary>
    public static void RemoveControl()
    {
      if (_isSelectedWormControlling)
      {
        GetCurrentWorm().Weapon?.SetInitialState();
        _isSelectedWormControlling = false;
        WormControlChangedEvent?.Invoke(null);
      }
      
    }

    /// <summary>
    /// Делегат на смену управляемого червяка
    /// </summary>
    /// <param name="parNextWorm">Червяк, на которого сменилось управление</param>
    public delegate void dWormControlChanged(Worm parNextWorm);
    /// <summary>
    /// Делегат на окончание игры
    /// </summary>
    /// <param name="parPlayerWinner">Победивший игрок</param>
    /// <param name="parScore">Очки</param>
    public delegate void dGameEnded(Player parPlayerWinner, int parScore);
    /// <summary>
    /// Делегат на получение червяком урона в конце хода
    /// </summary>
    /// <param name="parWorm">Червяк</param>
    /// <param name="parDamage">Урон</param>
    public delegate void dDamagedGained(Worm parWorm, int parDamage);
    /// <summary>
    /// Делегат на добавление физического объекта
    /// </summary>
    /// <param name="parPhycicalObject">Физический объект</param>
    public delegate void dNewObjectAdded(PhysicalObject parPhycicalObject);
    /// <summary>
    /// Делегат на удаление физического объекта
    /// </summary>
    /// <param name="parPhycicalObject">Физический объект</param>
    public delegate void dObjectDeleted(PhysicalObject parPhycicalObject);


  }
}
