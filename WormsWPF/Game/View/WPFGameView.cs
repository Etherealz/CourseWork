using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WormsGame.Base;
using WormsGame.Base.Weapons;
using WormsGame.View;
using WPFWorms.WPFView;
using WPFWorms.WPFView.ObjectsView;

namespace WormsWPF
{
  /// <summary>
  /// WPF представление игры
  /// </summary>
  public class WPFGameView : GameView
  {
    /// <summary>
    /// Изображение карты
    /// </summary>
    private WriteableBitmap writeableBitmap;
    /// <summary>
    /// Изображение карты
    /// </summary>
    private Image _mapImage;
    /// <summary>
    /// Источник изображения карты
    /// </summary>
    private ImageSource _imageSource;

    /// <summary>
    /// Таймер игры
    /// </summary>
    private TimeView _gameTimer = new TimeView(0, 50, 100);
    /// <summary>
    /// Таймер хода
    /// </summary>
    private TimeView _turnTimer = new TimeView(0, 50, 1000);

    /// <summary>
    /// Представление здоровья игроков
    /// </summary>
    private PlayerHealthView _playersHealth = new PlayerHealthView();
    /// <summary>
    /// Представление ветра
    /// </summary>
    private WindView _windView = new WindView();

    /// <summary>
    /// Окно
    /// </summary>
    private readonly Window _window;
    /// <summary>
    /// Канвас
    /// </summary>
    private Canvas _canvas;

    /// <summary>
    /// Представления объектов для отрисовки
    /// </summary>
    private static List<ObjectView> _objectViews = new List<ObjectView>();
    /// <summary>
    /// Нужно ли отрисовывать
    /// </summary>
    private bool _isNeedDraw;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parModel">Модель</param>
    /// <param name="parWindow">Окно</param>
    public WPFGameView(GameModel parModel, Window parWindow) : base(parModel)
    {
      _window = parWindow;

    }

    /// <summary>
    /// Инициализировать представление
    /// </summary>
    private void ViewInit()
    {
      SetHandlers();

      _objectViews.Clear();
      _objectViews.Add(_gameTimer);
      _objectViews.Add(_turnTimer);
      _objectViews.Add(_playersHealth);
      _objectViews.Add(_windView);

      SetWindow();

      PrepareMap();
    }

    /// <summary>
    /// Настроить окно
    /// </summary>
    private void SetWindow()
    {
      _canvas = new Canvas();
      _canvas.Width = _window.Width;
      _canvas.Height = _window.Height;
      _window.Content = _canvas;
    }

    /// <summary>
    /// Присвоить обработчики
    /// </summary>
    private void SetHandlers()
    {
      ExplosionMaker.ExplosionEvent += DestroyMap;
      GameModel.DamageGainedEvent += ShowDamageGained;
      GameModel.NewObjectAddedEvent += AddNewView;
      GameModel.ObjectDeletedEvent += DeletePhysicalObjectView;
      GameTimers.TurnTimeChangedEvent += ChangeTurnTime;
      GameTimers.GameTimeChangedEvent += ChangeGameTime;
    }

    /// <summary>
    /// Убрать обработчики
    /// </summary>
    private void UnSetHandlers()
    {
      ExplosionMaker.ExplosionEvent -= DestroyMap;
      GameModel.DamageGainedEvent -= ShowDamageGained;
      GameModel.NewObjectAddedEvent -= AddNewView;
      GameModel.ObjectDeletedEvent -= DeletePhysicalObjectView;
      GameTimers.TurnTimeChangedEvent -= ChangeTurnTime;
      GameTimers.GameTimeChangedEvent -= ChangeGameTime;
    }

    /// <summary>
    /// Изменить время у представления таймера игры
    /// </summary>
    /// <param name="parTime">Время</param>
    private void ChangeGameTime(int parTime)
    {
      _gameTimer.Time = parTime;
    }

    /// <summary>
    /// Изменить время у представления таймера хода
    /// </summary>
    /// <param name="parTime"></param>
    private void ChangeTurnTime(int parTime)
    {
      _turnTimer.Time = parTime;
    }

    /// <summary>
    /// Удалить представление физического объекта из списка представлений
    /// </summary>
    /// <param name="parPhycicalObject">Физический объект</param>
    private void DeletePhysicalObjectView(PhysicalObject parPhycicalObject)
    {
      lock (_objectViews)
      {
        for (int i = 0; i < _objectViews.Count; i++)
        {
          ObjectView view = _objectViews[i];
          if (view is PhysicalObjectView physicalObjectView)
          {
            if (physicalObjectView.PhysicalObject.Equals(parPhycicalObject))
            {
              _objectViews.RemoveAt(i);
              break;
            }
          }
        }
      }

    }

    /// <summary>
    /// Добавить представление физического объекта в список представлений
    /// </summary>
    /// <param name="parPhysicalObject">Физический объект</param>
    private void AddNewView(PhysicalObject parPhysicalObject)
    {
      PhysicalObjectView physicalObjectView = PhysicalObjectViewFormer.GetShape(parPhysicalObject);
      AddObjectView(physicalObjectView);

    }

    /// <summary>
    /// Добавить представление в список представлений объектов
    /// </summary>
    /// <param name="parObjectView">Представление объекта</param>
    public static void AddObjectView(ObjectView parObjectView)
    {
      new Thread(() =>
      {
        lock (_objectViews)
        {
          _objectViews.Add(parObjectView);
        }
      }).Start();
      
    }

    /// <summary>
    /// Удалить представление из списка представлений объектов
    /// </summary>
    /// <param name="parObjectView">Представление объекта</param>
    public static void RemoveObjectView(ObjectView parObjectView)
    {
      new Thread(() =>
      {
        lock (_objectViews)
        {
          _objectViews.Remove(parObjectView);
        }
      }).Start();
    }

    /// <summary>
    /// Начать отрисовку
    /// </summary>
    private void StartDrawing()
    {
      _isNeedDraw = true;
      new Thread(Draw).Start();
    }

    /// <summary>
    /// Разрушить часть карты взрывом
    /// </summary>
    /// <param name="parX">Координата X центра взрыва</param>
    /// <param name="parY">Координата Y центра взрыва</param>
    /// <param name="parRadius">Радиус взрыва</param>
    private void DestroyMap(int parX, int parY, int parRadius)
    {
      ExplosionView explosionView = new ExplosionView(parX, parY, parRadius);
      lock (_objectViews)
      {
        _objectViews.Add(explosionView);
      }

      new Thread(() => {
        _window.Dispatcher.Invoke(() => {
          byte[] colorData1 = { 0, 0, 0, 0 };
          for (int i = parX - parRadius; i <= parX + parRadius; i++)
          {
            for (int j = parY - parRadius; j <= parY + parRadius; j++)
            {
              if (i > 0 && i < 1920 && j > 0 && j < 1080)
              {
                if (ExplosionMaker.IsInExplosion(i, j, parX, parY, parRadius))
                {
                  Int32Rect rect = new Int32Rect(i, j, 1, 1);
                  writeableBitmap.WritePixels(rect, colorData1, 4, 0);
                }
              }

            }
          }

          _imageSource = writeableBitmap;


        });
      }).Start();



    }

    /// <summary>
    /// Показать нанесенный урон червяку
    /// </summary>
    /// <param name="parWorm">Червяк</param>
    /// <param name="parDamage">Урон</param>
    private void ShowDamageGained(Worm parWorm, int parDamage)
    {
      DamageView damageView = new DamageView(parWorm, parDamage);
      lock (_objectViews)
      {
        _objectViews.Add(damageView);
      }
    }

    /// <summary>
    /// Приготовить карту
    /// </summary>
    private void PrepareMap()
    {
      _mapImage = new Image();

      _imageSource = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\front.png", UriKind.Absolute));
      writeableBitmap = new WriteableBitmap((BitmapSource)_imageSource);
      _imageSource = writeableBitmap;
      _mapImage.Source = _imageSource;

      ImageBrush imageBrush = new ImageBrush(new BitmapImage(new Uri("background.png", UriKind.Relative)));
      _canvas.Background = imageBrush;

    }

    /// <summary>
    /// Рисовать карту
    /// </summary>
    public override void Draw()
    {
      while (_isNeedDraw)
      {
        Thread.Sleep(8);
        _window.Dispatcher.Invoke(() =>
        {

          _canvas.Children.Clear();
          _canvas.Children.Add(_mapImage);

          lock (_objectViews)
          {
            foreach (ObjectView elObjectView in _objectViews)
            {
              elObjectView.Draw(_canvas);
            }
          }

        });
      }
    }

    /// <summary>
    /// Запустить
    /// </summary>
    public override void Start()
    {
      ViewInit();
      StartDrawing();
      base.Start();
    }

    /// <summary>
    /// Остановить
    /// </summary>
    public override void Stop()
    {
      UnSetHandlers();
      _window.Dispatcher.Invoke(() =>
      {
        _window.Content = null;
      });
      _isNeedDraw = false;
      base.Stop();

    }

  }
}
