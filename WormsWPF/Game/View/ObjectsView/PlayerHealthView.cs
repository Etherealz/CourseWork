using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WormsGame.Base;

namespace WPFWorms.WPFView.ObjectsView
{
  /// <summary>
  /// Представление здоровья команды игрока
  /// </summary>
  public class PlayerHealthView : ObjectView
  {
    /// <summary>
    /// Конструктор
    /// </summary>
    public PlayerHealthView() : base()
    {
    }

    /// <summary>
    /// Нарисовать представлние
    /// </summary>
    /// <param name="parCanvas">Канвас</param>
    public override void Draw(Canvas parCanvas)
    {
      for (int i = GameModel.Players.Count - 1; i >= 0 ; i--)
      {
        Player player = GameModel.Players[i];
        if (player.Worms.Count != 0)
        {
          Rectangle playerHealthBar = new Rectangle();
          playerHealthBar.Width = player.Worms.Count * 30;
          playerHealthBar.Height = 20;

          switch (player.TeamColor)
          {
            case PlayersColor.Orange:
              playerHealthBar.Fill = new SolidColorBrush(Colors.DarkRed);
              break;
            case PlayersColor.Blue:
              playerHealthBar.Fill = new SolidColorBrush(Colors.DarkBlue);
              break;
            case PlayersColor.Green:
              playerHealthBar.Fill = new SolidColorBrush(Colors.DarkGreen);
              break;
            case PlayersColor.Yellow:
              playerHealthBar.Fill = new SolidColorBrush(Colors.DarkOrange);
              break;
            default:
              playerHealthBar.Fill = new SolidColorBrush(Colors.Red);
              break;
          }

          Canvas.SetLeft(playerHealthBar, 900);
          Canvas.SetTop(playerHealthBar, 1000 - (30 * (GameModel.Players.Count - i - 1)));
          parCanvas.Children.Add(playerHealthBar);
        }
        
      }
    }
  }
}
