using System.Drawing;

namespace TagsCloudVisualization
{
    public static class ImageSaver
    {
        public static void WriteToFile(string fileName, Image bitmap)
        {
            bitmap.Save(fileName, bitmap.RawFormat);
        }
    }
}
