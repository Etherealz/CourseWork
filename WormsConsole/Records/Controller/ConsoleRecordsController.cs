using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WormsConsole.Records.View;
using WormsGame.Records.Controller;
using WormsGame.Records.Model;
using WormsGame.Records.View;

namespace WormsConsole.Records.Controller
{
  /// <summary>
  /// Консольный контроллер рекордов
  /// </summary>
  public class ConsoleRecordsController : RecordsController
  {
    /// <summary>
    /// Работает ли считывание клавиш
    /// </summary>
    private bool _isWorking;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parModel">Модель</param>
    public ConsoleRecordsController(RecordsModel parModel) : base(parModel, new ConsoleRecordsView(parModel))
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
