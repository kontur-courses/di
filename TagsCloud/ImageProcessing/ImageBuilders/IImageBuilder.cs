using System.Drawing;

namespace TagsCloud.ImageProcessing.ImageBuilders
{
    public interface IImageBuilder
    {
        public Bitmap BuildImage(string textPath);
    }
}
