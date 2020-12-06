using System.ComponentModel;
using System.Drawing;

namespace TagsCloud.Infrastructure
{
    public class ImageSettings
    {
        public ImageSettings(int width, int height)
        {
            Width = width;
            Height = height;
        }

        [Category("Цвета")]
        [DisplayName("Палитра")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Palette Palette { get; set; } = new Palette();

        [DisplayName("Шрифт")]
        public Font Font { get; set; } = new Font(FontFamily.GenericSerif, 10);

        [DisplayName("Ширина")]
        public int Width { get; set; }

        [DisplayName("Высота")]
        public int Height { get; set; }
    }
}