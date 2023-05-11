using WormsConsole.ViewConsole.ViewObjects;
using WormsGame.Base;
using WormsGame.View;
using static WormsConsole.FastConsoleWorker;

namespace WormsConsole.ViewConsole
{
  /// <summary>
  /// Консольное представление игры
  /// </summary>
  public class ConsoleGameView : GameView
  {
    /// <summary>
    /// Ширина экрана
    /// </summary>
    public const int SCREEN_WIDTH = 320;
    /// <summary>
    /// Высота экрана
    /// </summary>
    public const int SCREEN_HEIGHT = 180;
    /// <summary>
    /// Коэффициент сжатия
    /// </summary>
    public const int COMPRESSION_RATIO = 6;

    /// <summary>
    /// Таймер хода
    /// </summary>
    private TurnTimeView _turnTimer = new TurnTimeView(10, SCREEN_HEIGHT - 10, 30);
    /// <summary>
    /// Таймер игры
    /// </summary>
    private GameTimeView _gameTimer = new GameTimeView(10, 10, SCREEN_WIDTH - 20);
    /// <summary>
    /// Список представлений объектов для отрисовки
    /// </summary>
    private static List<ObjectView> _objectViews = new List<ObjectView>();

    /// <summary>
    /// Нужно ли рисовать
    /// </summary>
    private bool _isNeedDraw;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parModel">Модель</param>
    public ConsoleGameView(GameModel parModel) : base(parModel)
    { 
    }

    /// <summary>
    /// Инициализация представления
    /// </summary>
    public void ViewInit()
    {
      FastConsoleWorker.SetFontSize(4, 5);
      _isNeedDraw = true;
      Console.WindowWidth = SCREEN_WIDTH;
      Console.WindowHeight = SCREEN_HEIGHT;
      FastConsoleWorker.Init(SCREEN_WIDTH, SCREEN_HEIGHT);

      GameModel.NewObjectAddedEvent += AddNewView;
      GameModel.ObjectDeletedEvent += DeletePhysicalObjectView;

      _objectViews.Clear();
      _objectViews.Add(_turnTimer);
      _objectViews.Add(_gameTimer);
    }

    /// <summary>
    /// Удалить представление физического объекта из списка представлений 
    /// </summary>
    /// <param name="parPhycicalObject"></param>
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
    /// <param name="parPhysicalObject"></param>
    private void AddNewView(PhysicalObject parPhysicalObject)
    {
      PhysicalObjectView physicalObjectView = PhysicalObjectViewFormer.GetShape(parPhysicalObject);
      lock (_objectViews)
      {
        _objectViews.Add(physicalObjectView);
      }

    }

    /// <summary>
    /// Нарисовать карту
    /// </summary>
    private void DrawMap()
    {
      if (GameModel.Map != null)
      {
        for (int i = 0; i < GameModel.MAP_WIDTH; i += COMPRESSION_RATIO)
        {
          for (int j = 0; j < GameModel.MAP_HEIGHT; j += COMPRESSION_RATIO)
          {
            int sum = 0;
            for (int l = i; l < i + COMPRESSION_RATIO; l++)
            {
              for (int k = j; k < j + COMPRESSION_RATIO; k++)
              {

                if (GameModel.Map[k, l] == 1)
                {
                  sum++;
                }
              }
            }
            if (sum >= (COMPRESSION_RATIO * COMPRESSION_RATIO) / 2)
            {
              SetPixel(i / COMPRESSION_RATIO, j / COMPRESSION_RATIO, (int)ConsoleColor.DarkYellow);
            }
            else
            {
              SetPixel(i / COMPRESSION_RATIO, j / COMPRESSION_RATIO, (int)ConsoleColor.Gray);
            }


          }
        }
      }
      
    }

    /// <summary>
    /// Запустить отрисовку представлений
    /// </summary>
    private void StartDrawing()
    {
      Thread drawThread = new Thread(Draw);
      drawThread.Start();
    }

    /// <summary>
    /// Рисовать представление
    /// </summary>
    public override void Draw()
    {
      while (_isNeedDraw)
      {
        lock (_objectViews)
        {
          DrawMap();
          foreach (ObjectView elObjectView in _objectViews)
          {
            elObjectView.Draw();
          }
        }

        FastConsoleWorker.Draw();

      }
    }

    /// <summary>
    /// Запустить представление
    /// </summary>
    public override void Start()
    {
      ViewInit();
      StartDrawing();
      base.Start();
    }

    /// <summary>
    /// Остановить представление
    /// </summary>
    public override void Stop()
    {
      GameModel.NewObjectAddedEvent -= AddNewView;
      GameModel.ObjectDeletedEvent -= DeletePhysicalObjectView;
      _isNeedDraw = false;
      base.Stop();
      Console.Clear();
    }
  }
}
