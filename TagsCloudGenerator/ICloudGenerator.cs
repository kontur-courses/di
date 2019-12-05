using System.Drawing;
using TagsCloudGenerator.Visualizer;

namespace TagsCloudGenerator
{
    public interface ICloudGenerator
    {
        void Generate(string inputPath, string outputPath, ImageSettings settings);
    }
}
