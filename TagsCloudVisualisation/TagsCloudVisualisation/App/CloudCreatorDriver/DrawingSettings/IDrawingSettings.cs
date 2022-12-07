using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualisation.App.DrawingSettings;

namespace TagsCloudVisualisation.App.CloudCreatorDriver.DrawingSettings
{
    /// <summary>
    /// Интерфейс настроек для рисования облака слов
    /// </summary>
    public interface IDrawingSettings
    {
        /// <summary>
        /// Цвет фона
        /// </summary>
        public Color BgColor { get; set; }
        
        /// <summary>
        /// Список оформления слов в зависимости от их встречаемости
        /// </summary>
        public List<IWordVisualisation> Visualisation { get; }
        
        /// <summary>
        /// Размер конечного изображения
        /// </summary>
        public Size PictureSize { get; set; }
    }
}