using System.Drawing;

namespace TagCloud.App.CloudCreatorDriver.DrawingSettings
{
    /// <summary>
    /// Интерфейс визуализации слова в зависимости от его встреяаемости
    /// </summary>
    public interface IWordVisualisation
    {
        /// <returns>Цвет, в который необходимо окрасить слово</returns>
        Color GetColor();
        
        /// <returns>Значение tf, с которого начитнается данное оформление</returns>
        double GetStartingValue();
        
        /// <returns>Размер шрифта, которыйм необходимо писать это слово</returns>
        Font GetFont();
    }
}