using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Infrastructure
{
    internal interface IImageGenerator
    {
        public Bitmap GenerateImage(Dictionary<string, Rectangle> cloud, Size imageSize);
    }
}