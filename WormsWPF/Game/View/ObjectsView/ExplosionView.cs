using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WormsGame.Base;
using WormsWPF;

namespace WPFWorms.WPFView.ObjectsView
{
  /// <summary>
  /// Представление взрыва
  /// </summary>
  public class ExplosionView : ObjectView
  {
    /// <summary>
    /// Шаг изменения радиуса взрва за кадр
    /// </summary>
    private const int STEP = 10;

    /// <summary>
    /// Координата X
    /// </summary>
    public int X { get; set; }
    /// <summary>
    /// Координата Y
    /// </summary>
    public int Y { get; set; }
    /// <summary>
    /// Текущий радус
    /// </summary>
    public int CurrentRadius { get; set; }
    /// <summary>
    /// Максимальный радиус
    /// </summary>
    public int MaxRadius { get; set; }
    
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parX">Координата X</param>
    /// <param name="parY">Координата Y</param>
    /// <param name="parRadius">Радуис</param>
    public ExplosionView(int parX, int parY, int parRadius) : base()
    {
      X = parX;
      Y = parY;
      CurrentRadius = 0;
      MaxRadius = parRadius;
    }

    /// <summary>
    /// Нарисовать представление
    /// </summary>
    /// <param name="parCanvas">Канвас</param>
    public override void Draw(Canvas parCanvas)
    {
      CurrentRadius += STEP;
      Ellipse explosion = new Ellipse();
      explosion.Width = CurrentRadius * 2;
      explosion.Height = CurrentRadius * 2;
      ImageBrush explosionBrush = new ImageBrush(new BitmapImage(new Uri("explosion.png", UriKind.Relative)));
      explosion.Fill = explosionBrush;
      Canvas.SetLeft(explosion, X - CurrentRadius);
      Canvas.SetTop(explosion, Y - CurrentRadius);
      parCanvas.Children.Add(explosion);
      
      if (CurrentRadius > MaxRadius)
      {
        WPFGameView.RemoveObjectView(this);
        
      }
    }
  }
}
