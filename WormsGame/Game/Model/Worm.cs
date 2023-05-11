using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WormsGame.Base.Weapons;

namespace WormsGame.Base
{
  /// <summary>
  /// Червяк
  /// </summary>
  public class Worm : PhysicalObject
  {
    /// <summary>
    /// Здоровье червяка
    /// </summary>
    public const int WORMS_HEALTH = 50;
    /// <summary>
    /// Высота червяка
    /// </summary>
    public const int WORM_HEIGHT = 18;
    /// <summary>
    /// Ширина червяка
    /// </summary>
    public const int WORM_WIDTH = 12;

    /// <summary>
    /// Радиус взрыва червяка
    /// </summary>
    private const int WORM_EXPLOSION_RADIUS = 50;
    /// <summary>
    /// Передаваемая скорость взрывом червяка
    /// </summary>
    private const int WORM_EXPLOSION_TRANSMITTED_SPEED = 10;
    /// <summary>
    /// Урон взрыва червяка
    /// </summary>
    private const int WORM_EXPLOSION_DAMAGE = 25;
    /// <summary>
    /// Подвержен ли червяк ветру
    /// </summary>
    private const bool IS_WORM_EXPOSED_TO_WIND = false;
    /// <summary>
    /// Скорость червяка по Y после прыжка вперед
    /// </summary>
    public const int FORWARD_JUMP_Y_SPEED = -5;
    /// <summary>
    /// Скорость червяка по X после прыжка вперед
    /// </summary>
    public const int FORWARD_JUMP_X_SPEED = 3;
    /// <summary>
    /// Скорость червяка по Y после прыжка назад
    /// </summary>
    public const int BACKFLIP_Y_SPEED = -7;
    /// <summary>
    /// Скорость червяка по X после прыжка назад
    /// </summary>
    public const int BACKFLIP_X_SPEED = 2;

    /// <summary>
    /// Объект для генерации случайных чисел
    /// </summary>
    private static Random _random = new Random();

    /// <summary>
    /// Игрок
    /// </summary>
    private Player _player;
    /// <summary>
    /// Выбранное оружие
    /// </summary>
    private Weapon _weapon;
    /// <summary>
    /// Здоровье червяка
    /// </summary>
    private int _health;
    /// <summary>
    /// Полученный урон
    /// </summary>
    private int _gainedDamage;
    /// <summary>
    /// Состояние
    /// </summary>
    private State _state;
    /// <summary>
    /// Использовано ли оружие
    /// </summary>
    private bool _isUsedWeapon;

    /// <summary>
    /// Игрок, которому принадлежит червяк
    /// </summary>
    public Player Player { get { return _player; }  set { _player = value; } }
    /// <summary>
    /// Выбранное оружие
    /// </summary>
    public Weapon Weapon { get { return _weapon; } set { _weapon = value; } }
    /// <summary>
    /// Здоровье червяка
    /// </summary>
    public int Health { get { return _health; } set { _health = value; } }
    /// <summary>
    /// Полученный урон
    /// </summary>
    public int GainedDamage { get { return _gainedDamage; } set { _gainedDamage = value; } }
    /// <summary>
    /// Состояние
    /// </summary>
    public State State { get { return _state; } set { _state = value; } }
    /// <summary>
    /// Использовано ли оружие
    /// </summary>
    public bool IsUsedWeapon { get { return _isUsedWeapon; } set { _isUsedWeapon = value; } }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parPlayer">Игрок</param>
    /// <param name="parX">X</param>
    /// <param name="parY">Y</param>
    /// <param name="parWidth">Ширина</param>
    /// <param name="parHeight">Высота</param>
    /// <param name="parHealth">Здоровье</param>
    /// <param name="parWeapon">Оружие</param>
    public Worm(Player parPlayer, int parX, int parY, int parWidth, int parHeight,
      int parHealth) : base(parX, parY, 0, 0, parWidth, parHeight, IS_WORM_EXPOSED_TO_WIND)
    {
      Player = parPlayer;
      Health = parHealth;
      GainedDamage = 0;
      Weapon = null;
      SetRandomState();
      IsUsedWeapon = false;

    }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parPlayer">Игрок</param>
    /// <param name="parX">X</param>
    /// <param name="parY">Y</param>
    public Worm(Player parPlayer, int parX, int parY) : base(parX, parY, 0, 0, WORM_WIDTH, WORM_HEIGHT, IS_WORM_EXPOSED_TO_WIND)
    {
      Player = parPlayer;
      Health = WORMS_HEALTH;
      GainedDamage = 0;
      Weapon = null;
      SetRandomState();
      IsUsedWeapon = false;
    }

    /// <summary>
    /// Установить случайное состояние
    /// </summary>
    private void SetRandomState()
    {
      int stateRand = _random.Next(0, 2);
      if (stateRand == 0)
      {
        State = State.Right;
      }
      else
      {
        State = State.Left;
      }
    }

    /// <summary>
    /// Обработчик столкновения с картой
    /// </summary>
    public override void OnCollapse()
    {
      YSpeed = 0;
      XSpeed = 0;
    }

    /// <summary>
    /// Обработчик выхода за границы карты
    /// </summary>
    public override void OnGoingOutOfBounds()
    {
      if (IsWormControlling())
      {
        GameModel.RemoveControl();
        GameModel.WaitingForChangingTurn();
      }
      Kill();
    }

    /// <summary>
    /// Управляется ли червяк
    /// </summary>
    /// <returns></returns>
    private bool IsWormControlling()
    {
      bool isWormControlling;
      if (GameModel._isSelectedWormControlling)
      {
        isWormControlling = GameModel.GetCurrentWorm().Equals(this);
      }
      else
      {
        isWormControlling = false;
      }

      return isWormControlling;
    }

    /// <summary>
    /// Убить червяка без взрыва, используется при выходе червяка за границы экрана
    /// </summary>
    public void Kill()
    {
      Player.Worms.Remove(this);
      GameModel.RemoveDamagedWorm(this);
      GameModel.RemoveObject(this);
    }

    /// <summary>
    /// Убить червяка со взрывом
    /// </summary>
    public void KillWithExplosion()
    {
      Player.Worms.Remove(this);
      GameModel.RemoveObject(this);
      ExplosionMaker.MakeExplosion(GetCenterX(), GetCenterY(), WORM_EXPLOSION_RADIUS, WORM_EXPLOSION_TRANSMITTED_SPEED, WORM_EXPLOSION_DAMAGE);
    }

    /// <summary>
    /// Найти высоту препятствия в координате X
    /// </summary>
    /// <param name="parX">X</param>
    /// <returns>Высоту препятствия</returns>
    private int FindObstacleHeight(int parX)
    {
      int y = Y + Height;
      int obstacleHeight = 0;
      while (y > Y - obstacleHeight)
      {
        if (GameModel.Map[y, parX] == 1)
        {
          obstacleHeight = Y + Height - y;
          if (obstacleHeight != 0)
          {
            if (Height / obstacleHeight < 3)
            {
              return -1;
            }
          }
        }
        y--;
      }

      return obstacleHeight;
    }

    /// <summary>
    ///Сделать шаг вправо
    /// </summary>
    public void MoveRight()
    {
      State = State.Right;
      TryRightMove();
    }

    /// <summary>
    /// Попытаться передвинуть червяка вправо
    /// </summary>
    private void TryRightMove()
    {
      int x = X + Width;
      int obstacleHeight = FindObstacleHeight(x);

      if (obstacleHeight != -1)
      {
        X++;
        Y -= obstacleHeight;
      }
    }

    /// <summary>
    /// Сделать шаг влево
    /// </summary>
    public void MoveLeft()
    {
      State = State.Left;
      TryLeftMove();
    }

    /// <summary>
    /// Попытаться передвинуть червяка влево
    /// </summary>
    private void TryLeftMove()
    {
      int x = X - 1;
      int obstacleHeight = FindObstacleHeight(x);

      if (obstacleHeight != -1)
      {
        X--;
        Y -= obstacleHeight;
      }
    }

    /// <summary>
    /// Выполнить прыжок вперед
    /// </summary>
    public void JumpForward()
    {
      if (IsOnGround())
      {
        YSpeed += FORWARD_JUMP_Y_SPEED;
        if (State == State.Right)
        {
          XSpeed += FORWARD_JUMP_X_SPEED;
        } 
        else if (State == State.Left)
        {
          XSpeed -= FORWARD_JUMP_X_SPEED;
        }
      }
      
    }

    /// <summary>
    /// Выполнить прыжок назад
    /// </summary>
    public void BackFlip()
    {
      if (IsOnGround())
      {
        YSpeed += BACKFLIP_Y_SPEED;
        if (State == State.Right)
        {
          XSpeed -= BACKFLIP_X_SPEED;
        }
        else if (State == State.Left)
        {
          XSpeed += BACKFLIP_X_SPEED;
        }
      }
    }

    /// <summary>
    /// Использовать оружие
    /// </summary>
    public void UseWeapon()
    {
      if (Weapon != null)
      {
        Weapon.Use(this);
        GameTimers.TurnTimeLast = Weapon.TimeAfterUse;
        Weapon = null;
        IsUsedWeapon = true;
        
      }
      
    }

    
  }
}
