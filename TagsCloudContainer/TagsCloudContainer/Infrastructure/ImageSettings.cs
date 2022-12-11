using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Infrastructure
{
    public class ImageSettings
    {
        [DisplayName("Ширина")]
        [Category("1. Размеры")]
        public int Width { get; set; } = 500;
        [DisplayName("Высота")]
        [Category("1. Размеры")]
        public int Height { get; set; } = 500;


        
        [DisplayName("Задний фон прямоугольников")]
        [Category("2. Цвета")]
        public Color RectangleBackgroundColor { get; set; } = Color.Red;
        [DisplayName("Граница прямоугольников")]
        [Category("2. Цвета")]
        public Color RectangleBordersColor { get; set; } = Color.Yellow;
        [DisplayName("Задний фон картинки")]
        [Category("2. Цвета")]
        public Color BackgroundColor { get; set; } = Color.DarkBlue;

        [DisplayName("Цвет шрифта")]
        [Category("3. Шрифт")]
        public Color TextColor { get; set; } = Color.Black;

        [DisplayName("Шрифт")]
        [Category("3. Шрифт")]
        public Font Font { get; set; } = new Font("Arial", 20, FontStyle.Bold, GraphicsUnit.Point);



        public void ThrowExcIfNonPositiveArgs()
        {
            if (Width <= 0)
                throw new ArgumentException($"Параметры только положительны! Width={Width}");
            if (Height <= 0)
                throw new ArgumentException($"Параметры только положительны! Height={Height}");
        }
    }
}
