using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.App.CloudGenerator;
using TagsCloudContainer.App.Settings;

namespace TagsCloudContainer.Infrastructure.CloudVisualizer
{
    internal interface IImageGenerator
    {
        public Bitmap GenerateImage(IEnumerable<Tag> cloud, ImageSettings settings);
    }
}