using System.Drawing;

namespace TagsCloudVisualization
{
    public class ImageSaver
    {
        public void Save(Bitmap image, string filename)
        {
            image.Save(filename);
        }
    }
}
