using System.Drawing;

namespace TagsCloudContainer.Infrastructure
{
    internal interface ICloudVisualizer
    {
        public Bitmap Visualize(string inputFileName, Size imageSize);
    }
}