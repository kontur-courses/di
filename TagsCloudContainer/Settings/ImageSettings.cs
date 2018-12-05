using System.Drawing;

namespace TagsCloudContainer.Settings
{
    public class ImageSettings
    {
        public ImageSettings(int height, int width, string outputFile)
        {
            OutputFile = outputFile;
            Height = height;
            Width = width;
            Center = new Point(height / 2, width / 2);
        }

        public int Height { get; }
        public int Width { get; }
        public Point Center { get; }
        public string OutputFile { get; }
    }
}