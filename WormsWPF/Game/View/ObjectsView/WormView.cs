using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WormsGame.Base;
using WormsGame.Base.Weapons;
using WormsWPF;

namespace WPFWorms.WPFView.ObjectsView
{
  /// <summary>
  /// Представление червяка
  /// </summary>
  public class WormView : PhysicalObjectView
  {
    /// <summary>
    /// Минимальное положение указателя по Y
    /// </summary>
    public const int POINTER_MIN_Y = 50;
    /// <summary>
    /// Максимальное положение указателя по Y
    /// </summary>
    public const int POINTER_MAX_Y = 60;
    /// <summary>
    /// Шаг указателя по X
    /// </summary>
    public const double POINTER_STEP = 0.3;

    /// <summary>
    /// Изображение червяка, повернутого влево
    /// </summary>
    private static ImageBrush wormBrushLeft = new ImageBrush(new BitmapImage(new Uri("wormLeft.png", UriKind.Relative)));
    /// <summary>
    /// Изображение червяка, повернутого вправо
    /// </summary>
    private static ImageBrush wormBrushRight = new ImageBrush(new BitmapImage(new Uri("wormRight.png", UriKind.Relative)));
    /// <summary>
    /// Изображени указателя
    /// </summary>
    private static ImageBrush pointerBrush = new ImageBrush(new BitmapImage(new Uri("arrow.png", UriKind.Relative)));
    /// <summary>
    /// Изображение прицела
    /// </summary>
    private static ImageBrush aimBrush = new ImageBrush(new BitmapImage(new Uri("aim.png", UriKind.Relative)));
    /// <summary>
    /// Изображение иконки базуки
    /// </summary>
    private static ImageBrush bazookaBrush = new ImageBrush(new BitmapImage(new Uri("baz.png", UriKind.Relative)));
    /// <summary>
    /// Изображение иконки гранаты
    /// </summary>
    private static ImageBrush grenadeBrush = new ImageBrush(new BitmapImage(new Uri("gren.png", UriKind.Relative)));

    /// <summary>
    /// Нужно ли смещать указатель вверх
    /// </summary>
    private bool _isUp;

    /// <summary>
    /// Червяк
    /// </summary>
    public Worm Worm { get; set; }
    /// <summary>
    /// Текущее положение указателя по Y
    /// </summary>
    public double PointerCurrentY { get; set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parPhysicalObject">Физический объект</param>
    public WormView(PhysicalObject parPhysicalObject) : base(parPhysicalObject)
    {
      Worm = parPhysicalObject as Worm;
      PointerCurrentY = POINTER_MIN_Y;
      _isUp = true;
    }

    /// <summary>
    /// Нарисовать представление
    /// </summary>
    /// <param name="parCanvas">Канвас</param>
    public override void Draw(Canvas parCanvas)
    {
      DrawWormShape(parCanvas);
      DrawWormHealth(parCanvas);
      if (Worm.Equals(GameModel.GetCurrentWorm()))
      {
        DrawWormPointer(parCanvas);
        DrawWormWeapon(parCanvas);
      } 
      
    }
    /// <summary>
    /// Нарисовать тело червяка
    /// </summary>
    /// <param name="parCanvas">Канвас</param>
    private void DrawWormShape(Canvas parCanvas)
    {
      Rectangle wormShape = new Rectangle();
      wormShape.Width = 22;
      wormShape.Height = 26;

      if (Worm.State == State.Right)
      {
        wormShape.Fill = wormBrushRight;
      }
      else if (Worm.State == State.Left)
      {
        wormShape.Fill = wormBrushLeft;
      }

      Canvas.SetLeft(wormShape, Worm.X - 5);
      Canvas.SetTop(wormShape, Worm.Y - 3);
      parCanvas.Children.Add(wormShape); 
    }

    /// <summary>
    /// Нарисовать здоровье червяка
    /// </summary>
    /// <param name="parCanvas">Канвас</param>
    private void DrawWormHealth(Canvas parCanvas)
    {
      Rectangle healthBackground = new Rectangle();
      healthBackground.Width = 30;
      healthBackground.Height = 15;
      //выбор цвета для червяка
      switch (Worm.Player.TeamColor)
      {
        case PlayersColor.Orange:
          healthBackground.Fill = new SolidColorBrush(Colors.DarkRed);
          break;
        case PlayersColor.Blue:
          healthBackground.Fill = new SolidColorBrush(Colors.DarkBlue);
          break;
        case PlayersColor.Green:
          healthBackground.Fill = new SolidColorBrush(Colors.DarkGreen);
          break;
        case PlayersColor.Yellow:
          healthBackground.Fill = new SolidColorBrush(Colors.DarkOrange);
          break;
        default:
          healthBackground.Fill = new SolidColorBrush(Colors.Red);
          break;
      }


      Canvas.SetLeft(healthBackground, Worm.X - 10);
      Canvas.SetTop(healthBackground, Worm.Y - 20);
      parCanvas.Children.Add(healthBackground);


      TextBlock healthText = new TextBlock();
      healthText.Text = Worm.Health.ToString();
      healthText.Foreground = new SolidColorBrush(Colors.White);

      if (healthText.Text.Length == 3)
      {
        Canvas.SetLeft(healthText, Worm.X - 5);
      }
      if (healthText.Text.Length <= 2)
      {
        Canvas.SetLeft(healthText, Worm.X - 2);
      }

      Canvas.SetTop(healthText, Worm.Y - 21);


      parCanvas.Children.Add(healthText);
    }

    /// <summary>
    /// Нарисовать оружие червяка
    /// </summary>
    /// <param name="parCanvas">Канвас</param>
    private void DrawWormWeapon(Canvas parCanvas)
    {
      if (Worm.Weapon != null)
      {
        DrawAim(parCanvas);
        DrawWeaponCount(parCanvas);
        DrawWeaponIcon(parCanvas);
        if (Worm.Weapon.Power > Weapon.INITIAL_POWER)
        {
          DrawWeaponPower(parCanvas);
        }
      }
    }

    /// <summary>
    /// Нарисовать заряд оружия
    /// </summary>
    /// <param name="parCanvas">Канвас</param>
    private void DrawWeaponPower(Canvas parCanvas)
    {
      Line power = new Line();
      power.Stroke = new SolidColorBrush(Colors.DarkRed);
      power.StrokeThickness = 7;
      int yPower = (int)(Worm.Weapon.Power * Math.Sin(Worm.Weapon.Angle * Math.PI / 180)) * 3;
      int xPower = (int)(Worm.Weapon.Power * Math.Cos(Worm.Weapon.Angle * Math.PI / 180)) * 3;
      power.X1 = Worm.GetCenterX();
      power.Y1 = Worm.GetCenterY();
      if (Worm.State == State.Right)
      {
        power.X2 = power.X1 + xPower;
      }
      else if (Worm.State == State.Left)
      {
        power.X2 = power.X1 - xPower;
      }
      power.Y2 = power.Y1 + yPower;
      parCanvas.Children.Add(power);
    }

    /// <summary>
    /// Нарисовать иконку оружия
    /// </summary>
    /// <param name="parCanvas">Канвас</param>
    /// <exception cref="Exception"></exception>
    private void DrawWeaponIcon(Canvas parCanvas)
    {
      Rectangle weaponIcon = new Rectangle();
      weaponIcon.Width = 50;
      weaponIcon.Height = 50;

      switch (Worm.Weapon)
      {
        case Bazooka:
          weaponIcon.Fill = bazookaBrush;
          break;
        case Grenade gren:
          weaponIcon.Fill = grenadeBrush;
          break;
        default:
          throw new Exception("Оружие неизвестного типа!");
      }

      Canvas.SetLeft(weaponIcon, 1740);
      Canvas.SetTop(weaponIcon, 950);
      parCanvas.Children.Add(weaponIcon);
    }

    /// <summary>
    /// Нарисовать кол-во оружия
    /// </summary>
    /// <param name="parCanvas">Канвас</param>
    private static void DrawWeaponCount(Canvas parCanvas)
    {
      Rectangle weaponCountBackground = new Rectangle();
      weaponCountBackground.Width = 50;
      weaponCountBackground.Height = 50;

      weaponCountBackground.Fill = new SolidColorBrush(Colors.Black);

      Canvas.SetLeft(weaponCountBackground, 1800);
      Canvas.SetTop(weaponCountBackground, 950);
      parCanvas.Children.Add(weaponCountBackground);


      TextBlock weaponCount = new TextBlock();
      weaponCount.Text = GameModel.GetCurrentWorm().Weapon.UsesNumber.ToString();
      weaponCount.Foreground = new SolidColorBrush(Colors.White);

      weaponCount.FontSize = 30;
      Canvas.SetLeft(weaponCount, 1817);
      Canvas.SetTop(weaponCount, 955);
      parCanvas.Children.Add(weaponCount);
    }

    /// <summary>
    /// Нарисовать прицел
    /// </summary>
    /// <param name="parCanvas">Канвас</param>
    private void DrawAim(Canvas parCanvas)
    {
      Rectangle aim = new Rectangle();
      aim.Width = 26;
      aim.Height = 26;

      aim.Fill = aimBrush;

      int len = 50;
      int y = (int)(len * Math.Sin(Worm.Weapon.Angle * Math.PI / 180));
      int x = (int)(len * Math.Cos(Worm.Weapon.Angle * Math.PI / 180));

      if (Worm.State == State.Right)
      {
        Canvas.SetLeft(aim, Worm.GetCenterX() + x - 13);
      }
      else if (Worm.State == State.Left)
      {
        Canvas.SetLeft(aim, Worm.GetCenterX() - 13 - x);
      }

      Canvas.SetTop(aim, Worm.GetCenterY() - 13 + y);
      parCanvas.Children.Add(aim);
    }

    /// <summary>
    /// Нарисовать указатель на червяка
    /// </summary>
    /// <param name="parCanvas">Канвас</param>
    private void DrawWormPointer(Canvas parCanvas)
    {
      Rectangle pointer = new Rectangle();
      pointer.Width = 30;
      pointer.Height = 30;

      pointer.Fill = pointerBrush;

      Canvas.SetLeft(pointer, Worm.X - 10);
      Canvas.SetTop(pointer, Worm.Y - PointerCurrentY);

      if (_isUp)
      {
        PointerCurrentY += POINTER_STEP;
        if (PointerCurrentY > POINTER_MAX_Y)
        {
          _isUp = false;
        }
      }
      else
      {
        PointerCurrentY -= POINTER_STEP;
        if (PointerCurrentY < POINTER_MIN_Y)
        {
          _isUp = true;
        }
      }

      parCanvas.Children.Add(pointer);
    }

  }
}
