using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPFWorms.WPFView.ObjectsView
{
  /// <summary>
  /// Представление таймера
  /// </summary>
  public class TimeView : ObjectView
  {
    /// <summary>
    /// Координата X
    /// </summary>
    public int X { get; set; }
    /// <summary>
    /// Координата Y
    /// </summary>
    public int Y { get; set; }
    /// <summary>
    /// Время
    /// </summary>
    public int Time { get; set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parTime">Время</param>
    /// <param name="parX">Координата X</param>
    /// <param name="parY">Координата Y</param>
    public TimeView(int parTime, int parX, int parY) : base()
    {
      Time = parTime;
      X = parX;
      Y = parY;
    }

    /// <summary>
    /// Нарисовать представление
    /// </summary>
    /// <param name="parCanvas">Канвас</param>
    public override void Draw(Canvas parCanvas)
    {
      Rectangle backgroundTimerText = new Rectangle();
      backgroundTimerText.Width = 60;
      backgroundTimerText.Height = 60;

      backgroundTimerText.Fill = new SolidColorBrush(Colors.Black);

      Canvas.SetLeft(backgroundTimerText, X - backgroundTimerText.Width/2);
      Canvas.SetTop(backgroundTimerText, Y - backgroundTimerText.Height/2);
      parCanvas.Children.Add(backgroundTimerText);

      TextBlock timerText = new TextBlock();
      

      timerText.FontSize = 30;
      timerText.Foreground = new SolidColorBrush(Colors.White);
      timerText.Text = Time.ToString();

      if (Time.ToString().Length  == 2)
      {
        Canvas.SetLeft(timerText, X - 15);
      }
      if (Time.ToString().Length == 3)
      {
        Canvas.SetLeft(timerText, X - 24);
      }
      if (Time.ToString().Length == 1)
      {
        Canvas.SetLeft(timerText, X - 7);
      }

      Canvas.SetTop(timerText, Y - 20);

      parCanvas.Children.Add(timerText);
    }
  }
}
