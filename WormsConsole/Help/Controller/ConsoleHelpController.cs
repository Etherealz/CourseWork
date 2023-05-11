
using WormsConsole.Help.View;
using WormsGame.Help.Controller;
using WormsGame.Help.Model;

namespace WormsConsole.Help.Controller
{
  /// <summary>
  /// Консольный контроллер справки
  /// </summary>
  public class ConsoleHelpController : HelpController
  {
    /// <summary>
    /// Работает ли считывание клавиш
    /// </summary>
    private bool _isWorking;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parModel">Модель</param>
    public ConsoleHelpController(HelpModel parModel) : base(parModel, new ConsoleHelpView(parModel))
    {
    }

    /// <summary>
    /// Запустить считывание нажатых клавиш в консоли
    /// </summary>
    public void ReadKeysStart()
    {
      _isWorking = true;
      do
      {
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);

        switch (keyInfo.Key)
        {
          case ConsoleKey.Escape:
            BackToMenu();
            break;

        }


      } while (_isWorking);
    }

    /// <summary>
    /// Остановить считывание клавиш
    /// </summary>
    public void ReadKeysStop()
    {
      _isWorking = false;
    }

    /// <summary>
    /// Запустить
    /// </summary>
    public override void Start()
    {
      base.Start();
      ReadKeysStart();
    }

    /// <summary>
    /// Остановить
    /// </summary>
    public override void Stop()
    {
      ReadKeysStop();
      base.Stop();
    }
  }
}
