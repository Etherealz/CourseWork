using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WormsGame.BaseMVC
{
  /// <summary>
  /// Представление
  /// </summary>
  /// <typeparam name="M"></typeparam>
  public abstract class View<M>
    where M : Model
  {
    /// <summary>
    /// Запущено ли представление
    /// </summary>
    private bool _isStarted = false;
    /// <summary>
    /// Модель
    /// </summary>
    private readonly M _model;

    /// <summary>
    /// Запущено ли представление
    /// </summary>
    public bool IsStarted { get { return _isStarted; } set { _isStarted = value; } }
    /// <summary>
    /// Модель
    /// </summary>
    public M Model { get { return _model; } }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parModel">Модель</param>
    public View(M parModel)
    {
      _model = parModel;
    }

    /// <summary>
    /// Нарисовать представление
    /// </summary>
    public abstract void Draw();

    /// <summary>
    /// Запустить
    /// </summary>
    public virtual void Start()
    {
      if (!_isStarted)
      {
        _isStarted = true;
      }
    }

    /// <summary>
    /// Остановить
    /// </summary>
    public virtual void Stop()
    {
      if (_isStarted)
      {
        _isStarted = false;
      }
    }
  }
}
