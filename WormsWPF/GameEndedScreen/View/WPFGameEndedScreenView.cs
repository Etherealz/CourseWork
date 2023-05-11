using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WormsGame.GameEndedScreen.Model;
using WormsGame.GameEndedScreen.View;

namespace WormsWPF.GameEndedScreen.Model
{
  /// <summary>
  /// WPF представление экрана конца игры
  /// </summary>
  public class WPFGameEndedScreenView : GameEndedScreenView
  {
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
    public WPFGameEndedScreenView(GameEndedScreenModel parModel, Window parWindow) : base(parModel)
    {
      _window = parWindow;
    }

    /// <summary>
    /// Настроить канвас
    /// </summary>
    private void SetupCanvas()
    {
      _window.Dispatcher.Invoke(() =>
      {
        _canvas = new Canvas();
        _canvas.Width = _window.Width;
        _canvas.Height = _window.Height;
        _canvas.Background = new SolidColorBrush(Colors.Beige);
      });
      
    }

    /// <summary>
    /// Нарисовать представление
    /// </summary>
    public override void Draw()
    {
      _window.Dispatcher.Invoke(() =>
      {
        _canvas.Children.Clear();
        TextBlock endText = new TextBlock();
        endText.Text = Model.GetEndText();
        endText.FontSize = 26;
        endText.MaxWidth = 1720;
        endText.MaxHeight = 880;
        endText.TextWrapping = TextWrapping.Wrap;
        Canvas.SetTop(endText, 100);
        Canvas.SetLeft(endText, 100);

        _canvas.Children.Add(endText);

        TextBlock header = new TextBlock();
        header.Text = "Результаты игры. Нажмите Esc для возврата в меню";
        header.FontSize = 40;
        header.Foreground = new SolidColorBrush(Colors.DarkRed);
        Canvas.SetTop(header, 30);
        Canvas.SetLeft(header, 30);

       

        if (Model.IsRecord)
        {
          TextBlock name = new TextBlock();
          name.Text = Model.Name;
          name.FontSize = 60;
          name.Foreground = new SolidColorBrush(Colors.DarkRed);
          Canvas.SetTop(name, 400);
          Canvas.SetLeft(name, 800);

          _canvas.Children.Add(name);

          header.Text = "Результаты игры. Нажмите Esc для возврата в меню. Нажмите Enter для подтверждения имени.";
        }
        _canvas.Children.Add(header);

      });
      
      
    }

    /// <summary>
    /// Запустить
    /// </summary>
    public override void Start()
    {
      base.Start();
      SetupCanvas();
      Draw();
      _window.Dispatcher.Invoke(() =>
      {
        _window.Content = _canvas;
      });
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
