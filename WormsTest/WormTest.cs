using Microsoft.VisualStudio.TestTools.UnitTesting;
using WormsGame.Base;

namespace WormsTest
{
  /// <summary>
  /// Класс тестов для тестирования класса Worm
  /// </summary>
  [TestClass]
  public class WormTest
  {
    /// <summary>
    /// Ширина карты для тестов
    /// </summary>
    public const int TEST_MAP_WIDTH = 100;
    /// <summary>
    /// Высота карты для тестов
    /// </summary>
    public const int TEST_MAP_HEIGHT = 100;
    /// <summary>
    /// Координата Y земли
    /// </summary>
    public const int GROUND_Y = 50;
    /// <summary>
    /// Высота низкого препятствия
    /// </summary>
    public const int SMALL_OBSTACLE_HEIGHT = WORM_HEIGHT / 3;
    /// <summary>
    /// Высота высокого препятствия
    /// </summary>
    public const int BIG_OBSTACLE_HEIGHT = WORM_HEIGHT;

    /// <summary>
    /// Высота червяка
    /// </summary>
    public const int WORM_HEIGHT = 18;
    /// <summary>
    /// Ширина червяка
    /// </summary>
    public const int WORM_WIDTH = 12;

    /// <summary>
    /// Инициализировать карту для тестов с землей
    /// </summary>
    private void InitializeDefaultTestMap()
    {
      int[,] map = new int[TEST_MAP_HEIGHT, TEST_MAP_WIDTH];
      for (int i = 0;  i < TEST_MAP_WIDTH; i++)
      {
        for (int j = 0; j < TEST_MAP_HEIGHT; j++)
        {
          map[j, i] = 0;
        }
      }

      for (int x = 0; x < TEST_MAP_WIDTH; x++)
      {
        map[GROUND_Y, x] = 1;
      }

      GameModel.Map = map;
    }

    /// <summary>
    /// Установить карту с низким препятствием для движения червяка влево
    /// </summary>
    /// <param name="parStartX">Координата червяка</param>
    private void SetupMapForWormMoveRightInSmallObstacleTest(int parStartX)
    {
      InitializeDefaultTestMap();
      for (int x = 0; x < TEST_MAP_WIDTH; x++)
      {
        GameModel.Map[GROUND_Y, x] = 1;
      }

      for (int i = 0; i <= SMALL_OBSTACLE_HEIGHT; i++)
      {
        GameModel.Map[GROUND_Y - i, parStartX + WORM_WIDTH] = 1;
      }

    }

    /// <summary>
    /// Установить карту с низким препятствием для движения червяка вправо
    /// </summary>
    /// <param name="parStartX">Координата червяка</param>
    private void SetupMapForWormMoveLeftInSmallObstacleTest(int parStartX)
    {
      InitializeDefaultTestMap();
      for (int x = 0; x < TEST_MAP_WIDTH; x++)
      {
        GameModel.Map[GROUND_Y, x] = 1;
      }

      for (int i = 0; i <= SMALL_OBSTACLE_HEIGHT; i++)
      {
        GameModel.Map[GROUND_Y - i, parStartX - 1] = 1;
      }

    }

    /// <summary>
    /// Установить карту с высоким препятствием для движения червяка влево
    /// </summary>
    /// <param name="parStartX">Координата червяка</param>
    private void SetupMapForWormMoveLeftInBigObstacleTest(int parStartX)
    {
      InitializeDefaultTestMap();
      for (int x = 0; x < TEST_MAP_WIDTH; x++)
      {
        GameModel.Map[GROUND_Y, x] = 1;
      }

      for (int i = 0; i <= BIG_OBSTACLE_HEIGHT; i++)
      {
        GameModel.Map[GROUND_Y - i, parStartX - 1] = 1;
      }

    }

    /// <summary>
    /// Установить карту с высоким препятствием для движения червяка вправо
    /// </summary>
    /// <param name="parStartX"></param>
    private void SetupMapForWormMoveRightInBigObstacleTest(int parStartX)
    {
      InitializeDefaultTestMap();
      for (int x = 0; x < TEST_MAP_WIDTH; x++)
      {
        GameModel.Map[GROUND_Y, x] = 1;
      }

      for (int i = 0; i <= BIG_OBSTACLE_HEIGHT; i++)
      {
        GameModel.Map[GROUND_Y - i, parStartX + WORM_WIDTH] = 1;
      }

    }

    /// <summary>
    /// Тестирование успешного движения червяка влево
    /// </summary>
    [TestMethod]
    public void TestSuccessWormMoveLeft()
    {
      InitializeDefaultTestMap();
      int startX = 50;
      int expectedX = 49;
      int startY = GROUND_Y - WORM_HEIGHT;

      Worm worm = new Worm(null, startX, startY, WORM_WIDTH, WORM_HEIGHT, 100);
      worm.MoveLeft();
      Assert.AreEqual(expectedX, worm.X);
      Assert.AreEqual(startY, worm.Y);
    }

    /// <summary>
    /// Тестирование успешного движения червяка вправо
    /// </summary>
    [TestMethod]
    public void TestSuccessWormMoveRight()
    {
      InitializeDefaultTestMap();
      int startX = 50;
      int expectedX = 51;
      int startY = GROUND_Y - WORM_HEIGHT;

      Worm worm = new Worm(null, startX, startY, WORM_WIDTH, WORM_HEIGHT, 100);
      worm.MoveRight();
      Assert.AreEqual(expectedX, worm.X);
      Assert.AreEqual(startY, worm.Y);
    }

    /// <summary>
    /// Тестирование успешного движения червяка вправо через низкое препятсвие
    /// </summary>
    [TestMethod]
    public void TestWormMoveRightInSmallObstacle()
    {
      int startX = 50;
      int expectedX = 51;
      int startY = GROUND_Y - WORM_HEIGHT;
      int expectedY = startY - SMALL_OBSTACLE_HEIGHT;

      SetupMapForWormMoveRightInSmallObstacleTest(startX);

      Worm worm = new Worm(null, startX, startY, WORM_WIDTH, WORM_HEIGHT, 100);
      worm.MoveRight();  

      Assert.AreEqual(expectedX, worm.X);
      Assert.AreEqual(expectedY, worm.Y);
    }

    /// <summary>
    /// Тестирование успешного движения червяка влево через низкое препятсвие
    /// </summary>
    [TestMethod]
    public void TestWormMoveLeftInSmallObstacle()
    {
      int startX = 50;
      int expectedX = 49;
      int startY = GROUND_Y - WORM_HEIGHT;
      int expectedY = startY - SMALL_OBSTACLE_HEIGHT;

      SetupMapForWormMoveLeftInSmallObstacleTest(startX);

      Worm worm = new Worm(null, startX, startY, WORM_WIDTH, WORM_HEIGHT, 100);
      worm.MoveLeft();

      Assert.AreEqual(expectedX, worm.X);
      Assert.AreEqual(expectedY, worm.Y);
    }

    /// <summary>
    /// Тестирование движения червяка вправо через высокое препятсвие
    /// </summary>
    [TestMethod]
    public void TestWormMoveRightInBigObstacle()
    {
      int startX = 50;
      int startY = GROUND_Y - WORM_HEIGHT;

      SetupMapForWormMoveRightInBigObstacleTest(startX);

      Worm worm = new Worm(null, startX, startY, WORM_WIDTH, WORM_HEIGHT, 100);
      worm.MoveRight();

      Assert.AreEqual(startX, worm.X);
      Assert.AreEqual(startY, worm.Y);
    }

    /// <summary>
    /// Тестирование движения червяка влево через низкое препятсвие
    /// </summary>
    [TestMethod]
    public void TestWormMoveLeftInBigObstacle()
    {
      int startX = 50;
      int startY = GROUND_Y - WORM_HEIGHT;

      SetupMapForWormMoveLeftInBigObstacleTest(startX);

      Worm worm = new Worm(null, startX, startY, WORM_WIDTH, WORM_HEIGHT, 100);
      worm.MoveLeft();

      Assert.AreEqual(startX, worm.X);
      Assert.AreEqual(startY, worm.Y);
    }

    /// <summary>
    /// Тестирование червяка на земле 
    /// </summary>
    [TestMethod]
    public void TestWormSuccessIsOnGround()
    {
      int startX = 50;
      int startY = GROUND_Y - WORM_HEIGHT;

      InitializeDefaultTestMap();

      Worm worm = new Worm(null, startX, startY, WORM_WIDTH, WORM_HEIGHT, 100);

      Assert.IsTrue(worm.IsOnGround());
    }

    /// <summary>
    /// Тестирование червяка, находящегося не на земле
    /// </summary>
    [TestMethod]
    public void TestWormFailIsOnGround()
    {
      int startX = 50;
      int startY = GROUND_Y - WORM_HEIGHT - 1;

      InitializeDefaultTestMap();

      Worm worm = new Worm(null, startX, startY, WORM_WIDTH, WORM_HEIGHT, 100);

      Assert.IsFalse(worm.IsOnGround());
    }

    /// <summary>
    /// Тестирование прыжка червяка вперед, когда он повернут влево
    /// </summary>
    [TestMethod]
    public void TestWormSuccessLeftForwardJump()
    {
      int startX = 50;
      int startY = GROUND_Y - WORM_HEIGHT;

      InitializeDefaultTestMap();

      Worm worm = new Worm(null, startX, startY, WORM_WIDTH, WORM_HEIGHT, 100);
      worm.State = State.Left;
      worm.JumpForward();
      

      Assert.AreEqual(Worm.FORWARD_JUMP_Y_SPEED, worm.YSpeed);
      Assert.AreEqual(-Worm.FORWARD_JUMP_X_SPEED, worm.XSpeed);
    }

    /// <summary>
    /// Тестирование прыжка червяка вперед, когда он повернут вправо
    /// </summary>
    [TestMethod]
    public void TestWormSuccessRightForwardJump()
    {
      int startX = 50;
      int startY = GROUND_Y - WORM_HEIGHT;

      InitializeDefaultTestMap();

      Worm worm = new Worm(null, startX, startY, WORM_WIDTH, WORM_HEIGHT, 100);
      worm.State = State.Right;
      worm.JumpForward();

      Assert.AreEqual(Worm.FORWARD_JUMP_Y_SPEED, worm.YSpeed);
      Assert.AreEqual(Worm.FORWARD_JUMP_X_SPEED, worm.XSpeed);
    }

    /// <summary>
    /// Тестирование прыжка червяка вперед, когда он не на земле
    /// </summary>
    [TestMethod]
    public void TestWormFailForwardJump()
    {
      int startX = 50;
      int startY = GROUND_Y - WORM_HEIGHT - 1;

      InitializeDefaultTestMap();

      Worm worm = new Worm(null, startX, startY, WORM_WIDTH, WORM_HEIGHT, 100);
      worm.JumpForward();

      Assert.AreEqual(0, worm.YSpeed);
      Assert.AreEqual(0, worm.XSpeed);
    }

    /// <summary>
    /// Тестирование прыжка червяка назад, когда он повернут влево
    /// </summary>
    [TestMethod]
    public void TestWormSuccessLeftBackFlip()
    {
      int startX = 50;
      int startY = GROUND_Y - WORM_HEIGHT;

      InitializeDefaultTestMap();

      Worm worm = new Worm(null, startX, startY, WORM_WIDTH, WORM_HEIGHT, 100);
      worm.State = State.Left;
      worm.BackFlip();

      Assert.AreEqual(Worm.BACKFLIP_Y_SPEED, worm.YSpeed);
      Assert.AreEqual(Worm.BACKFLIP_X_SPEED, worm.XSpeed);
    }

    /// <summary>
    /// Тестирование прыжка червяка назад, когда он повернут вправо
    /// </summary>
    [TestMethod]
    public void TestWormSuccessRightBackFlip()
    {
      int startX = 50;
      int startY = GROUND_Y - WORM_HEIGHT;

      InitializeDefaultTestMap();

      Worm worm = new Worm(null, startX, startY, WORM_WIDTH, WORM_HEIGHT, 100);
      worm.State = State.Right;
      worm.BackFlip();

      Assert.AreEqual(Worm.BACKFLIP_Y_SPEED, worm.YSpeed);
      Assert.AreEqual(-Worm.BACKFLIP_X_SPEED, worm.XSpeed);
    }

    /// <summary>
    /// Тестирование прыжка червяка назад, когда он находится не на земле
    /// </summary>
    [TestMethod]
    public void TestWormFailBackFlip()
    {
      int startX = 50;
      int startY = GROUND_Y - WORM_HEIGHT - 1;

      InitializeDefaultTestMap();

      Worm worm = new Worm(null, startX, startY, WORM_WIDTH, WORM_HEIGHT, 100);
      worm.BackFlip();

      Assert.AreEqual(0, worm.YSpeed);
      Assert.AreEqual(0, worm.XSpeed);
    }

    /// <summary>
    /// Тестирование червяка, столкнувшегося с землей
    /// </summary>
    [TestMethod]
    public void TestWormSuccessIsCollapse()
    {
      int startX = 50;
      int startY = GROUND_Y - WORM_HEIGHT + 2;

      InitializeDefaultTestMap();

      Worm worm = new Worm(null, startX, startY, WORM_WIDTH, WORM_HEIGHT, 100);

      Assert.IsTrue(worm.IsCollapse(worm.X, worm.Y));
    }

    /// <summary>
    /// Тестирование червяка, не столкнувшегося с землей
    /// </summary>
    [TestMethod]
    public void TestWormFailIsCollapse()
    {
      int startX = 50;
      int startY = GROUND_Y - WORM_HEIGHT;

      InitializeDefaultTestMap();

      Worm worm = new Worm(null, startX, startY, WORM_WIDTH, WORM_HEIGHT, 100);

      Assert.IsFalse(worm.IsCollapse(worm.X, worm.Y));
    }

    /// <summary>
    /// Тестирование увеличения скорости червяка по Y, когда он в воздухе
    /// </summary>
    [TestMethod]
    public void TestYSpeedRaisedInAir()
    {
      int startX = 50;
      int startY = GROUND_Y - WORM_HEIGHT - 2;

      InitializeDefaultTestMap();

      Worm worm = new Worm(null, startX, startY, WORM_WIDTH, WORM_HEIGHT, 100);
      worm.Update();

      Assert.AreEqual(1, worm.YSpeed);
    }

    /// <summary>
    /// Тестирование отсутствия увеличения скорости червяка по Y, когда он на земле
    /// </summary>
    [TestMethod]
    public void TestYSpeedNotRaisedOnGround()
    {
      int startX = 50;
      int startY = GROUND_Y - WORM_HEIGHT;

      InitializeDefaultTestMap();

      Worm worm = new Worm(null, startX, startY, WORM_WIDTH, WORM_HEIGHT, 100);
      worm.Update();

      Assert.AreEqual(0, worm.YSpeed);
    }

    /// <summary>
    /// Тестирование остановки червяка на земле, когда его скорость превышает расстояние до земли
    /// </summary>
    [TestMethod]
    public void TestWormStoppedOnGroundOnBigYSpeed()
    {
      int startX = 50;
      int startY = GROUND_Y - WORM_HEIGHT - 10;
      int expectedY = GROUND_Y - WORM_HEIGHT;
      int YSpeed = 20;

      InitializeDefaultTestMap();

      Worm worm = new Worm(null, startX, startY, WORM_WIDTH, WORM_HEIGHT, 100);
      worm.YSpeed = YSpeed;
      worm.Update();

      Assert.AreEqual(expectedY, worm.Y);
    }

    /// <summary>
    /// Тестирование перемещения червяка посредством скорости
    /// </summary>
    [TestMethod]
    public void TestWormSuccessfullyMovedBySpeed()
    {
      int startX = 50;
      int startY = GROUND_Y - WORM_HEIGHT;
      int YSpeed = -1;
      int XSpeed = 1;
      int expectedX = startX + XSpeed;
      int expectedY = startY + YSpeed;

      InitializeDefaultTestMap();

      Worm worm = new Worm(null, startX, startY, WORM_WIDTH, WORM_HEIGHT, 100);
      worm.YSpeed = YSpeed;
      worm.XSpeed = XSpeed;
      worm.Update();

      Assert.AreEqual(expectedX, worm.X);
      Assert.AreEqual(expectedY, worm.Y);

    }


  }
}