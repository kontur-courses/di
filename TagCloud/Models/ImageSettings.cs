namespace TagCloud.Models
{
    public class ImageSettings
    {
        public ImageSettings(int width, int height, string fontName, string paletteName)
        {
            PaletteName = paletteName;
            Width = width;
            Height = height;
            FontName = fontName;
        }

        public int Width { get; }
        public int Height { get; }
        public string FontName { get; }
        public string PaletteName { get; }
    }
}