using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WormsGame.MainMenu
{
  /// <summary>
  /// Меню
  /// </summary>
  public class Menu
  {
    /// <summary>
    /// Список пунктов меню
    /// </summary>
    private readonly List<MenuItem> _items;

    /// <summary>
    /// Спсиок пунктов меню
    /// </summary>
    public List<MenuItem> Items { get { return _items; } }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parItems">Пункты меню</param>
    public Menu(params MenuItem[] parItems)
    {
      _items = parItems.ToList();
    }

    /// <summary>
    /// Получить пункт меню по индексу
    /// </summary>
    /// <param name="parIndex"></param>
    /// <returns></returns>
    public MenuItem GetMenuItemByIndex(int parIndex)
    {
      return _items[parIndex];
    }
  }
}
