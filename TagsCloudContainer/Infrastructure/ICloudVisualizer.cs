using System.Drawing;
using TagsCloudContainer.App.Settings;

namespace TagsCloudContainer.Infrastructure
{
    internal interface ICloudVisualizer
    {
        public Bitmap Visualize(string inputFileName, ImageSettings imageSettings);
    }
}