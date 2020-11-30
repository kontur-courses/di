using System.Drawing;
using TagsCloudContainer.App.Settings;

namespace TagsCloudContainer.Infrastructure.CloudVisualizer
{
    internal interface ICloudVisualizer
    {
        public Bitmap Visualize(string inputFileName, ImageSettings imageSettings);
    }
}