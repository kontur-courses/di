using System.Drawing;

namespace TagCloud.App.CloudCreatorDriver.RectanglesLayouters.SpiralCloudLayouters
{
    /// <summary>
    /// Класс настроек для раскладчика прямоугольников, работающего по принципу спиральной раскладки
    /// </summary>
    public class SpiralCloudLayouterSettings : ICloudLayouterSettings
    {
        public Point Center { get; set; }
        public double SpiralStep { get; set; }
        public double RotationStep { get; set; }
    }
}