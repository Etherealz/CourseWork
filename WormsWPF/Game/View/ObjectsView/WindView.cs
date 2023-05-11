using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WormsGame.Base;

namespace WPFWorms.WPFView.ObjectsView
{
  /// <summary>
  /// Представление ветра
  /// </summary>
  public class WindView : ObjectView
  {
    /// <summary>
    /// Минимальное положение указателя по X
    /// </summary>
    public const int POINTER_MIN_X = 1720;
    /// <summary>
    /// Максимальное положение указателя по X
    /// </summary>
    public const int POINTER_MAX_X = 1750;
    /// <summary>
    /// Смещение указателя по X за кадр
    /// </summary>
    public const double POINTER_STEP = 0.5;

    /// <summary>
    /// Изображение левого указателя
    /// </summary>
    private static ImageBrush _windLeftArrow = new ImageBrush(new BitmapImage(new Uri("leftArrow.png", UriKind.Relative)));
    /// <summary>
    /// Изображение правого указателя
    /// </summary>
    private static ImageBrush _windRightArrow = new ImageBrush(new BitmapImage(new Uri("rightArrow.png", UriKind.Relative)));

    /// <summary>
    /// Нужно ли смещать указатель влево
    /// </summary>
    private bool _isLeft = true;

    /// <summary>
    /// Текущее положение указателя по X
    /// </summary>
    public double PointerCurrentX = POINTER_MIN_X;

    /// <summary>
    /// Конструктор
    /// </summary>
    public WindView() : base()
    {

    }

    /// <summary>
    /// Нарисовать представление
    /// </summary>
    /// <param name="parCanvas"></param>
    public override void Draw(Canvas parCanvas)
    {
      if (GameModel.WindStrength != 0)
      {
        Rectangle windIndicator = new Rectangle();
        windIndicator.Width = 90;
        windIndicator.Height = 40;
        if (GameModel.WindStrength > 0)
        {
          windIndicator.Fill = _windRightArrow;
        }
        else if (GameModel.WindStrength < 0)
        {
          windIndicator.Fill = _windLeftArrow;
        }


        if (_isLeft)
        {
          PointerCurrentX -= POINTER_STEP;
          if (PointerCurrentX < POINTER_MIN_X)
          {
            _isLeft = false;
          }
        }
        else
        {
          PointerCurrentX += POINTER_STEP;
          if (PointerCurrentX > POINTER_MAX_X)
          {
            _isLeft = true;
          }
        }

        Canvas.SetLeft(windIndicator, PointerCurrentX);
        Canvas.SetTop(windIndicator, 100);
        parCanvas.Children.Add(windIndicator);
      }
      
    }
  }
}
