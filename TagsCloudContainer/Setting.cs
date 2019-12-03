using System.Drawing;
using System.IO;

namespace TagsCloudContainer
{
    public class Setting
    {
        public Font Font { get; }
        public Brush Brush { get; }
        public string Path { get; }
        public int Height { get; }
        public int Width { get; }

        public Setting(string path, string fontName, int size, int width, int height, string color)
        {
            Width = width;
            Height = height;
            Path = path;
            Font = new Font(fontName, size);
            Brush = new SolidBrush(Color.FromName(color));
        }
    }
}