using System.Collections.Generic;

namespace WormsGame.Base
{
  /// <summary>
  /// Инвентарь
  /// </summary>
  public class Inventory
  {
    /// <summary>
    /// Список оружия
    /// </summary>
    public List<Weapon> Weapons { get; set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parWeapons">Список оружия</param>
    public Inventory(List<Weapon> parWeapons)
    {
      Weapons = parWeapons;
    }
  }
}