using System.Drawing;

namespace TagsCloudContainer.Drawing
{
    public class ImageWriter : IWriter
    {
        public void Write(Bitmap image, string name) => image.Save(name);
    }
}
