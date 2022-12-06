using System;
using System.Collections.Generic;
using System.ComponentModel;
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


        [DisplayName("Цвет текста внутри прямоугольников")]
        [Category("2. Цвета")]
        public Color TextColor { get; set; } = Color.Black;
        [DisplayName("Задний фон прямоугольников")]
        [Category("2. Цвета")]
        public Color RectangleBackgroundColor { get; set; } = Color.Red;
        [DisplayName("Граница прямоугольников")]
        [Category("2. Цвета")]
        public Color RectangleBordersColor { get; set; } = Color.Yellow;
        [DisplayName("Задний фон картинки")]
        [Category("2. Цвета")]
        public Color BackgroundColor { get; set; } = Color.DarkBlue;
    }
}
