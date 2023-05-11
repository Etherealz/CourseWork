using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WormsGame.Base;
using WormsGame.Records;
using WormsGame.Records.Model;

namespace WormsGame.GameEndedScreen.Model
{
  /// <summary>
  /// Модель экрана конца игры
  /// </summary>
  public class GameEndedScreenModel : BaseMVC.Model
  {

    /// <summary>
    /// Имя рекордсмена
    /// </summary>
    private string _name;
    /// <summary>
    /// Игрок
    /// </summary>
    private Player _player;
    /// <summary>
    /// Очки
    /// </summary>
    private int _score;

    /// <summary>
    /// Событие о необходимости перерисовки
    /// </summary>
    public event dNeedRedraw? NeedRedrawEvent;

    /// <summary>
    /// Имя рекордсмена
    /// </summary>
    public string Name
    {
      get
      {
        return _name;
      }
      set
      {
        if (value.Length <= 15)
        {
          _name = value;
          NeedRedrawEvent?.Invoke();
        }

      }
    }

    /// <summary>
    /// Игрок
    /// </summary>
    public Player Player { get { return _player; } set { _player = value; } }
    /// <summary>
    /// Очки
    /// </summary>
    public int Score { get { return _score; } set { _score = value; } }

    /// <summary>
    /// Рекорд ли
    /// </summary>
    public bool IsRecord { get; set; }

    /// <summary>
    /// Установить свойство рекорд ли 
    /// </summary>
    public void SetIsRecord()
    {
      IsRecord = RecordsRepository.Instance.IsRecord(Score);
      if (IsRecord)
      {
        Name = "Player_Name";
      }
    }

    /// <summary>
    /// Получить текст конца игры
    /// </summary>
    /// <returns>Текст конца игры</returns>
    public string GetEndText()
    {
      if (Player == null)
      {
        return "К сожалению, никто не победил. ";
      }
      else
      {
        string text = "";
        switch (Player.TeamColor)
        {
          case PlayersColor.Orange:
            text = "Победила команда красных!";
            break;
          case PlayersColor.Yellow:
            text = "Победила команда красных!";
            break;
          case PlayersColor.Blue:
            text = "Победила команда синих!";
            break;
          case PlayersColor.Green:
            text = "Победила команда зеленых!";
            break;
        }

        if (IsRecord)
        {
          text += $" Ваш результат попадает в таблицу рекордов! Вы сумели победить всего лишь за {Score} сек.! Введите имя победителя, оно будет отображаться в таблице рекордов. ";
        }
        else
        {
          text +=  $" Вы победили за {Score} сек. К сожалению, ваш результат не попадает в таблицу рекордов. ";
        }

        return text;

      }
    }

    /// <summary>
    /// Сохранить рекорд
    /// </summary>
    /// <returns>true, если рекорд сохранен, false - иначе</returns>
    public bool SaveRecord()
    {
      if (IsRecord && Name.Length > 0)
      {
        GameRecord gameRecord = new GameRecord(Name, Score);
        RecordsRepository.Instance.SaveRecord(gameRecord);
        return true;
      }
      return false;
    }

    /// <summary>
    /// Делегат о необходимости перерисовки
    /// </summary>
    public delegate void dNeedRedraw();

  }
}
