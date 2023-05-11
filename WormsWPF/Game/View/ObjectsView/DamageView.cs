using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using WormsGame.Base;
using WormsWPF;

namespace WPFWorms.WPFView.ObjectsView
{
  /// <summary>
  /// Представление нанесенного по червяку урона
  /// </summary>
  public class DamageView : ObjectView
  {
    /// <summary>
    /// Макисмальное смещение по Y
    /// </summary>
    private const int MAX_BIAS = 55;
    /// <summary>
    /// Начальное смещение по Y
    /// </summary>
    private const int INITIAL_BIAS = 30;
    /// <summary>
    /// Шаг, с которым увеличивается смещение за 1 кадр
    /// </summary>
    private const double STEP = 0.3;

    /// <summary>
    /// Червяк, которому нанесен урон
    /// </summary>
    public Worm  DamagedWorm { get; set; }
    /// <summary>
    /// Урон
    /// </summary>
    public int Damage { get; set; }
    /// <summary>
    /// Смещение
    /// </summary>
    public double Bias { get; set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parWorm">Червяк</param>
    /// <param name="parDamage">Урон</param>
    public DamageView(Worm parWorm, int parDamage) : base()
    {
      DamagedWorm = parWorm;
      Damage = parDamage;
      Bias = INITIAL_BIAS;
    }

    /// <summary>
    /// Нарисовать представление
    /// </summary>
    /// <param name="parCanvas">Канвас</param>
    public override void Draw(Canvas parCanvas)
    {
      TextBlock damageText = new TextBlock();
      damageText.Text = "-" + Damage.ToString();
      damageText.Foreground = new SolidColorBrush(Colors.Red);

      Canvas.SetLeft(damageText, DamagedWorm.X - 5);
      Canvas.SetTop(damageText, DamagedWorm.Y - (int)Bias);
      parCanvas.Children.Add(damageText);
      Bias+=STEP;
      if (Bias > MAX_BIAS)
      {
        WPFGameView.RemoveObjectView(this);
        
      }
    }
  }
}
