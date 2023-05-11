using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WormsGame.Records.Model;
using WormsGame.Records.View;

namespace WormsWPF.Records.View
{
  /// <summary>
  /// WPF представление рекордов
  /// </summary>
  public class WPFRecordsView : RecordsView
  {
    /// <summary>
    /// Окно
    /// </summary>
    private readonly Window _window;
    /// <summary>
    /// Канвас
    /// </summary>
    private Canvas _canvas;

    /// <summary>
    /// Контруктор
    /// </summary>
    /// <param name="parModel">Модель</param>
    /// <param name="parWindow">Окно</param>
    public WPFRecordsView(RecordsModel parModel, Window parWindow) : base(parModel)
    {
      _window = parWindow;
    }

    /// <summary>
    /// Настроить канвас
    /// </summary>
    private void SetupCanvas()
    {
      _canvas = new Canvas();
      _canvas.Width = _window.Width;
      _canvas.Height = _window.Height;
      _canvas.Background = new SolidColorBrush(Colors.Beige);

    }

    /// <summary>
    /// Нарисовать представление
    /// </summary>
    public override void Draw()
    {
      TextBlock header = new TextBlock();
      header.Text = "Рекорды. Нажмите Esc для возврата в меню";
      header.FontSize = 40;
      header.Foreground = new SolidColorBrush(Colors.DarkRed);
      Canvas.SetTop(header, 30);
      Canvas.SetLeft(header, 30);

      _canvas.Children.Add(header);

      List<GameRecord> records = Model.GetRecords();
      for (int i = 0; i < records.Count; i++)
      {
        GameRecord record = records[i];
        TextBlock recordText = new TextBlock();
        recordText.Text = $"{i + 1}. {record.PlayerName} - {record.Score} сек.";
        recordText.FontSize = 40;
        Canvas.SetTop(recordText, 100 + 80 * i);
        Canvas.SetLeft(recordText, 700);

        _canvas.Children.Add(recordText);
      }
    }

    /// <summary>
    /// Запустить
    /// </summary>
    public override void Start()
    {
      base.Start();
      SetupCanvas();
      Draw();
      _window.Content = _canvas;
    }

    /// <summary>
    /// Остановить
    /// </summary>
    public override void Stop()
    {
      _window.Content = null;
      base.Stop();
    }
  }
}
