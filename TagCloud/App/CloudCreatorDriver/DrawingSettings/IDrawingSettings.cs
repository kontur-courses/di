using System.Drawing;

namespace TagCloud.App.CloudCreatorDriver.DrawingSettings;

/// <summary>
/// Интерфейс настроек для рисования облака слов
/// </summary>
public interface IDrawingSettings
{
    /// <summary>
    /// Цвет фона
    /// </summary>
    Color BgColor { get; set; }
        
    /// <summary>
    /// Список оформления слов в зависимости от их встречаемости
    /// </summary>
    List<IWordVisualisation> Visualisations { get; }
        
    /// <summary>
    /// Размер конечного изображения
    /// </summary>
    Size PictureSize { get; set; }

    void AddVisualisation(IWordVisualisation wordVisualisation);
}