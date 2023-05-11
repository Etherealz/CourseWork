using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WormsGame.MainMenu;
using WormsGame.MainMenu.Model;
using WormsGame.MainMenu.View;

namespace WormsConsole.MainMenu.View
{
  /// <summary>
  /// Консольное представление главного меню
  /// </summary>
  public class ConsoleMainMenuView : MainMenuView
  {
    /// <summary>
    /// Ширина кнопки
    /// </summary>
    private const int BUTTON_WIDTH = 40;
    /// <summary>
    /// Высота кнопки
    /// </summary>
    private const int BUTTON_HEIGHT = 5;
    /// <summary>
    /// Расстояние по Y между кнопками
    /// </summary>
    private const int Y_BIAS = 10;
    /// <summary>
    /// Y, от которого начинается отрисовка кнопок
    /// </summary>
    private const int Y_START = 8;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parModel">Модель</param>
    public ConsoleMainMenuView(MainMenuModel parModel) : base(parModel)
    {
    }

    /// <summary>
    /// Настроить окно
    /// </summary>
    public void SetupWindow()
    {
      FastConsoleWorker.SetFontSize(8, 14);
      Console.SetWindowSize(120, 50);

      Console.BackgroundColor = ConsoleColor.DarkGray;
      Console.Clear();

    }

    /// <summary>
    /// Нарисовать представление
    /// </summary>
    public override void Draw()
    {
      Menu menu = Model.Menu;
      for (int i = 0; i < menu.Items.Count; i++)
      {
        string text = MenuItemTextFormer.GetTitle(menu.GetMenuItemByIndex(i));

        ConsoleColor color;
        if (Model.FocusedIndex == i)
        {
          color = ConsoleColor.DarkRed;
        }
        else
        {
          color = ConsoleColor.DarkYellow;
        }
        
        ConsoleDrawer.DrawRectangle(Console.WindowWidth / 2 - BUTTON_WIDTH / 2, Y_START + Y_BIAS * i, BUTTON_WIDTH, BUTTON_HEIGHT, color);
        Console.BackgroundColor = color;
        Console.SetCursorPosition(Console.WindowWidth / 2 - text.Length / 2, Y_START + Y_BIAS * i + BUTTON_HEIGHT/2);
        Console.Write(text);

      }
      
    }

    /// <summary>
    /// Запустить
    /// </summary>
    public override void Start()
    {
      Console.Clear();
      SetupWindow();
      Draw();
      base.Start();
    }

    /// <summary>
    /// Остановить
    /// </summary>
    public override void Stop()
    {
      base.Stop();
      Console.Clear();
    }
  }
}
