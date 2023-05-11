using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WormsGame.Help.Model;
using WormsGame.Help.View;

namespace WormsWPF.Help.View
{
  /// <summary>
  /// WPF представление справки
  /// </summary>
  public class WPFHelpView : HelpView
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
    /// <param name="parModel"></param>
    /// <param name="parWindow"></param>
    public WPFHelpView(HelpModel parModel, Window parWindow) : base(parModel)
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
      Draw();
    }

    /// <summary>
    /// Нарисовать представление
    /// </summary>
    public override void Draw()
    {
      TextBlock helpText = new TextBlock();
      helpText.Text = Model.GetHelpText();
      helpText.TextWrapping = TextWrapping.Wrap;
      helpText.FontSize = 26;
      helpText.MaxWidth = 1720;
      helpText.MaxHeight = 880;
      Canvas.SetTop(helpText, 100);
      Canvas.SetLeft(helpText, 100);

      _canvas.Children.Add(helpText);

      TextBlock header = new TextBlock();
      header.Text = "Справка по игре Worms. Нажмите Esc для возврата в меню";
      header.FontSize = 40;
      header.Foreground = new SolidColorBrush(Colors.DarkRed);
      Canvas.SetTop(header, 30);
      Canvas.SetLeft(header, 30);

      _canvas.Children.Add(header);
    }

    /// <summary>
    /// Запустить
    /// </summary>
    public override void Start()
    {
      base.Start();
      SetupCanvas();
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
