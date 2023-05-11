using System;
using System.Collections.Generic;

namespace WormsGame.Base
{
  /// <summary>
  /// Игрок
  /// </summary>
  public class Player
  {
    /// <summary>
    /// Список червяков
    /// </summary>
    public List<Worm> Worms { get; set; }
    /// <summary>
    /// Инвентарь
    /// </summary>
    public Inventory Inventory { get; set; }
    /// <summary>
    /// Индекс управляемого червяка
    /// </summary>
    public int CurrentWormIndex { get; set; }
    /// <summary>
    /// Цвет команды
    /// </summary>
    public PlayersColor TeamColor { get; set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    public Player()
    {
      CurrentWormIndex = 0;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parWorms">Список червяков</param>
    /// <param name="parInventory">Инвентарь</param>
    /// <param name="parPlayersColor">Цвет команды</param>
    public Player(List<Worm> parWorms, Inventory parInventory, PlayersColor parPlayersColor)
    {
      Worms = parWorms;
      Inventory = parInventory;
      TeamColor = parPlayersColor;
      CurrentWormIndex = 0;
    }

    /// <summary>
    /// Получить управляемого червяка
    /// </summary>
    /// <returns></returns>
    public Worm GetCurrentWorm()
    {
      return Worms[CurrentWormIndex];
    }

    /// <summary>
    /// Выбрать следующего червяка для управления
    /// </summary>
    public void NextWorm()
    {
      CurrentWormIndex++;
      if (CurrentWormIndex >= Worms.Count)
      {
        CurrentWormIndex = 0;
      }
    }
  }
}