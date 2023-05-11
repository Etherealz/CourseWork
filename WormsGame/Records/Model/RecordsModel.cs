using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WormsGame.Records.Model
{
  /// <summary>
  /// Модель рекордов
  /// </summary>
  public class RecordsModel : BaseMVC.Model
  {
    /// <summary>
    /// Событие о необходимости перерисовки
    /// </summary>
    public event dNeedRedraw? NeedRedrawEvent;
    /// <summary>
    /// Рекорды
    /// </summary>
    private List<GameRecord> _records;

    /// <summary>
    /// Вызвать событие о необходимости перерисовки
    /// </summary>
    public void NeedRedraw()
    {
      NeedRedrawEvent?.Invoke();
    }

    /// <summary>
    /// Получить рекорды
    /// </summary>
    /// <returns></returns>
    public List<GameRecord> GetRecords()
    {
      return _records;
    }

    /// <summary>
    /// Обновить рекорды
    /// </summary>
    public void UpdateRecords()
    {
      _records = RecordsRepository.Instance.GetAllRecords();
    }

    /// <summary>
    /// Делегат о необходимости перерисовки
    /// </summary>
    public delegate void dNeedRedraw();

  }
}
