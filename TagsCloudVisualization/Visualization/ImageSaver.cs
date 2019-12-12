using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudVisualization.PathFinders;

namespace TagsCloudVisualization.Visualization
{
    public static class ImageSaver
    {
        public static string SaveImageToDefaultDirectory(string name, Bitmap image, ImageFormat format)
        {
            var path = PathFinder.GetImagesPath(name, format);
            image.Save(path, format);

            return path;
        }
        
        public static string SaveImage(string path, Bitmap image, ImageFormat format)
        {
            image.Save(path, format);

            return path;
        }
    }
}