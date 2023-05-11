using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WormsGame.Base;
using WormsGame.Base.Weapons;

namespace WPFWorms.WPFView.ObjectsView
{
  /// <summary>
  /// Представление снаряда базуки
  /// </summary>
  public class BazookaProjectileView : PhysicalObjectView
  {
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parPhysicalObject">Физический объект</param>
    public BazookaProjectileView(PhysicalObject parPhysicalObject) : base(parPhysicalObject)
    {
    }

    /// <summary>
    /// Нарисовать снаряд базуки
    /// </summary>
    /// <param name="parCanvas">Канвас</param>
    public override void Draw(Canvas parCanvas)
    {
      BazookaProjectile bazookaProjectile = (BazookaProjectile)PhysicalObject;
      Ellipse projectileShape = new Ellipse();
      projectileShape.Width = bazookaProjectile.Width;
      projectileShape.Height = bazookaProjectile.Height;
      projectileShape.Fill = new SolidColorBrush(Colors.Blue);

      Canvas.SetLeft(projectileShape, bazookaProjectile.X);
      Canvas.SetTop(projectileShape, bazookaProjectile.Y);
      parCanvas.Children.Add(projectileShape);
    }
  }
}