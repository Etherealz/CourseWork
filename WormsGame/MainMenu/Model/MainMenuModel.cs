using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WormsGame.MainMenu.Model
{
  /// <summary>
  /// Модель главного меню
  /// </summary>
  public class MainMenuModel : BaseMVC.Model
  {
    /// <summary>
    /// Событие о необходимости перерисовки
    /// </summary>
    public event dNeedRedraw? NeedRedrawEvent;
    /// <summary>
    /// Событие о нажатии на пункт меню
    /// </summary>
    public event MainMenuClick? OnMenuClick;

    /// <summary>
    /// Индекс выбранного пункта
    /// </summary>
    private int _focusedIndex = 0;

    /// <summary>
    /// Меню
    /// </summary>
    private readonly Menu _menu = new Menu(
       MenuItem.Game,
       MenuItem.Records,
       MenuItem.Help,
       MenuItem.Exit
       );

    /// <summary>
    /// Индекс выбранного пункта
    /// </summary>
    public int FocusedIndex { get { return _focusedIndex; } }

    /// <summary>
    /// Меню
    /// </summary>
    public Menu Menu { get { return _menu; } }

    /// <summary>
    /// Выбрать следующий пункт меню
    /// </summary>
    public void NextMenuItem()
    {
      if (_focusedIndex < _menu.Items.Count - 1)
      {
        _focusedIndex++;
        NeedRedrawEvent?.Invoke();
      }
    }

    /// <summary>
    /// Выбрать предыдущий пункт меню
    /// </summary>
    public void PreviousMenuItem()
    {
      if (_focusedIndex > 0)
      {
        _focusedIndex--;
        NeedRedrawEvent?.Invoke();
      }
    }

    /// <summary>
    /// Выделить пункт меню
    /// </summary>
    /// <param name="parIndex">Пункт меню</param>
    public void FocusMenuItem(int parIndex)
    {
       _focusedIndex = parIndex;
      NeedRedrawEvent?.Invoke();
    }

    /// <summary>
    /// Нажать на выбранный пункт меню
    /// </summary>
    public void Enter()
    {
      OnMenuClick?.Invoke(_menu.GetMenuItemByIndex(_focusedIndex));
    }

    /// <summary>
    /// Вызвать событие о необходимости перерисовки
    /// </summary>
    public void NeedRedraw()
    {
      NeedRedrawEvent?.Invoke();
    }

    /// <summary>
    /// Делегат о необходимости перерисовки
    /// </summary>
    public delegate void dNeedRedraw();
    /// <summary>
    /// Делегат на нажатие на пункт меню
    /// </summary>
    /// <param name="parSelectedMenuItem">Пункт меню</param>
    public delegate void MainMenuClick(MenuItem parSelectedMenuItem);
  }
}
