using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.App.CloudGenerator;

namespace TagsCloudContainer.Infrastructure.CloudVisualizer
{
    internal interface IImageGenerator
    {
        public Bitmap GenerateImage(IEnumerable<Tag> cloud);
    }
}