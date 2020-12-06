using System.Drawing;

namespace TagCloud.App
{
    public interface IImageGenerator
    {
        public Image Generate();
    }
}