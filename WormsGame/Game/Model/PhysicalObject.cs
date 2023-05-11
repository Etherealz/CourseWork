using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WormsGame.Base
{
  /// <summary>
  /// Физический объект
  /// </summary>
  public abstract class PhysicalObject
  {
    /// <summary>
    /// Координата X
    /// </summary>
    private int _x;
    /// <summary>
    /// Координата Y
    /// </summary>
    private int _y;
    /// <summary>
    /// Скорость по X
    /// </summary>
    private int _xSpeed;
    /// <summary>
    /// Скорость по Y
    /// </summary>
    private int _ySpeed;
    /// <summary>
    /// Ширина
    /// </summary>
    private int _width;
    /// <summary>
    /// Высота
    /// </summary>
    private int _height;
    /// <summary>
    /// Подвержен ли ветру
    /// </summary>
    private bool _isExposedToWind;

    /// <summary>
    /// Координата X
    /// </summary>
    public int X { get { return _x; } set { _x = value; } }
    /// <summary>
    /// Координата Y
    /// </summary>
    public int Y { get { return _y; } set { _y = value; } }
    /// <summary>
    /// Скорость по X
    /// </summary>
    public int XSpeed { get { return _xSpeed; } set { _xSpeed = value; } }
    /// <summary>
    /// Скорость по Y
    /// </summary>
    public int YSpeed { get { return _ySpeed; } set { _ySpeed = value; } }
    /// <summary>
    /// Ширина
    /// </summary>
    public int Width { get { return _width; } set { _width = value; } }
    /// <summary>
    /// Высота
    /// </summary>
    public int Height { get { return _height; } set { _height = value; } }
    /// <summary>
    /// Повержен ли ветру
    /// </summary>
    public bool IsExposedToWind { get { return _isExposedToWind; } set { _isExposedToWind = value;} }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parX">Y</param>
    /// <param name="parY">Y</param>
    /// <param name="parXSpeed">Скорость по X</param>
    /// <param name="parYSpeed">Скорость по Y</param>
    /// <param name="parWidth">Ширина</param>
    /// <param name="parHeight">Высота</param>
    /// <param name="parIsExposedToWind">Подвержен ли ветру</param>
    public PhysicalObject(int parX, int parY, int parXSpeed, int parYSpeed, int parWidth, int parHeight, bool parIsExposedToWind)
    {
      X = parX;
      Y = parY;
      XSpeed = parXSpeed;
      YSpeed = parYSpeed;
      Width = parWidth;
      Height = parHeight;
      IsExposedToWind = parIsExposedToWind;
    }

    /// <summary>
    /// Получить координату X центра объекта
    /// </summary>
    /// <returns>X</returns>
    public int GetCenterX()
    {
      return X + Width / 2;
    }

    /// <summary>
    /// Получить координату Y центра объекта
    /// </summary>
    /// <returns>Y</returns>
    public int GetCenterY()
    {
      return Y + Height / 2;
    }

    /// <summary>
    /// Находится ли на поверхности
    /// </summary>
    /// <returns>true, если находится на поверхности, false - иначе</returns>
    public bool IsOnGround()
    {
      bool isOnGround = false;
      for (int i = X; i <= X + Width; i++)
      {
        if (i < GameModel.MAP_WIDTH && Y >= 0 && i > 0 && Y + Height < GameModel.MAP_HEIGHT)
          if (GameModel.Map[Y + Height, i] == 1)
          {
            isOnGround = true;
            break;
          }
      }

      return isOnGround;
    }

    /// <summary>
    /// Столкнулся ли с картой
    /// </summary>
    /// <param name="parX">X</param>
    /// <param name="parY">Y</param>
    /// <returns>true, если столкнулся, false - иначе</returns>
    public bool IsCollapse(int parX, int parY)
    {
      bool isCollapse = false;
      for (int i = parX; i <= parX + Width - 1; i++)
      {
        for (int j = parY; j <= parY + Height - 1; j++)
        {
          if (i < 1920 && j > 0 && j < 1080 && i > 0)
          {
            if (GameModel.Map[j, i] == 1)
            {
              isCollapse = true;
              break;
            }
          }

        }
      }
      return isCollapse;
    }

    /// <summary>
    /// Обновить состояние объекта
    /// </summary>
    public void Update()
    {
      lock (this)
      {
        int max = Math.Max(Math.Abs(XSpeed), Math.Abs(YSpeed));
        float minX = (float)XSpeed / max;
        float minY = (float)YSpeed / max;
        int k = 1;
        int newX = 0;
        int newY = 0;

        while ((Math.Abs(minX * k) <= Math.Abs(XSpeed) && XSpeed != 0) || (Math.Abs(minY * k) <= Math.Abs(YSpeed) && YSpeed != 0))
        {
          newX = X + (int)(minX * k);
          newY = Y + (int)(minY * k);
          if (IsCollapse(newX, newY))
          {
            OnCollapse();
            break;
          }
          X = newX;
          Y = newY;
          k++;
        }

        if ((X + XSpeed >= GameModel.Map.GetLength(1)) || (X + XSpeed + Width < 0) || (Y + YSpeed >= GameModel.Map.GetLength(0)))
        {
          OnGoingOutOfBounds();
        }


        if (!IsOnGround())
        {
          YSpeed++;
        }

        if (IsExposedToWind)
        {
          XSpeed += GameModel.WindStrength;
        }
      }

    }

    /// <summary>
    /// Обработчмк столкновения с картой
    /// </summary>
    public abstract void OnCollapse();

    /// <summary>
    /// Обработчик выхода за границы карты
    /// </summary>
    public abstract void OnGoingOutOfBounds();

  }
}
