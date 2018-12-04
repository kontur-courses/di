using System.Drawing;
using System.Runtime.InteropServices;

namespace TagsCloudContainer.Settings
{
    public class ImageSettings
    {
        public int Heigth { get; set; }
        public int Width { get; set; }
        public Point Center { get; set; }
        public string OutputFile { get; set; }

        public ImageSettings(int h, int w, string outputFile)
        {
            OutputFile = outputFile;
            Heigth = h;
            Width = w;
            Center = new Point(h/2, w/2);
        }
    }
}