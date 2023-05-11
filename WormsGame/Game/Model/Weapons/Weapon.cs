namespace WormsGame.Base
{
  /// <summary>
  /// Оружие
  /// </summary>
  public abstract class Weapon
  {
    /// <summary>
    /// Шаг изменения угла оружия
    /// </summary>
    private const int ANGLE_STEP = 5;
    /// <summary>
    /// Максимальный угол оружия
    /// </summary>
    private const int MAX_ANGLE = 90;
    /// <summary>
    /// Максимальный заряд оружия
    /// </summary>
    public static int MAX_POWER = 18;
    /// <summary>
    /// Начальный заряд оружия
    /// </summary>
    public static int INITIAL_POWER = 1;
    /// <summary>
    /// Шаг заряда оружия
    /// </summary>
    public static double POWER_STEP = 0.3;

    /// <summary>
    /// Кол-во использований
    /// </summary>
    private int _usesNumber;
    /// <summary>
    /// Заряд
    /// </summary>
    private double _power;
    /// <summary>
    /// Угол
    /// </summary>
    private int _angle;
    /// <summary>
    /// Передаваемая скорость
    /// </summary>
    private int _transmittedSpeed;
    /// <summary>
    /// Урон
    /// </summary>
    private int _damage;
    /// <summary>
    /// Радуис взрыва
    /// </summary>
    private int _explosionRadius;
    /// <summary>
    /// Подвержен ли ветру
    /// </summary>
    private bool _isExposedToWind;
    /// <summary>
    /// Время после использования
    /// </summary>
    private int _timeAfterUse;

    /// <summary>
    /// Заряд
    /// </summary>
    public double Power { get { return _power; } set { _power = value; } }
    /// <summary>
    /// Угол
    /// </summary>
    public int Angle { get { return _angle; } set { _angle = value; } }
    /// <summary>
    /// Кол-во использований
    /// </summary>
    public int UsesNumber { get { return _usesNumber; } set { _usesNumber = value; } }
    /// <summary>
    /// Передаваемая скорость
    /// </summary>
    public int TransmittedSpeed { get { return _transmittedSpeed; } set { _transmittedSpeed = value; } }
    /// <summary>
    /// Урон
    /// </summary>
    public int Damage { get { return _damage; } set { _damage = value; } }
    /// <summary>
    /// Радиус взрыва
    /// </summary>
    public int ExplosionRadius { get { return _explosionRadius; } set { _explosionRadius = value; } }
    /// <summary>
    /// Подвержен ли снаряд взрыву
    /// </summary>
    public bool IsExposedToWind { get { return _isExposedToWind; } set { _isExposedToWind = value;} }
    /// <summary>
    /// Время после использования
    /// </summary>
    public int TimeAfterUse { get { return _timeAfterUse; } set { _timeAfterUse = value; } }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parUsesNumber">Кол-во использований</param>
    /// <param name="parTransmittedSpeed">Передаваемая скорость</param>
    /// <param name="parDamage">Урон</param>
    /// <param name="parExplosionRadius">Радуис взрыва</param>
    /// <param name="parIsExposedToWind">Подвержен ли снаряд взрыву</param>
    /// <param name="parTimeAfterUse">Время после использования</param>
    public Weapon(int parUsesNumber, int parTransmittedSpeed, int parDamage, int parExplosionRadius, bool parIsExposedToWind, int parTimeAfterUse)
    {
      UsesNumber = parUsesNumber;
      TransmittedSpeed = parTransmittedSpeed;
      Damage = parDamage;
      IsExposedToWind = parIsExposedToWind;
      ExplosionRadius = parExplosionRadius;
      Power = INITIAL_POWER;
      Angle = 0;
      TimeAfterUse = parTimeAfterUse;
    }

    /// <summary>
    /// Поднять угол оружия
    /// </summary>
    public void RiseAngle()
    {
      if (Angle + ANGLE_STEP <= MAX_ANGLE)
      {
        Angle += ANGLE_STEP;
      }
    }

    /// <summary>
    /// Опустить угол оружия
    /// </summary>
    public void DownAngle()
    {
      if (Angle - ANGLE_STEP >= -MAX_ANGLE)
      {
        Angle -= ANGLE_STEP;
      }
      
    }

    /// <summary>
    /// Добавить заряд оружия
    /// </summary>
    public void PowerUp()
    {
      if (Power + POWER_STEP <= MAX_POWER)
      {
        Power += POWER_STEP;
      }
    }

    /// <summary>
    /// Установить начальное состояние
    /// </summary>
    public void SetInitialState()
    {
      Power = INITIAL_POWER;
      Angle = 0;
    }

    /// <summary>
    /// Использовать
    /// </summary>
    /// <param name="parWorm"></param>
    public abstract void Use(Worm parWorm);

  }
}