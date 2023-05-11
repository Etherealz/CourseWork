using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WormsGame.Base.Weapons;

namespace WormsGame.Base
{
  /// <summary>
  /// Инициализатор игры
  /// </summary>
  public class GameInitializer
  {
    /// <summary>
    /// Кол-во игроков
    /// </summary>
    private const int PLAYERS_COUNT = 3;
    /// <summary>
    /// Кол-во червяков у игрока
    /// </summary>
    private const int WORMS_COUNT = 5;

    /// <summary>
    /// Объект для генерирования случайных чисел
    /// </summary>
    private static Random _random = new Random();
    /// <summary>
    /// Цвета игроков
    /// </summary>
    private static PlayersColor[] _colors = new PlayersColor[] { PlayersColor.Orange, PlayersColor.Blue, PlayersColor.Green, PlayersColor.Yellow };

    /// <summary>
    /// Инициализировать
    /// </summary>
    public static void Init()
    {
      GameTimers.InitTimers();
      GameModel.AllObjects = new List<PhysicalObject>();
      GameModel.DamagedWorms = new List<Worm>();
      GameModel.Map = new int[1080, 1920];
      MapReader.ReadMapFromFile("map.txt");
      InitPlayers();
      GameModel.SetRandomWindStrength();
    }

    /// <summary>
    /// Инициализировать игроков 
    /// </summary>
    public static void InitPlayers()
    {
      GameModel.Players = new List<Player>();

      for (int i = 0; i < PLAYERS_COUNT; i++)
      {
        Player player = new Player();
        Inventory inventory = InitInventory();
        player.Inventory = inventory;
        List<Worm> worms = InitWorms(player);
        player.Worms = worms;
        player.TeamColor = _colors[i];

        GameModel.Players.Add(player);
      }
    }

    /// <summary>
    /// Инициализировать инвентарь
    /// </summary>
    /// <returns>Инвентарь</returns>
    private static Inventory InitInventory()
    {
      List<Weapon> weapons = new List<Weapon>();
      weapons.Add(new Bazooka(7));
      weapons.Add(new Grenade(5));
      Inventory inventory = new Inventory(weapons);
      return inventory;
    }

    /// <summary>
    /// Инициализировать червяков
    /// </summary>
    /// <param name="parPlayer">Игрок</param>
    /// <returns>Список червяков</returns>
    private static List<Worm> InitWorms(Player parPlayer)
    {
      List<Worm> worms = new List<Worm>();

      for (int i = 0; i < WORMS_COUNT; i++)
      {
        int x;
        int y;

        do
        {
          x = _random.Next(100, 1820);
          y = GetYWorm(x);
        } while (y == -1);

        Worm worm = new Worm(parPlayer, x, y);
        worms.Add(worm);
        GameModel.AddObject(worm);
      }

      return worms;
    }

    /// <summary>
    /// Получить Y червяка, при котором он будет стоять на поверхности карты в координате X
    /// </summary>
    /// <param name="parX">X</param>
    /// <returns>Y</returns>
    private static int GetYWorm(int parX)
    {
      for (int y = 0; y < 1080; y++)
      {
        for (int i = parX; i <= parX + Worm.WORM_WIDTH; i++)
        {
          if (GameModel.Map[y, i] == 1)
          {
            return y - Worm.WORM_HEIGHT;
          }
        }
      }

      return -1;
    }
  }
}
