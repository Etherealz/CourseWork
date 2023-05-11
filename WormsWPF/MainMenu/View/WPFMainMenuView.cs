using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WormsGame.MainMenu.Model;
using WormsGame.MainMenu.View;

namespace WormsWPF.MainMenu.View
{
  /// <summary>
  /// WPF представление главного меню
  /// </summary>
  public class WPFMainMenuView : MainMenuView
  {
    /// <summary>
    /// Ширина кнопки
    /// </summary>
    private const int BUTTON_WIDTH = 250;
    /// <summary>
    /// Высота кнопки
    /// </summary>
    private const int BUTTON_HEIGHT = 50;
    /// <summary>
    /// Расстояние по Y между кнопками меню
    /// </summary>
    private const int Y_BIAS = 100;
    /// <summary>
    /// Начальный Y кнопок меню
    /// </summary>
    private const int Y_START = 350;

    /// <summary>
    /// Окно
    /// </summary>
    private readonly Window _window;
    /// <summary>
    /// Канвас
    /// </summary>
    private Canvas _canvas;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parModel">Модель</param>
    /// <param name="parWindow">Окно</param>
    public WPFMainMenuView(MainMenuModel parModel, Window parWindow) : base(parModel)
    {
      _window = parWindow;
    }

    /// <summary>
    /// Настроить канвас
    /// </summary>
    private void SetupCanvas()
    {
      _canvas = new Canvas();
      _canvas.Width = _window.Width;
      _canvas.Height = _window.Height;
      _canvas.Background = new SolidColorBrush(Colors.Beige);
      
    }

    /// <summary>
    /// Нарисовать представление
    /// </summary>
    public override void Draw()
    {
      _canvas.Children.Clear();
      WormsGame.MainMenu.Menu menu = Model.Menu;
      for (int i = 0; i < menu.Items.Count; i++)
      {
        string text = MenuItemTextFormer.GetTitle(menu.GetMenuItemByIndex(i));

        Rectangle button = new Rectangle();
        button.Width = BUTTON_WIDTH;
        button.Height = BUTTON_HEIGHT;
        if (Model.FocusedIndex == i)
        {
          button.Fill = new SolidColorBrush(Colors.DarkRed);
        }
        else
        {
          button.Fill = new SolidColorBrush(Colors.DarkOrange);
        }
        Canvas.SetLeft(button, WPFWormsApplication.WINDOW_WIDTH / 2 - BUTTON_WIDTH / 2);
        Canvas.SetTop(button, Y_START + Y_BIAS * i);

        _canvas.Children.Add(button);

        TextBlock buttonText = new TextBlock();
        buttonText.Text = text;
        buttonText.FontSize = 28;
        buttonText.Foreground = new SolidColorBrush(Colors.White);
        buttonText.Padding = new Thickness(0, 0, 0, 0);
        Canvas.SetLeft(buttonText, WPFWormsApplication.WINDOW_WIDTH / 2 - (15 * text.Length) / 2 );
        Canvas.SetTop(buttonText, 2 + Y_START + Y_BIAS * i);

        _canvas.Children.Add(buttonText);
      }
    }

    /// <summary>
    /// Запустить
    /// </summary>
    public override void Start()
    {
      base.Start();
      SetupCanvas();
      Draw();
      _window.Content = _canvas;
    }

    /// <summary>
    /// Остановить
    /// </summary>
    public override void Stop()
    {
      _window.Content = null;
      base.Stop();
    }
  }
}
