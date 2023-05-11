using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WormsGame.Base;

namespace WormsGame.View
{
  /// <summary>
  /// Представление игры
  /// </summary>
  public abstract class GameView : BaseMVC.View<GameModel>
  {
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parModel">Модель</param>
    public GameView(GameModel parModel) : base(parModel)
    {
    }
  }
}
