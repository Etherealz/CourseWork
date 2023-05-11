using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WormsGame.Records.Model;
using WormsGame.Records.View;

namespace WormsGame.Records.Controller
{
  /// <summary>
  /// Контроллер рекордов
  /// </summary>
  public abstract class RecordsController : BaseMVC.Controller<RecordsModel, RecordsView>
  {
    /// <summary>
    /// Событие возвращения в меню
    /// </summary>
    public event dBackToMenu? BackToMenuEvent;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parModel">Модель</param>
    /// <param name="parView">Представление</param>
    public RecordsController(RecordsModel parModel, RecordsView parView) : base(parModel, parView)
    {
    }

    /// <summary>
    /// Запустить
    /// </summary>
    public override void Start()
    {
      Model.UpdateRecords();
      base.Start();
    }

    /// <summary>
    /// Вернуться в меню
    /// </summary>
    public void BackToMenu()
    {
      BackToMenuEvent?.Invoke();
    }

    /// <summary>
    /// Делегат на возвращение в меню
    /// </summary>
    public delegate void dBackToMenu();

  }
}
