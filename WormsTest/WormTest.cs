using Microsoft.VisualStudio.TestTools.UnitTesting;
using WormsGame.Base;

namespace WormsTest
{
  /// <summary>
  /// ����� ������ ��� ������������ ������ Worm
  /// </summary>
  [TestClass]
  public class WormTest
  {
    /// <summary>
    /// ������ ����� ��� ������
    /// </summary>
    public const int TEST_MAP_WIDTH = 100;
    /// <summary>
    /// ������ ����� ��� ������
    /// </summary>
    public const int TEST_MAP_HEIGHT = 100;
    /// <summary>
    /// ���������� Y �����
    /// </summary>
    public const int GROUND_Y = 50;
    /// <summary>
    /// ������ ������� �����������
    /// </summary>
    public const int SMALL_OBSTACLE_HEIGHT = WORM_HEIGHT / 3;
    /// <summary>
    /// ������ �������� �����������
    /// </summary>
    public const int BIG_OBSTACLE_HEIGHT = WORM_HEIGHT;

    /// <summary>
    /// ������ �������
    /// </summary>
    public const int WORM_HEIGHT = 18;
    /// <summary>
    /// ������ �������
    /// </summary>
    public const int WORM_WIDTH = 12;

    /// <summary>
    /// ���������������� ����� ��� ������ � ������
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
    /// ���������� ����� � ������ ������������ ��� �������� ������� �����
    /// </summary>
    /// <param name="parStartX">���������� �������</param>
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
    /// ���������� ����� � ������ ������������ ��� �������� ������� ������
    /// </summary>
    /// <param name="parStartX">���������� �������</param>
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
    /// ���������� ����� � ������� ������������ ��� �������� ������� �����
    /// </summary>
    /// <param name="parStartX">���������� �������</param>
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
    /// ���������� ����� � ������� ������������ ��� �������� ������� ������
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
    /// ������������ ��������� �������� ������� �����
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
    /// ������������ ��������� �������� ������� ������
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
    /// ������������ ��������� �������� ������� ������ ����� ������ ����������
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
    /// ������������ ��������� �������� ������� ����� ����� ������ ����������
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
    /// ������������ �������� ������� ������ ����� ������� ����������
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
    /// ������������ �������� ������� ����� ����� ������ ����������
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
    /// ������������ ������� �� ����� 
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
    /// ������������ �������, ������������ �� �� �����
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
    /// ������������ ������ ������� ������, ����� �� �������� �����
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
    /// ������������ ������ ������� ������, ����� �� �������� ������
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
    /// ������������ ������ ������� ������, ����� �� �� �� �����
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
    /// ������������ ������ ������� �����, ����� �� �������� �����
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
    /// ������������ ������ ������� �����, ����� �� �������� ������
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
    /// ������������ ������ ������� �����, ����� �� ��������� �� �� �����
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
    /// ������������ �������, �������������� � ������
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
    /// ������������ �������, �� �������������� � ������
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
    /// ������������ ���������� �������� ������� �� Y, ����� �� � �������
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
    /// ������������ ���������� ���������� �������� ������� �� Y, ����� �� �� �����
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
    /// ������������ ��������� ������� �� �����, ����� ��� �������� ��������� ���������� �� �����
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
    /// ������������ ����������� ������� ����������� ��������
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