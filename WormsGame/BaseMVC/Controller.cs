using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WormsGame.BaseMVC
{
  /// <summary>
  /// Контроллер
  /// </summary>
  /// <typeparam name="M"></typeparam>
  /// <typeparam name="V"></typeparam>
  public abstract class Controller<M, V>
    where M : Model
    where V : View<M>
  {
    /// <summary>
    /// Модель
    /// </summary>
    private readonly M _model;
    /// <summary>
    /// Представление
    /// </summary>
    private readonly V _view;

    /// <summary>
    /// Модель
    /// </summary>
    public M Model { get { return _model; } }
    /// <summary>
    /// Представление
    /// </summary>
    public V View { get { return _view; } }

    /// <summary>
    /// Констурктор
    /// </summary>
    /// <param name="parModel">Модель</param>
    /// <param name="parView">Представление</param>
    public Controller(M parModel, V parView)
    {
      _model = parModel;
      _view = parView;
    }

    /// <summary>
    /// Запустить
    /// </summary>
    public virtual void Start()
    {
      _view.Start();
    }

    /// <summary>
    /// Остановить
    /// </summary>
    public virtual void Stop()
    {
      _view.Stop();
    }
  }
}
