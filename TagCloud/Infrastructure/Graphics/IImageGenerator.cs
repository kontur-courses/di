using System.Drawing;

namespace TagCloud.Infrastructure.Graphics
{
    public interface IImageGenerator
    {
        public Image Generate();
    }
}