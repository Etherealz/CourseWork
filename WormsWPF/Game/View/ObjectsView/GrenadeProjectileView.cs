using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WormsGame.Base;
using WormsGame.Base.Weapons;

namespace WPFWorms.WPFView.ObjectsView
{
  /// <summary>
  /// Представление гранаты
  /// </summary>
  public class GrenadeProjectileView : PhysicalObjectView
  {
    /// <summary>
    /// Изображение для гаранаты
    /// </summary>
    private static ImageBrush grenadeBrush = new ImageBrush(new BitmapImage(new Uri("grenade.png", UriKind.Relative)));

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parPhysicalObject">Физический объект</param>
    public GrenadeProjectileView(PhysicalObject parPhysicalObject) : base(parPhysicalObject)
    {
    }

    /// <summary>
    /// Нарисовать представление
    /// </summary>
    /// <param name="parCanvas">Канвас/param>
    public override void Draw(Canvas parCanvas)
    {
      GrenadeProjectile grenade = (GrenadeProjectile)PhysicalObject;

      TextBlock grenadeTimeLast = new TextBlock();
      grenadeTimeLast.Text = grenade.TimeLast.ToString();
      grenadeTimeLast.Foreground = new SolidColorBrush(Colors.Black);
      grenadeTimeLast.FontWeight = FontWeights.UltraBold;

      Canvas.SetLeft(grenadeTimeLast, grenade.GetCenterX() - 4);
      Canvas.SetTop(grenadeTimeLast, grenade.Y - 21);

      parCanvas.Children.Add(grenadeTimeLast);

      GrenadeProjectile grenadeProjectile = (GrenadeProjectile)PhysicalObject;
      Ellipse projectileShape = new Ellipse();
      projectileShape.Width = grenadeProjectile.Width;
      projectileShape.Height = grenadeProjectile.Height;
      projectileShape.Fill = grenadeBrush;

      Canvas.SetLeft(projectileShape, grenadeProjectile.X);
      Canvas.SetTop(projectileShape, grenadeProjectile.Y);
      parCanvas.Children.Add(projectileShape);
    }
  }
}