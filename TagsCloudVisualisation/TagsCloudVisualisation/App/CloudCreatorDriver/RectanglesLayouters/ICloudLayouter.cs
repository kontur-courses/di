using System.Drawing;
using TagsCloudVisualisation.App.RectanglesLayouters.Exceptions;

namespace TagsCloudVisualisation.App.RectanglesLayouters
{
    /// <summary>
    /// Интерфейс раскладчиков прямоугольников по кругу
    /// </summary>
    public interface ICloudLayouter
    {
        /// <summary>
        /// Метод, позволяющий разместить прмяоугольник, основываясь о его размере
        /// </summary>
        /// <param name="rectangleSize">Размеры прямоугольника, который нужно разместить</param>
        /// <exception cref="CloudSettingsException">Если не были установлены настроки</exception>
        /// <returns>Положение верхнего левого угла на изображении</returns>
        Rectangle PutNextRectangle(Size rectangleSize);
        
        /// <summary>
        /// Метод, позволяющий установить настройки для раскладчика
        /// </summary>
        /// <param name="settings">Настройки, уникальные для каждого раскладчика</param>
        /// <exception cref="CloudSettingsException">Если переданные настройки не соответствуют требованиям
        /// настроек для раскладчика</exception>
        void SetSettings(ICloudLayouterSettings settings);
    }
}